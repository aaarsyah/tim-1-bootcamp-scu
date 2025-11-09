using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Courses.Commands;

public class UpdateCourseCommand : IRequest<ApiResponse<CourseDto>>
{
    public Guid RefId { get; set; }
    public UpdateCourseRequestDto UpdateCourseDto { get; set; } = null!;
}
public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, ApiResponse<CourseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCourseCommandHandler> _logger;
    public UpdateCourseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateCourseCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<CourseDto>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Courses
            .GetByRefIdAsync(request.RefId);
        if (course == null)
        {
            throw new NotFoundException("Course RefId", request.RefId);
        }

        _mapper.Map(request.UpdateCourseDto, course);
        var updatedCourse = await _unitOfWork.Courses.UpdateAsync(course);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Course updated: {CourseRefId}", request.RefId);

        var courseDto = _mapper.Map<CourseDto>(updatedCourse);

        return ApiResponse<CourseDto>.SuccessResult(courseDto);
    }
}
