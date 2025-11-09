using AutoMapper;
using MediatR;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.CartItems.Queries;

public class GetAllCartItemsQuery : IRequest<ApiResponse<List<CartItemResponseDto>>>
{
    public Guid UserRefId { get; set; }
}
public class GetAllCartItemsQueryHandler : IRequestHandler<GetAllCartItemsQuery, ApiResponse<List<CartItemResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllCartItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<CartItemResponseDto>>> Handle(GetAllCartItemsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var cartItems = await _unitOfWork.CartItems.GetAllAsync();

        var cartItemsDto = _mapper.Map<List<CartItemResponseDto>>(cartItems);

        return ApiResponse<List<CartItemResponseDto>>.SuccessResult(cartItemsDto);
    }
}
