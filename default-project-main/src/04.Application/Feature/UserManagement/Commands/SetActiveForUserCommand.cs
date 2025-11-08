using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Schedules.Commands;

public class SetActiveForUserCommand : IRequest<ApiResponse<object>>
{
    public Guid RefId { get; set; }
    public bool isActive { get; set; }
}
public class SetActiveForUserCommandHandler : IRequestHandler<SetActiveForUserCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<SetActiveForUserCommandHandler> _logger;
    public SetActiveForUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SetActiveForUserCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(SetActiveForUserCommand request, CancellationToken cancellationToken)
    {
        var changed = await _unitOfWork.UserManager.SetActiveForUserAsync(request.RefId, request.isActive);
        await _unitOfWork.SaveChangesAsync();
        if (!changed)
        {
            throw new NotFoundException("User RefId", request.RefId);
        }
        if (request.isActive)
        {
            _logger.LogInformation("User {UserRefId} activated", request.RefId);
        }
        else
        {
            _logger.LogInformation("User {UserRefId} deactivated", request.RefId);
        }
        return ApiResponse<object>.SuccessResult();
    }
}
