using AutoMapper;
using MediatR;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.PaymentMethods.Queries;

public class GetAllPaymentMethodsQuery : IRequest<ApiResponse<List<CategoryDto>>>
{
}
public class GetAllPaymentMethodsQueryHandler : IRequestHandler<GetAllPaymentMethodsQuery, ApiResponse<List<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllPaymentMethodsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<CategoryDto>>> Handle(GetAllPaymentMethodsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Tambahkan caching
        var paymentMethods = await _unitOfWork.PaymentMethods.GetAllAsync();

        var paymentMethodsDto = _mapper.Map<List<CategoryDto>>(paymentMethods);

        return ApiResponse<List<CategoryDto>>.SuccessResult(paymentMethodsDto);
    }
}
