using AutoMapper;
using MediatR;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.Categories.Queries;

public class GetAllCategoriesQuery : IRequest<ApiResponse<List<CategoryDto>>>
{
}
public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, ApiResponse<List<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var categories = await _unitOfWork.Categories.GetAllAsync();

        var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

        return ApiResponse<List<CategoryDto>>.SuccessResult(categoriesDto);
    }
}
