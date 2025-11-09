using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Application.Configuration;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;
using MyApp.Application.Services;
using System.Security.Claims;

namespace MyApp.Application.Feature.Authentications.Commands;

public class RefreshTokenCommand : IRequest<ApiResponse<AuthResponseDto>>
{
    public RefreshTokenRequestDto RefreshTokenDto { get; set; }
}
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<AuthResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;
    public RefreshTokenCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        JwtSettings jwtSettings)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings;
    }
    public async Task<ApiResponse<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Extract claims dari expired access token
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.RefreshTokenDto.AccessToken);
        if (principal == null)
        {
            throw new ValidationException("Invalid access token");
        }

        var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null || !Guid.TryParse(claim.Value, out Guid userRefId))
        {
            throw new ValidationException("Invalid token claims");
        }

        // Validasi refresh token
        var user = await _unitOfWork.UserManager.GetUserByRefIdAsync(userRefId);
        if (user == null)
        {
            //return new AuthResponseDto
            //{
            //    Success = false,
            //    Message = "Invalid refresh token"
            //};
            throw new ValidationException("User is invalid");
        }
        if (!user.EmailConfirmed)
        {
            throw new AccountInactiveException("User has not confirmed email");
        }
        if (!user.IsActive)
        {
            throw new AccountInactiveException("Account is inactive. Contact the administrator for help.");
        }
        if (user.RefreshToken != request.RefreshTokenDto.RefreshToken)
        {
            throw new ValidationException("Refresh token is invalid");
        }
        if (user.RefreshTokenExpiry <= DateTime.UtcNow)
        {
            throw new ValidationException("Refresh token has expired");
        }
        // Generate tokens baru
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        // Update refresh token
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
        await _unitOfWork.SaveChangesAsync();

        var authResponseDto = new AuthResponseDto
        {
            Message = "Token refreshed successfully",
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes)
        };
        return ApiResponse<AuthResponseDto>.SuccessResult(authResponseDto);
    }
}