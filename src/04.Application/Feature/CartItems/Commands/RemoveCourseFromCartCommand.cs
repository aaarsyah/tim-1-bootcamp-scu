using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.CartItems.Commands;

public class RemoveCourseFromCartCommand : IRequest<ApiResponse<object>>
{
    public Guid UserRefId { get; set; }
    public Guid CartItemRefId { get; set; }
}
public class RemoveCourseFromCartCommandHandler : IRequestHandler<RemoveCourseFromCartCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<RemoveCourseFromCartCommandHandler> _logger;
    public RemoveCourseFromCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RemoveCourseFromCartCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(RemoveCourseFromCartCommand request, CancellationToken cancellationToken)
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
        var cartitem = await _unitOfWork.CartItems
            .GetByRefIdAsync(request.CartItemRefId);
        if (cartitem == null)
        {
            throw new ValidationException($"CartItemRefId {request.CartItemRefId} not found");
        }
        // ===== STEP 4 =====
        await _unitOfWork.CartItems.DeleteAsync(request.CartItemRefId);
        // ===== STEP 5 =====
        // All operations succeeded, make permanent
        await _unitOfWork.SaveChangesAsync();

        return ApiResponse<object>.SuccessResult();
    }
}
