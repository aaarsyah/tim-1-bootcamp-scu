using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.CartItems.Commands;

public class CheckoutCartItemsCommand : IRequest<ApiResponse<CheckoutResponseDto>>
{
    public Guid UserRefId { get; set; }
    public required CheckoutRequestDto CheckoutDto { get; set; }
}
public class CheckoutCartItemsCommandHandler : IRequestHandler<CheckoutCartItemsCommand, ApiResponse<CheckoutResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CheckoutCartItemsCommandHandler> _logger;
    public CheckoutCartItemsCommandHandler(IUnitOfWork unitOfWork, ILogger<CheckoutCartItemsCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<ApiResponse<CheckoutResponseDto>> Handle(CheckoutCartItemsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
        "Checking out {ItemCartIdsCount} items for {UserId}",
        request.CheckoutDto.ItemCartRefIds.Count,
        request.UserRefId);
        // ===== VALIDATION: Cart item is not empty =====
        // Business Rule: Cannot check out empty cart
        if (request.CheckoutDto.ItemCartRefIds.Count == 0)
        {
            throw new ValidationException("Checked out items cannot be empty");
        }
        // ===== USE EXECUTION STRATEGY WITH RETRY =====
        // Purpose: Make transaction compatible with retry logic
        var strategy = _unitOfWork.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            // ===== STEP 1 =====
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                // ===== STEP 2 =====
                var user = await _unitOfWork.UserManager
                    .GetUserByRefIdAsync(request.UserRefId);
                if (user == null)
                {
                    throw new ValidationException($"Invalid UserRefId {request.UserRefId} ");
                }
                // ===== STEP 3 =====
                if (!user.EmailConfirmed)
                {
                    throw new PermissionException($"UserId {user.Id} not active");
                }
                // ===== STEP 4 =====
                var cartItems = (await _unitOfWork.CartItems
                    .GetAllByRefIdsAsync(request.CheckoutDto.ItemCartRefIds))
                    .ToList();
                if (cartItems.Count != request.CheckoutDto.ItemCartRefIds.Count)
                {
                    var missingCartItems = cartItems.Select(e => e.RefId).Except(request.CheckoutDto.ItemCartRefIds);
                    throw new ValidationException($"CartId {string.Join(", ", missingCartItems)} not found"); // Return salah satu cartitem id yang invalid
                }
                // ===== STEP 5 =====
                var paymentmethod = await _unitOfWork.PaymentMethods
                    .GetByRefIdAsync(request.CheckoutDto.PaymentMethodRefId);
                if (paymentmethod == null)
                {
                    throw new ValidationException($"PaymentMethodRefId {request.CheckoutDto.PaymentMethodRefId} not found");
                }
                // ===== STEP 6 =====
                await _unitOfWork.CartItems.DeleteEntitiesAsync(cartItems);
                // ===== STEP 7 AND 8 =====
                var invoice = new Invoice
                {
                    RefCode = GenerateInvoiceId(),
                    CreatedAt = DateTime.UtcNow,
                    UserId = user.Id,
                    PaymentMethodId = paymentmethod.Id
                };
                await _unitOfWork.InvoiceManager.CreateAsync(invoice);
                await _unitOfWork.SaveChangesAsync(); // Save transaction record to generate Id
                foreach (CartItem item in cartItems)
                {
                    await _unitOfWork.InvoiceManager.CreateDetailAsync(new InvoiceDetail
                    {
                        InvoiceId = invoice.Id, // Id generated from saving transaction
                        CourseName = item.Schedule.Course.Name,
                        CategoryName = item.Schedule.Course.Category.Name,
                        Price = item.Schedule.Course.Price,
                        ScheduleDate = item.Schedule.Date
                    });
                    await _unitOfWork.MyClasses.CreateAsync(new MyClass
                    {
                        UserId = user.Id,
                        ScheduleId = item.ScheduleId
                    });
                }
                // ===== STEP 9 =====
                // All operations succeeded, make permanent
                _unitOfWork.CommitTransaction(transaction);

                _logger.LogInformation(
                    "Checkout successful. Invoice ID: {invoiceId}",
                    invoice.Id);

                // ===== RETURN RESPONSE =====
                return ApiResponse<CheckoutResponseDto>.SuccessResult(new CheckoutResponseDto
                {
                    InvoiceId = invoice.Id,
                    CreatedAt = invoice.CreatedAt
                });
            }
            catch (Exception)
            {
                // ===== ROLLBACK ON ERROR =====
                // Any exception -> undo all changes
                _unitOfWork.RollbackTransaction(transaction);
                _logger.LogError("Checkout failed, transaction rolled back");
                throw; // Re-throw to be handled by middleware
            }
        });
    }
    /// <summary>
    /// Generate unique transaction ID<br />
    /// Format: APM{yyMMddHHmmss}{random 6 digits}<br />
    /// Example: APM251005123059012345
    /// </summary>
    private string GenerateInvoiceId()
    {
        return $"APM{DateTime.UtcNow:yyMMddHHmmss}{Random.Shared.Next(0, 999999):D6}";
    }
}
