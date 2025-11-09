using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.PaymentMethods.Commands;

public class UpdatePaymentMethodCommand : IRequest<ApiResponse<PaymentMethodDto>>
{
    public Guid RefId { get; set; }
    public required UpdatePaymentMethodRequestDto updatePaymentMethodDto { get; set; }
}
public class UpdatePaymentMethodCommandHandler : IRequestHandler<UpdatePaymentMethodCommand, ApiResponse<PaymentMethodDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdatePaymentMethodCommandHandler> _logger;
    public UpdatePaymentMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdatePaymentMethodCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<PaymentMethodDto>> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = await _unitOfWork.PaymentMethods
            .GetByRefIdAsync(request.RefId);
        if (paymentMethod == null)
        {
            throw new NotFoundException("PaymentMethod RefId", request.RefId);
        }

        _mapper.Map(request.updatePaymentMethodDto, paymentMethod);
        var updatedPaymentMethod = await _unitOfWork.PaymentMethods.UpdateAsync(paymentMethod);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("PaymentMethod updated: {PaymentMethodRefId}", request.RefId);

        var paymentMethodDto = _mapper.Map<PaymentMethodDto>(updatedPaymentMethod);

        return ApiResponse<PaymentMethodDto>.SuccessResult(paymentMethodDto);
    }
}
