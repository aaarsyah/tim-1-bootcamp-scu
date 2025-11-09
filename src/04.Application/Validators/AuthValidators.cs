using FluentValidation;
using MyApp.Shared.DTOs;

namespace MyApp.Application.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
                .MaximumLength(64).WithMessage("Name cannot exceed 64 characters long");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(256).WithMessage("Email cannot exceed 256 characters long");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required")
                .MinimumLength(8).WithMessage("New Password must be at least 8 characters long")
                // Panjang maksimum password dibatasi oleh BCrypt, yang hanya dapat menghash password paling besar 72 byte
                .MaximumLength(72).WithMessage("New Password cannot exceed 72 characters long")
                .Matches(@"[A-Z]").WithMessage("New Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("New Password must contain at least one lowercase letter")
                .Matches(@"\d").WithMessage("New Password must contain at least one digit number")
                // special character yang dimaksud merujuk pada karakter yang terdaftar disini:
                // https://owasp.org/www-community/password-special-characters
                .Matches(@"[ -/:-@[-`{-~]").WithMessage("New Password must contain at least one special character");
            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Confirm New Password is required")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match");
        }
    }

    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequestDto>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.EmailConfirmationToken)
                .NotEmpty().WithMessage("Email Confirmation Token is required");
        }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }

    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequestDto>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("Access Token is required");
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh Token is required");
        }
    }

    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequestDto>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required")
                .MinimumLength(8).WithMessage("New Password must be at least 8 characters long")
                // Panjang maksimum password dibatasi oleh BCrypt, yang hanya dapat menghash password paling besar 72 byte
                .MaximumLength(72).WithMessage("New Password cannot exceed 72 characters long")
                .Matches(@"[A-Z]").WithMessage("New Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("New Password must contain at least one lowercase letter")
                .Matches(@"\d").WithMessage("New Password must contain at least one digit number")
                // special character yang dimaksud merujuk pada karakter yang terdaftar disini:
                // https://owasp.org/www-community/password-special-characters
                .Matches(@"[ -/:-@[-`{-~]").WithMessage("New Password must contain at least one special character");
            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Confirm New Password is required")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match");
        }
    }

    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequestDto>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }

    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequestDto>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.PasswordResetToken)
                .NotEmpty().WithMessage("Token is required");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required")
                .MinimumLength(8).WithMessage("New Password must be at least 8 characters long")
                // Panjang maksimum password dibatasi oleh BCrypt, yang hanya dapat menghash password paling besar 72 byte
                .MaximumLength(72).WithMessage("New Password cannot exceed 72 characters long")
                .Matches(@"[A-Z]").WithMessage("New Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("New Password must contain at least one lowercase letter")
                .Matches(@"\d").WithMessage("New Password must contain at least one digit number")
                // special character yang dimaksud merujuk pada karakter yang terdaftar disini:
                // https://owasp.org/www-community/password-special-characters
                .Matches(@"[ -/:-@[-`{-~]").WithMessage("New Password must contain at least one special character");
            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Confirm New Password is required")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match");
        }
    }
}