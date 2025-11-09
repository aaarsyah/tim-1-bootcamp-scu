using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyApp.Base.Exceptions;
using MyApp.Infrastructure.Configuration;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Shared.DTOs;
using MyApp.Application.Services;

namespace MyApp.Application.Feature.Authentications.Commands;

public class LogoutCommand : IRequest<ApiResponse<object>>
{
    public Guid UserRefId { get; set; }
}
public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<LogoutCommandHandler> _logger;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IPasswordService _passwordService;
    private readonly JwtSettings _jwtSettings;
    public LogoutCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<LogoutCommandHandler> logger,
        ITokenService tokenService,
        IEmailService emailService,
        IPasswordService passwordService,
        JwtSettings jwtSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _tokenService = tokenService;
        _emailService = emailService;
        _passwordService = passwordService;
        _jwtSettings = jwtSettings;
    }
    public async Task<ApiResponse<object>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserManager.GetUserByRefIdAsync(request.UserRefId);
        if (user == null)
        {
            throw new ValidationException("User is invalid");
        }
        // Invalidate refresh token
        user.RefreshToken = null;
        user.RefreshTokenExpiry = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User logged out: {UserRefId}", user.RefId);
        return ApiResponse<object>.SuccessResult();
    }
}