namespace MyApp.WebAPI.Services;

public interface IEmailService
{
    Task SendEmailConfirmationAsync(string email, string name, string confirmationLink);
    Task SendPasswordResetAsync(string email, string name, string resetLink);
    Task SendPasswordChangedNotificationAsync(string email, string name);
}