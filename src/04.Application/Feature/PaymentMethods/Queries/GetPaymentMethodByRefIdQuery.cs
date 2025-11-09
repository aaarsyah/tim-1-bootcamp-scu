using AutoMapper;
using MediatR;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.PaymentMethods.Queries;

public class GetPaymentMethodByRefIdQuery : IRequest<ApiResponse<PaymentMethodDto>>
{
    public Guid RefId { get; set; }
}
public class GetPaymentMethodByRefIdQueryHandler : IRequestHandler<GetPaymentMethodByRefIdQuery, ApiResponse<PaymentMethodDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetPaymentMethodByRefIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResponse<PaymentMethodDto>> Handle(GetPaymentMethodByRefIdQuery request, CancellationToken cancellationToken)
    {
        var paymentMethod = await _unitOfWork.PaymentMethods.GetByRefIdAsync(request.RefId);
        if (paymentMethod == null)
        {
            throw new NotFoundException("PaymentMethod RefId", request.RefId);
        }

        var paymentMethodDto = _mapper.Map<PaymentMethodDto>(paymentMethod);

        return ApiResponse<PaymentMethodDto>.SuccessResult(paymentMethodDto);
    }
}
