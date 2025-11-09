using AutoMapper;
using MediatR;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Schedules.Queries;

public class GetAllSchedulesQuery : IRequest<ApiResponse<List<ScheduleDto>>>
{
}
public class GetAllSchedulesQueryHandler : IRequestHandler<GetAllSchedulesQuery, ApiResponse<List<ScheduleDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllSchedulesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<ScheduleDto>>> Handle(GetAllSchedulesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var Schedules = await _unitOfWork.Schedules.GetAllAsync();

        var SchedulesDto = _mapper.Map<List<ScheduleDto>>(Schedules);

        return ApiResponse<List<ScheduleDto>>.SuccessResult(SchedulesDto);
    }
}
