using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Feature.PaymentMethods.Commands;

public class CreatePaymentMethodCommand : IRequest<ApiResponse<PaymentMethodDto>>
{
    public CreatePaymentMethodRequestDto createPaymentMethodDto { get; set; } = null!;
}
public class CreatePaymentMethodCommandHandler : IRequestHandler<CreatePaymentMethodCommand, ApiResponse<PaymentMethodDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePaymentMethodCommandHandler> _logger;
    public CreatePaymentMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreatePaymentMethodCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<ApiResponse<PaymentMethodDto>> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = _mapper.Map<PaymentMethod>(request.createPaymentMethodDto);
        var logourl = request.createPaymentMethodDto.LogoUrl;
        // === Upload Image (jika ImageUrl berisi base64 dari client) ===
        if (!string.IsNullOrWhiteSpace(logourl) &&
            logourl.StartsWith("data:image"))
        {
            // Contoh: data:image/png;base64,AAAA...
            var base64Data = logourl.Substring(logourl.IndexOf(",") + 1);
            var bytes = Convert.FromBase64String(base64Data);

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}.png";
            var filePath = Path.Combine(uploadsFolder, fileName);
            await File.WriteAllBytesAsync(filePath, bytes);

            // Simpan URL publik ke database
            paymentMethod.LogoUrl = $"/img/{fileName}";
        }

        await _unitOfWork.PaymentMethods.CreateAsync(paymentMethod);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("PaymentMethod created: {PaymentMethodName} with RefID: {PaymentMethodRefId}",
            paymentMethod.Name, paymentMethod.RefId);

        var paymentMethodDto = _mapper.Map<PaymentMethodDto>(paymentMethod);

        return ApiResponse<PaymentMethodDto>.SuccessResult(paymentMethodDto);
    }
}
