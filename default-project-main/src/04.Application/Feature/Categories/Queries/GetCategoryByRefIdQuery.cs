using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Categories.Queries;

public class GetCategoryByRefIdQuery : IRequest<ApiResponse<CategoryDto>>
{
    public Guid RefId { get; set; }
}
public class GetCategoryByRefIdQueryHandler : IRequestHandler<GetCategoryByRefIdQuery, ApiResponse<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetCategoryByRefIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<CategoryDto>> Handle(GetCategoryByRefIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByRefIdAsync(request.RefId);
        if (category == null)
        {
            throw new NotFoundException("Category RefId", request.RefId);
        }

        var categoryDto = _mapper.Map<CategoryDto>(category);

        return ApiResponse<CategoryDto>.SuccessResult(categoryDto);
    }
}
