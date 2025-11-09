using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Schedules.Commands;

public class SetClaimForUserCommand : IRequest<ApiResponse<object>>
{
    public Guid RefId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}
public class SetClaimForUserCommandHandler : IRequestHandler<SetClaimForUserCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<SetClaimForUserCommandHandler> _logger;
    public SetClaimForUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SetClaimForUserCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(SetClaimForUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.GetUserByRefIdAsync(request.RefId);
        if (user == null)
        {
            throw new NotFoundException("User RefId", request.RefId);
        }
        var userClaim = await _unitOfWork.UserManager.SetClaimForUserAsync(user, request.ClaimType, request.ClaimValue);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Claim {ClaimType} set to {ClaimValue} for user {UserRefId}", userClaim.ClaimType, userClaim.ClaimValue, user.RefId);

        return ApiResponse<object>.SuccessResult();
    }
}
