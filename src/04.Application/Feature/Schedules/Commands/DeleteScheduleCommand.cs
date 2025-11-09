using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Schedules.Commands;

public class DeleteScheduleCommand : IRequest<ApiResponse<object>>
{
    public Guid RefId { get; set; }
}
public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteScheduleCommandHandler> _logger;
    public DeleteScheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteScheduleCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _unitOfWork.Schedules
            .GetByRefIdAsync(request.RefId);
        if (schedule == null)
        {
            throw new NotFoundException("Schedule RefId", request.RefId);
        }

        await _unitOfWork.Schedules.DeleteAsync(request.RefId);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Schedule deleted: {ScheduleRefId}", request.RefId);

        return ApiResponse<object>.SuccessResult();
    }
}
