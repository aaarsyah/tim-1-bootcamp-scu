using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MyApp.Application.Services;
using MyApp.Domain.Models;
using MyApp.Application.Configuration;
using MyApp.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace MyApp.Application.Tests.Services;

public class TokenServiceTests
{
    private readonly JwtSettings _mockConfiguration;
    private readonly Mock<ILogger<TokenService>> _mockLogger;
    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        //No need for mock,
        //_mockConfiguration = new Mock<JwtSettings>();
        //_mockConfiguration.Setup(s => s.SecretKey).Returns("YourSuperSecretKeyThatIsAtLeast32CharactersLongForHS256Algorithm");
        //_mockConfiguration.Setup(s => s.Issuer).Returns("AppleMusicAPI");
        //_mockConfiguration.Setup(s => s.Audience).Returns("AppleMusicAPI");
        //_mockConfiguration.Setup(s => s.AccessTokenExpirationMinutes).Returns(10);

        _mockConfiguration = new JwtSettings();
        _mockConfiguration.SecretKey = "YourSuperSecretKeyThatIsAtLeast32CharactersLongForHS256Algorithm";
        _mockConfiguration.Issuer = "AppleMusicAPI";
        _mockConfiguration.Audience = "AppleMusicAPI";
        _mockConfiguration.AccessTokenExpirationMinutes = 10;

        _mockLogger = new Mock<ILogger<TokenService>>();

        _tokenService = new TokenService(_mockConfiguration, _mockLogger.Object);
    }

    [Fact]
    public void GenerateAccessToken_Should_Create_Valid_Token()
    {
        // Arrange
        var guid = Guid.Parse("8E95B5E9-957D-493A-8B7F-6A9433962A19");
        var roles = new List<UserRole>() {
            new UserRole() { Role = new Role() { Name = "User" } }
        };
        var claims = new List<UserClaim>() { };
        var user = new User
        {
            Id = 42,
            RefId = guid,
            Email = "test@example.com",
            Name = "John Doe",
            UserRoles = roles,
            UserClaims = claims
        };

        // Act
        var token = _tokenService.GenerateAccessToken(user);

        // Assert
        token.Should().NotBeEmpty();
        token.Should().Contain(".");
        token.Split('.').Should().HaveCount(3); // JWT format: header.payload.signature
    }

    [Fact]
    public void GenerateAccessToken_Should_Include_User_Claims()
    {
        // Arrange
        var guid = Guid.Parse("8E95B5E9-957D-493A-8B7F-6A9433962A19");
        var roles = new List<UserRole>() {
            new UserRole() { Role = new Role() { Name = "User" } }
        };
        var claims = new List<UserClaim>() { };
        var user = new User
        {
            Id = 42,
            RefId = guid,
            Email = "test@example.com",
            Name = "John Doe",
            UserRoles = roles,
            UserClaims = claims
        };

        // Act
        var token = _tokenService.GenerateAccessToken(user);

        // Assert
        // Parse the token to verify claims
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(token) as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

        jsonToken.Should().NotBeNull();
        jsonToken.Claims.Should().Contain(c => c.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub && c.Value == guid.ToString());
        jsonToken.Claims.Should().Contain(c => c.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email && c.Value == "test@example.com");
        jsonToken.Claims.Should().Contain(c => c.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Name && c.Value == "John Doe");
    }

    [Fact]
    public void GenerateAccessToken_Should_Include_Role_Claims()
    {
        // Arrange
        var guid = Guid.Parse("8E95B5E9-957D-493A-8B7F-6A9433962A19");
        var roles = new List<UserRole>() {
            new UserRole() { Role = new Role() { Name = "User" } },
            new UserRole() { Role = new Role() { Name = "Admin" } },
            new UserRole() { Role = new Role() { Name = "Manager" } }
        };
        var claims = new List<UserClaim>() { };
        var user = new User
        {
            Id = 42,
            RefId = guid,
            Email = "test@example.com",
            Name = "John Doe",
            UserRoles = roles,
            UserClaims = claims
        };

        // Act
        var token = _tokenService.GenerateAccessToken(user);

        // Assert
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(token) as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

        jsonToken.Should().NotBeNull();
        jsonToken.Claims.Should().Contain(c => c.Type == "role" && c.Value == "User");
        jsonToken.Claims.Should().Contain(c => c.Type == "role" && c.Value == "Admin");
        jsonToken.Claims.Should().Contain(c => c.Type == "role" && c.Value == "Manager");
    }

    [Fact]
    public void GenerateAccessToken_Should_Include_Custom_Claims()
    {
        // Arrange
        var guid = Guid.Parse("8E95B5E9-957D-493A-8B7F-6A9433962A19");
        var roles = new List<UserRole>() {
            new UserRole() { Role = new Role() { Name = "User" } },
            new UserRole() { Role = new Role() { Name = "Admin" } },
            new UserRole() { Role = new Role() { Name = "Manager" } }
        };
        var claims = new List<UserClaim>() {
            new UserClaim() { ClaimType = "department", ClaimValue = "IT" },
            new UserClaim() { ClaimType = "location", ClaimValue = "Jakarta" }
        };
        var user = new User
        {
            Id = 42,
            RefId = guid,
            Email = "test@example.com",
            Name = "John Doe",
            UserRoles = roles,
            UserClaims = claims
        };

        // Act
        var token = _tokenService.GenerateAccessToken(user);

        // Assert
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(token) as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

        jsonToken.Should().NotBeNull();
        jsonToken.Claims.Should().Contain(c => c.Type == "department" && c.Value == "IT");
        jsonToken.Claims.Should().Contain(c => c.Type == "location" && c.Value == "Jakarta");
    }

    [Fact]
    public void GenerateRefreshToken_Should_Create_Random_Token()
    {
        // Act
        var refreshToken = _tokenService.GenerateRefreshToken();

        // Assert
        refreshToken.Should().NotBeEmpty();
        refreshToken.Should().NotBeNullOrWhiteSpace();
        refreshToken.Length.Should().BeGreaterThanOrEqualTo(64); // Base64 encoded 64 bytes
    }

    [Fact]
    public void GenerateRefreshToken_Should_Create_Unique_Tokens()
    {
        // Act
        var token1 = _tokenService.GenerateRefreshToken();
        var token2 = _tokenService.GenerateRefreshToken();

        // Assert
        token1.Should().NotBe(token2);
    }

    [Fact]
    public async Task GenerateEmailConfirmationTokenAsync_Should_Create_Random_Token()
    {
        // Act
        var token = await _tokenService.GenerateEmailConfirmationTokenAsync();

        // Assert
        token.Should().NotBeEmpty();
        token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GeneratePasswordResetTokenAsync_Should_Create_Random_Token()
    {
        // Act
        var token = await _tokenService.GeneratePasswordResetTokenAsync();

        // Assert
        token.Should().NotBeEmpty();
        token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GenerateEmailConfirmationTokenAsync_Should_Create_Unique_Tokens()
    {
        // Act
        var token1 = await _tokenService.GenerateEmailConfirmationTokenAsync();
        var token2 = await _tokenService.GenerateEmailConfirmationTokenAsync();

        // Assert
        token1.Should().NotBe(token2);
    }

    [Fact]
    public async Task GeneratePasswordResetTokenAsync_Should_Create_Unique_Tokens()
    {
        // Act
        var token1 = await _tokenService.GeneratePasswordResetTokenAsync();
        var token2 = await _tokenService.GeneratePasswordResetTokenAsync();

        // Assert
        token1.Should().NotBe(token2);
    }

    [Fact]
    public void GetPrincipalFromExpiredToken_Should_Return_Claims_For_Valid_Token()
    {
        // Arrange
        var guid = Guid.Parse("8E95B5E9-957D-493A-8B7F-6A9433962A19");
        var roles = new List<UserRole>() {
            new UserRole() { Role = new Role() { Name = "User" } },
            new UserRole() { Role = new Role() { Name = "Admin" } },
            new UserRole() { Role = new Role() { Name = "Manager" } }
        };
        var claims = new List<UserClaim>() {
            new UserClaim() { ClaimType = "department", ClaimValue = "IT" },
            new UserClaim() { ClaimType = "location", ClaimValue = "Jakarta" }
        };
        var user = new User
        {
            Id = 42,
            RefId = guid,
            Email = "test@example.com",
            Name = "John Doe",
            UserRoles = roles,
            UserClaims = claims
        };

        var token = _tokenService.GenerateAccessToken(user);

        // Act
        var principal = _tokenService.GetPrincipalFromExpiredToken(token);

        // Assert
        principal.Should().NotBeNull();
        principal.Identity.Should().NotBeNull();
        principal.Identity!.IsAuthenticated.Should().BeTrue();

        var nameClaim = principal.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Name);
        nameClaim.Should().NotBeNull();
        nameClaim.Value.Should().Be("John Doe");
    }

    [Fact]
    public void GetPrincipalFromExpiredToken_Should_Return_Null_For_Invalid_Token()
    {
        // Arrange
        var invalidToken = "invalid.token.format";

        // Act
        var principal = _tokenService.GetPrincipalFromExpiredToken(invalidToken);

        // Assert
        principal.Should().BeNull();
    }

    [Fact]
    public void GenerateAccessToken_Should_Throw_Exception_When_SecretKey_Not_Configured()
    {
        // Arrange
        var mockEmptyConfig = new JwtSettings();
        //mockEmptyConfig.SecretKey = "YourSuperSecretKeyThatIsAtLeast32CharactersLongForHS256Algorithm";
        mockEmptyConfig.Issuer = "AppleMusicAPI";
        mockEmptyConfig.Audience = "AppleMusicAPI";
        mockEmptyConfig.AccessTokenExpirationMinutes = 10;

        var emptyTokenService = new TokenService(mockEmptyConfig, _mockLogger.Object);

        var user = new User { Id = 1, Email = "test@example.com", Name = "Test" };
        var roles = new List<string> { "User" };
        var claims = new List<Claim>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            emptyTokenService.GenerateAccessToken(user));
    }
}