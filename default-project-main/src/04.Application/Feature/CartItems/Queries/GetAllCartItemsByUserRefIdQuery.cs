using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.CartItems.Queries;

public class GetAllCartItemsByUserRefIdQuery : IRequest<ApiResponse<List<CartItemResponseDto>>>
{
    public Guid UserRefId { get; set; }
}
public class GetAllCartItemsByUserRefIdQueryHandler : IRequestHandler<GetAllCartItemsByUserRefIdQuery, ApiResponse<List<CartItemResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllCartItemsByUserRefIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<CartItemResponseDto>>> Handle(GetAllCartItemsByUserRefIdQuery request, CancellationToken cancellationToken)
    {
        var cartItems = await _unitOfWork.CartItems.GetAllByUserRefIdAsync(request.UserRefId);
        if (cartItems == null)
        {
            throw new NotFoundException("User RefId", request.UserRefId);
        }

        var cartItemsDto = _mapper.Map<List<CartItemResponseDto>>(cartItems);

        return ApiResponse<List<CartItemResponseDto>>.SuccessResult(cartItemsDto);
    }
}
