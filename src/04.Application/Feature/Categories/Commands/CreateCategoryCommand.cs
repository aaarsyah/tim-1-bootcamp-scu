using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Categories.Commands;

public class CreateCategoryCommand : IRequest<ApiResponse<CategoryDto>>
{
    public CreateCategoryRequestDto createCategoryDto { get; set; } = null!;
}
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCategoryCommandHandler> _logger;
    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCategoryCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.createCategoryDto);
        var imageurl = request.createCategoryDto.ImageUrl;
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
            category.ImageUrl = $"/img/{fileName}";
        }

        await _unitOfWork.Categories.CreateAsync(category);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Category created: {CategoryName} with RefID: {CategoryRefId}",
            category.Name, category.RefId);

        var categoryDto = _mapper.Map<CategoryDto>(category);

        return ApiResponse<CategoryDto>.SuccessResult(categoryDto);
    }
}
