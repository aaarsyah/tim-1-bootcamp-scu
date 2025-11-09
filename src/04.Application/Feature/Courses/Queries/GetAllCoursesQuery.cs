using AutoMapper;
using MediatR;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Courses.Queries;

public class GetAllCoursesQuery : IRequest<ApiResponse<List<CourseDto>>>
{
}
public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, ApiResponse<List<CourseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllCoursesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var courses = await _unitOfWork.Courses.GetAllAsync();

        var coursesDto = _mapper.Map<List<CourseDto>>(courses);

        return ApiResponse<List<CourseDto>>.SuccessResult(coursesDto);
    }
}
