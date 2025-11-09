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

        //foreach (char c in password)
        //{
        //    if (char.IsUpper(c)) hasUpper = true;
        //    else if (char.IsLower(c)) hasLower = true;
        //    else if (char.IsDigit(c)) hasDigit = true;
        //    else if (!char.IsLetterOrDigit(c)) hasSpecial = true;
        //}

        if (!Regex.IsMatch(password, @"[A-Z]")) return false; //hasUpper
        if (!Regex.IsMatch(password, @"[a-z]")) return false; //hasLower
        if (!Regex.IsMatch(password, @"\d")) return false; //hasDigit
        if (!Regex.IsMatch(password, @"[ -/:-@[-`{-~]")) return false; //hasSpecial

        //return hasUpper && hasLower && hasDigit && hasSpecial;
        return true;
    }
}
