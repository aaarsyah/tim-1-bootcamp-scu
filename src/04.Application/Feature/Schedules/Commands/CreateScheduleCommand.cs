using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Schedules.Commands;

public class CreateScheduleCommand : IRequest<ApiResponse<ScheduleDto>>
{
    public required CreateScheduleRequestDto createScheduleDto { get; set; }
}
public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, ApiResponse<ScheduleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateScheduleCommandHandler> _logger;
    public CreateScheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateScheduleCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<ScheduleDto>> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = _mapper.Map<Schedule>(request.createScheduleDto);

        await _unitOfWork.Schedules.CreateAsync(schedule);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Schedule created with RefID: {ScheduleRefId}", schedule.RefId);

        var ScheduleDto = _mapper.Map<ScheduleDto>(schedule);

        return ApiResponse<ScheduleDto>.SuccessResult(ScheduleDto);
    }
}
