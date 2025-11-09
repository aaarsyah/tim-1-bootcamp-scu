using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Courses.Commands;

public class CreateCourseCommand : IRequest<ApiResponse<CourseDto>>
{
    public required CreateCourseRequestDto createCourseDto { get; set; }
}
public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ApiResponse<CourseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCourseCommandHandler> _logger;
    public CreateCourseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCourseCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<CourseDto>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = _mapper.Map<Course>(request.createCourseDto);
        var imageurl = request.createCourseDto.ImageUrl;
        // === Upload Image (jika ImageUrl berisi base64 dari client) ===
        if (!string.IsNullOrWhiteSpace(imageurl) &&
            imageurl.StartsWith("data:image"))
        {
            // Contoh: data:image/png;base64,AAAA...
            var base64Data = imageurl.Substring(imageurl.IndexOf(",") + 1);
            var bytes = Convert.FromBase64String(base64Data);

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}.png";
            var filePath = Path.Combine(uploadsFolder, fileName);
            await File.WriteAllBytesAsync(filePath, bytes);

            // Simpan URL publik ke database
            course.ImageUrl = $"/img/{fileName}";
        }

        await _unitOfWork.Courses.CreateAsync(course);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Course created: {CourseName} with RefID: {CourseRefId}",
            course.Name, course.RefId);

        var courseDto = _mapper.Map<CourseDto>(course);

        return ApiResponse<CourseDto>.SuccessResult(courseDto);
    }
}
