using AutoMapper;
using MediatR;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Schedules.Queries;

public class GetAllSchedulesQuery : IRequest<ApiResponse<List<CategoryDto>>>
{
}
public class GetAllSchedulesQueryHandler : IRequestHandler<GetAllSchedulesQuery, ApiResponse<List<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllSchedulesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<CategoryDto>>> Handle(GetAllSchedulesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var Schedules = await _unitOfWork.Schedules.GetAllAsync();

        var SchedulesDto = _mapper.Map<List<CategoryDto>>(Schedules);

        return ApiResponse<List<CategoryDto>>.SuccessResult(SchedulesDto);
    }
}
