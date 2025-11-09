using FluentAssertions;
using MyApp.Application.Services;
using Xunit;

namespace MyApp.Application.Tests.Services;

public class PasswordServiceTests
{
    private readonly PasswordService _passwordService;

    public PasswordServiceTests()
    {
        _passwordService = new PasswordService();
    }

    [Theory]
    [InlineData("Password123!", true)]
    [InlineData("StrongP@ssw0rd", true)]
    [InlineData("MySecurePass#2024", true)]
    [InlineData("weak", false)]
    [InlineData("12345678", false)]
    [InlineData("password", false)]
    [InlineData("PASSWORD", false)]
    [InlineData("Pass12", false)] // Too short
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsPasswordStrong_Should_Validate_Password_Strength(string? password, bool isStrong)
    {
        // Act
        var result = _passwordService.IsPasswordStrong(password);

        // Assert
        result.Should().Be(isStrong);
    }

    [Fact]
    public void HashPassword_Should_Create_Hash()
    {
        // Arrange
        var password = "MySecurePassword123!";

        // Act
        var hashedPassword = _passwordService.HashPassword(password);

        // Assert
        hashedPassword.Should().NotBe(password);
        hashedPassword.Should().NotBeEmpty();
        hashedPassword.Should().StartWith("$2"); // BCrypt hash format
    }

    [Fact]
    public void VerifyPassword_Should_Return_True_For_Correct_Password()
    {
        // Arrange
        var password = "MySecurePassword123!";
        var hashedPassword = _passwordService.HashPassword(password);

        // Act
        var isVerified = _passwordService.VerifyPassword(password, hashedPassword);

        // Assert
        isVerified.Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_Should_Return_False_For_Incorrect_Password()
    {
        // Arrange
        var password = "MySecurePassword123!";
        var wrongPassword = "WrongPassword123!";
        var hashedPassword = _passwordService.HashPassword(password);

        // Act
        var isVerified = _passwordService.VerifyPassword(wrongPassword, hashedPassword);

        // Assert
        isVerified.Should().BeFalse();
    }

    [Fact]
    public void HashPassword_Should_Generate_Different_Hashes_For_Same_Password()
    {
        // Arrange
        var password = "MySecurePassword123!";

        // Act
        var hash1 = _passwordService.HashPassword(password);
        var hash2 = _passwordService.HashPassword(password);

        // Assert
        hash1.Should().NotBe(hash2);
        // Both should verify the original password
        _passwordService.VerifyPassword(password, hash1).Should().BeTrue();
        _passwordService.VerifyPassword(password, hash2).Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_Should_Handle_Invalid_Hash_Gracefully()
    {
        // Arrange
        var password = "MySecurePassword123!";
        var invalidHash = "invalid_hash";

        // Act
        var isVerified = _passwordService.VerifyPassword(password, invalidHash);

        // Assert
        isVerified.Should().BeFalse();
    }

    [Fact]
    public void VerifyPassword_Should_Handle_Null_Hash()
    {
        // Arrange
        var password = "MySecurePassword123!";

        // Act
        var isVerified = _passwordService.VerifyPassword(password, null!);

        // Assert
        isVerified.Should().BeFalse();
    }

    [Theory]
    [InlineData("")] // Empty string
    [InlineData("a")] // Single character
    [InlineData("12345678")] // No letters or special characters
    [InlineData("abcdefgh")] // No numbers or special characters
    [InlineData("ABCDEFGH")] // No lowercase letters, numbers or special characters
    [InlineData("Abcdefgh")] // No numbers or special characters
    [InlineData("Abcdefg1")] // No special characters
    [InlineData("Abcdefg!")] // No numbers
    public void IsPasswordStrong_Should_Return_False_For_Weak_Passwords(string password)
    {
        // Act
        var result = _passwordService.IsPasswordStrong(password);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("StrongPass1!")] // Exactly meets minimum requirements
    [InlineData("VeryStrongPassword123!@#")] // Complex password
    [InlineData("P@ssw0rd")] // Minimum length but meets all criteria
    public void IsPasswordStrong_Should_Return_True_For_Strong_Passwords(string password)
    {
        // Act
        var result = _passwordService.IsPasswordStrong(password);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HashPassword_Should_Throw_Exception_For_Null_Password()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _passwordService.HashPassword(null!));
    }

    [Fact]
    public void HashPassword_Should_Handle_Empty_Password()
    {
        // Act
        var hashedPassword = _passwordService.HashPassword("");

        // Assert
        hashedPassword.Should().NotBe("");
        hashedPassword.Should().NotBeEmpty();
        hashedPassword.Should().StartWith("$2");
    }

    [Fact]
    public void VerifyPassword_Should_Handle_Empty_Password()
    {
        // Arrange
        var hashedPassword = _passwordService.HashPassword("");

        // Act
        var isVerified = _passwordService.VerifyPassword("", hashedPassword);

        // Assert
        isVerified.Should().BeTrue();
    }
}