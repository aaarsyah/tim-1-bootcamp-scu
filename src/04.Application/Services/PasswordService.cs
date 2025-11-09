using System.Text.RegularExpressions;

namespace MyApp.Application.Services;

// Password Service Implementation menggunakan BCrypt
public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password); //salt is auto-generated and included in the password string
    }

    public bool VerifyPassword(string password, string? hashedPassword)
    {
        if (hashedPassword == null) return false;

        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        catch
        {
            return false;
        }
    }

    public bool IsPasswordStrong(string? password)
    {
        // Are you joking me? A null password?
        if (password == null) return false;

        // Minimum 8 characters, at least one uppercase, one lowercase, one number, and one special character
        if (password.Length < 8) return false;

        if (!Regex.IsMatch(password, @"[A-Z]", RegexOptions.NonBacktracking)) return false; //hasUpper
        if (!Regex.IsMatch(password, @"[a-z]", RegexOptions.NonBacktracking)) return false; //hasLower
        if (!Regex.IsMatch(password, @"\d", RegexOptions.NonBacktracking)) return false; //hasDigit
        if (!Regex.IsMatch(password, @"[ -/:-@[-`{-~]", RegexOptions.NonBacktracking)) return false; //hasSpecial

        return true;
    }
}
