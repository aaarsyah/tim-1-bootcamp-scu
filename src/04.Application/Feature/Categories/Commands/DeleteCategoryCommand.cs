using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Categories.Commands;

public class DeleteCategoryCommand : IRequest<ApiResponse<object>>
{
    public Guid RefId { get; set; }
}
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;
    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteCategoryCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories
            .GetByRefIdAsync(request.RefId);
        if (category == null)
        {
            throw new NotFoundException("Category RefId", request.RefId);
        }

        // Check if category has products
        if (category.Courses.Any())
        {
            throw new ValidationException("Cannot delete category that has products");
        }

        await _unitOfWork.Categories.DeleteAsync(request.RefId);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Category deleted: {CategoryRefId}", request.RefId);

        return ApiResponse<object>.SuccessResult();
    }
}
