using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Schedules.Queries;

public class GetScheduleByRefIdQuery : IRequest<ApiResponse<ScheduleDto>>
{
    public Guid RefId { get; set; }
}
public class GetScheduleByRefIdQueryHandler : IRequestHandler<GetScheduleByRefIdQuery, ApiResponse<ScheduleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetScheduleByRefIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<ScheduleDto>> Handle(GetScheduleByRefIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _unitOfWork.Schedules.GetByRefIdAsync(request.RefId);
        if (schedule == null)
        {
            throw new NotFoundException("Schedule RefId", request.RefId);
        }

        var scheduleDto = _mapper.Map<ScheduleDto>(schedule);

        return ApiResponse<ScheduleDto>.SuccessResult(scheduleDto);
    }
}
