using FluentAssertions;
using MyApp.Domain.Models;
using Xunit;

namespace MyApp.Domain.Tests.Entities;

public class UserTests
{
    [Fact]
    public void User_Should_Initialize_With_Correct_Values()
    {
        // Arrange
        var email = "test@example.com";
        var passwordHash = "hashedPassword";
        var name = "John Doe";

        // Act
        var user = new User
        {
            Email = email,
            PasswordHash = passwordHash,
            Name = name
        };

        // Assert
        user.Email.Should().Be(email);
        user.PasswordHash.Should().Be(passwordHash);
        user.Name.Should().Be(name);
        user.FailedLoginAttempts.Should().Be(0); // Default value
        user.EmailConfirmed.Should().BeFalse(); // Default value
    }

    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("invalid-email", false)]
    [InlineData("", false)]
    [InlineData("@example.com", false)]
    [InlineData("test@", false)]
    [InlineData("test@.com", false)]
    public void User_Should_Validate_Email_Format(string email, bool isValid)
    {
        // Arrange & Act
        var isValidEmail = IsValidEmail(email);

        // Assert
        isValidEmail.Should().Be(isValid);
    }

    [Fact]
    public void User_Should_Track_Failed_Login_Attempts()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };

        // Act
        user.FailedLoginAttempts = 3;

        // Assert
        user.FailedLoginAttempts.Should().Be(3);
    }

    [Fact]
    public void User_Should_Reset_Failed_Login_Attempts()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        user.FailedLoginAttempts = 5;

        // Act
        user.FailedLoginAttempts = 0;
        user.LockoutEnd = null;

        // Assert
        user.FailedLoginAttempts.Should().Be(0);
        user.LockoutEnd.Should().BeNull();
    }

    [Fact]
    public void User_Should_Set_EmailConfirmed_Flag()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        user.EmailConfirmed.Should().BeFalse();

        // Act
        user.EmailConfirmed = true;

        // Assert
        user.EmailConfirmed.Should().BeTrue();
    }

    [Fact]
    public void User_Should_Set_Password_Reset_Token()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        var resetToken = Guid.NewGuid().ToString();
        var expiry = DateTime.UtcNow.AddHours(1);

        // Act
        user.PasswordResetToken = resetToken;
        user.PasswordResetTokenExpiry = expiry;

        // Assert
        user.PasswordResetToken.Should().Be(resetToken);
        user.PasswordResetTokenExpiry.Should().Be(expiry);
    }

    [Fact]
    public void User_Should_Set_Refresh_Token()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        var refreshToken = Guid.NewGuid().ToString();
        var expiry = DateTime.UtcNow.AddDays(7);

        // Act
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = expiry;

        // Assert
        user.RefreshToken.Should().Be(refreshToken);
        user.RefreshTokenExpiry.Should().Be(expiry);
    }

    [Fact]
    public void User_Should_Update_Last_Login()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        var originalLastLogin = user.LastLoginAt ?? DateTime.MinValue;

        // Act
        Thread.Sleep(10); // Small delay
        user.LastLoginAt = DateTime.UtcNow;

        // Assert
        user.LastLoginAt.Should().NotBeNull();
        user.LastLoginAt.Value.Should().BeAfter(originalLastLogin);
    }

    [Fact]
    public void User_Should_Handle_Lockout_Period()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        var lockoutEnd = DateTime.UtcNow.AddMinutes(15);

        // Act
        user.LockoutEnd = lockoutEnd;

        // Assert
        user.LockoutEnd.Should().Be(lockoutEnd);
        user.LockoutEnd.Value.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(15), TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void User_Should_Initialize_Collections()
    {
        // Arrange & Act
        var user = new User();

        // Assert
        user.UserRoles.Should().NotBeNull();
        user.UserClaims.Should().NotBeNull();
        user.UserRoles.Should().BeEmpty();
        user.UserClaims.Should().BeEmpty();
    }

    // Helper method for email validation
    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}