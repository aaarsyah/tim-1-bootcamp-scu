using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Courses.Commands;

public class DeleteCourseCommand : IRequest<ApiResponse<object>>
{
    public Guid RefId { get; set; }
}
public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCourseCommandHandler> _logger;
    public DeleteCourseCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteCourseCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Courses
            .GetByRefIdAsync(request.RefId);
        if (course == null)
        {
            throw new NotFoundException("Course RefId", request.RefId);
        }

        await _unitOfWork.Courses.DeleteAsync(request.RefId);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Course deleted: {CourseRefId}", request.RefId);

        return ApiResponse<object>.SuccessResult();
    }
}
