using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Courses.Queries;

public class GetCourseByRefIdQuery : IRequest<ApiResponse<CourseDto>>
{
    public Guid RefId { get; set; }
}
public class GetCourseByRefIdQueryHandler : IRequestHandler<GetCourseByRefIdQuery, ApiResponse<CourseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetCourseByRefIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<CourseDto>> Handle(GetCourseByRefIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Courses.GetByRefIdAsync(request.RefId);
        if (course == null)
        {
            throw new NotFoundException("Course RefId", request.RefId);
        }

        var courseDto = _mapper.Map<CourseDto>(course);

        return ApiResponse<CourseDto>.SuccessResult(courseDto);
    }
}
