using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;
using MyApp.Application.Services;

namespace MyApp.Application.Feature.Authentications.Commands;

public class ConfirmEmailCommand : IRequest<ApiResponse<object>>
{
    public required ConfirmEmailRequestDto ConfirmEmailDto { get; set; }
}
public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ConfirmEmailCommandHandler> _logger;
    public ConfirmEmailCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<ConfirmEmailCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<ApiResponse<object>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.GetUserByEmailAsync(request.ConfirmEmailDto.Email);
        if (user == null)
        {
            _logger.LogWarning("Confirm email failed: User not found or inactive for email: {Email}", request.ConfirmEmailDto.Email);
            throw new ValidationException("Invalid email");
        }
        // Cek token valid dan belum expired
        if (user.EmailConfirmationToken != request.ConfirmEmailDto.EmailConfirmationToken || user.EmailConfirmationTokenExpiry < DateTime.UtcNow)
        {
            throw new ValidationException("Invalid or expired confirmation token");
        }
        // Tandai email sebagai terverifikasi
        user.EmailConfirmed = true;
        user.EmailConfirmationToken = null; // hapus token agar tidak bisa digunakan lagi
        user.EmailConfirmationTokenExpiry = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Confirm email success for: {Email}", request.ConfirmEmailDto.Email);
        return ApiResponse<object>.SuccessResult();
    }
}