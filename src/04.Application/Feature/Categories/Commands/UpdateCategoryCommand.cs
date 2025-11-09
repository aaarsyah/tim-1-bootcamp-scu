using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Categories.Commands;

public class UpdateCategoryCommand : IRequest<ApiResponse<CategoryDto>>
{
    public Guid RefId { get; set; }
    public UpdateCategoryRequestDto updateCategoryDto { get; set; } = null!;
}
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResponse<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCategoryCommandHandler> _logger;
    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateCategoryCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories
            .GetByRefIdAsync(request.RefId);
        if (category == null)
        {
            throw new NotFoundException("Category RefId", request.RefId);
        }

        _mapper.Map(request.updateCategoryDto, category);
        var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Category updated: {CategoryRefId}", request.RefId);

        var categoryDto = _mapper.Map<CategoryDto>(updatedCategory);

        return ApiResponse<CategoryDto>.SuccessResult(categoryDto);
    }
}
