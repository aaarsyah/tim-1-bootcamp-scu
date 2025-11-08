using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.CartItems.Commands;

public class AddCourseToCartCommand : IRequest<ApiResponse<CartItemResponseDto>>
{
    public Guid UserRefId { get; set; }
    public Guid ScheduleRefId { get; set; }
}
public class AddCourseToCartCommandHandler : IRequestHandler<AddCourseToCartCommand, ApiResponse<CartItemResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<AddCourseToCartCommandHandler> _logger;
    public AddCourseToCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddCourseToCartCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<CartItemResponseDto>> Handle(AddCourseToCartCommand request, CancellationToken cancellationToken)
    {
        // ===== STEP 1 =====
        var user = await _unitOfWork.UserManager.GetUserByRefIdAsync(request.UserRefId);
        if (user == null)
        {
            throw new ValidationException($"Invalid UserRefId {request.UserRefId} ");
        }
        // ===== STEP 2 =====
        if (!user.EmailConfirmed)
        {
            throw new AccountInactiveException("User has not confirmed email");
        }
        if (!user.IsActive)
        {
            throw new AccountInactiveException("Account is inactive. Contact the administrator for help.");
        }
        // ===== STEP 3 =====
        var schedule = await _unitOfWork.Schedules.GetByRefIdAsync(request.ScheduleRefId);
        if (schedule == null)
        {
            throw new ValidationException($"ScheduleRefId {request.ScheduleRefId} not found");
        }
        // ===== STEP 4 =====
        var cartItem = new CartItem
        {
            UserId = user.Id,
            ScheduleId = schedule.Id
        };
        await _unitOfWork.CartItems.CreateAsync(cartItem);
        // ===== STEP 5 =====
        // All operations succeeded, make permanent
        await _unitOfWork.SaveChangesAsync();

        var cartItemDto = _mapper.Map<CartItemResponseDto>(cartItem);

        return ApiResponse<CartItemResponseDto>.SuccessResult(cartItemDto);
    }
}
