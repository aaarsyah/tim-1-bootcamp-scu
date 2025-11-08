using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.PaymentMethods.Commands;

public class DeletePaymentMethodCommand : IRequest<ApiResponse<object>>
{
    public Guid RefId { get; set; }
}
public class DeletePaymentMethodCommandHandler : IRequestHandler<DeletePaymentMethodCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<DeletePaymentMethodCommandHandler> _logger;
    public DeletePaymentMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeletePaymentMethodCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = await _unitOfWork.PaymentMethods
            .GetByRefIdAsync(request.RefId);
        if (paymentMethod == null)
        {
            throw new NotFoundException("PaymentMethod RefId", request.RefId);
        }

        await _unitOfWork.PaymentMethods.DeleteAsync(request.RefId);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("PaymentMethod deleted: {PaymentMethodRefId}", request.RefId);

        return ApiResponse<object>.SuccessResult();
    }
}
