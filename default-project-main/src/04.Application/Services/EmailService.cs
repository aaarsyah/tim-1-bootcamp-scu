using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace MyApp.WebAPI.Services;


// Email Service (Email Confirmation) Implementation with SMTP (MailKit)
public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPassword;
    private readonly string _fromEmail;
    private readonly string _fromName;
    private readonly bool _enableSsl;
    public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        
        // Load email settings from configuration
        
        _smtpHost = _configuration["EmailSettings:SmtpHost"] ?? "smtp.gmail.com";
        _smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "465"); //587
        _smtpUser = _configuration["EmailSettings:SmtpUser"] ?? "addindaarsyah@gmail.com";
        // _smtpUser = string.Empty; // di-set empty untuk kepentingan demo
        _smtpPassword = _configuration["EmailSettings:SmtpPassword"] ?? "mjru kfkz ibks wfwz";
        _fromEmail = _configuration["EmailSettings:FromEmail"] ?? _smtpUser;
        _fromName = _configuration["EmailSettings:FromName"] ?? "AppleMusic Support";
        _enableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"] ?? "true");
    }

    private async Task SendEmailAsync(string toEmail, string toName, string subject, string htmlBody)
    {
        try
        {
            // Check if SMTP is configured
            if (string.IsNullOrEmpty(_smtpUser) || string.IsNullOrEmpty(_smtpPassword))
            {
                _logger.LogWarning("SMTP not configured. Email would be sent to: {Email}", toEmail);
                _logger.LogInformation("Subject: {Subject}", subject);
                _logger.LogInformation("Body: {Body}", htmlBody);
                return;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_fromName, _fromEmail));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            
            // Connect to SMTP server
            await client.ConnectAsync(_smtpHost, _smtpPort, _enableSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.None); //SecureSocketOptions.StartTls
            
            // Authenticate
            await client.AuthenticateAsync(_smtpUser, _smtpPassword);
            
            // Send email
            await client.SendAsync(message);
            
            // Disconnect
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email sent successfully to: {Email}", toEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to: {Email}", toEmail);
            throw;
        }
    }

    public async Task SendEmailConfirmationAsync(string email, string name, string confirmationLink)
    {
        var subject = "Confirm Your Email - AppleMusic";
        var htmlBody = $@"
                <html>
                <body style='font-family: Arial, sans-serif; background-color:#f9f9f9; margin:0; padding:0;'>
                    <div style='max-width:600px; margin:40px auto; background-color:#ffffff; border-radius:10px; box-shadow:0 4px 10px rgba(0,0,0,0.1); overflow:hidden;'>
                        <div style='background-color:#fa233b; padding:20px; text-align:center;'>
                            <h1 style='color:white; margin:0;'>AppleMusic</h1>
                        </div>
                        <div style='padding:30px;'>
                            <h2 style='color:#333;'>Hi {name},</h2>
                            <p>Thank you for registering with <strong>AppleMusic</strong>!</p>
                            <p>Please click the button below to confirm your email address:</p>
                            <div style='text-align:center; margin:30px 0;'>
                                <a href='{confirmationLink}' style='background-color:#fa233b; color:white; padding:14px 28px; border-radius:6px; text-decoration:none; display:inline-block; font-weight:bold;'>
                                    Confirm Email
                                </a>
                            </div>
                            <p style='color:#777; font-size:13px;'>If you didn’t create an account, please ignore this message.</p>
                        </div>
                        <div style='background-color:#f0f0f0; text-align:center; padding:15px; font-size:12px; color:#999;'>
                            &copy; 2025 AppleMusic. All rights reserved.
                        </div>
                    </div>
                </body>
                </html>";

        await SendEmailAsync(email, name, subject, htmlBody);
    }

    public async Task SendPasswordResetAsync(string email, string name, string resetLink)
    {
        var subject = "Reset Your Password - AppleMusic";
        var htmlBody = $@"
                <html>
                <body style='font-family: Arial, sans-serif; background-color:#f9f9f9; margin:0; padding:0;'>
                    <div style='max-width:600px; margin:40px auto; background-color:#ffffff; border-radius:10px; box-shadow:0 4px 10px rgba(0,0,0,0.1); overflow:hidden;'>
                        <div style='background-color:#fa233b; padding:20px; text-align:center;'>
                            <h1 style='color:white; margin:0;'>AppleMusic</h1>
                        </div>
                        <div style='padding:30px;'>
                            <h2 style='color:#333;'>Hi {name},</h2>
                            <p>We received a request to reset your password for your <strong>AppleMusic</strong> account.</p>
                            <p>Click the button below to reset your password:</p>
                            <div style='text-align:center; margin:30px 0;'>
                                <a href='{resetLink}' style='background-color:#007bff; color:white; padding:14px 28px; border-radius:6px; text-decoration:none; display:inline-block; font-weight:bold;'>
                                    Reset Password
                                </a>
                            </div>
                            <p style='color:#ff6b6b; font-weight:bold;'>⚠️ This link will expire in 1 hour.</p>
                            <p style='color:#777; font-size:13px;'>If you didn’t request this, please ignore this email or contact support.</p>
                        </div>
                        <div style='background-color:#f0f0f0; text-align:center; padding:15px; font-size:12px; color:#999;'>
                            &copy; 2025 AppleMusic. All rights reserved.
                        </div>
                    </div>
                </body>
                </html>";

        await SendEmailAsync(email, name, subject, htmlBody);
    }

    public async Task SendPasswordChangedNotificationAsync(string email, string name)
    {
        var subject = "Password Changed Successfully - AppleMusic";
        var htmlBody = $@"
                <html>
                <body style='font-family: Arial, sans-serif; background-color:#f9f9f9; margin:0; padding:0;'>
                    <div style='max-width:600px; margin:40px auto; background-color:#ffffff; border-radius:10px; box-shadow:0 4px 10px rgba(0,0,0,0.1); overflow:hidden;'>
                        <div style='background-color:#fa233b; padding:20px; text-align:center;'>
                            <h1 style='color:white; margin:0;'>AppleMusic</h1>
                        </div>
                        <div style='padding:30px;'>
                            <h2 style='color:#333;'>Hi {name},</h2>
                            <p>Your password has been changed successfully.</p>
                            <div style='background-color:#fff3cd; border-left:4px solid #ffc107; padding:15px; margin:20px 0;'>
                                <p style='margin:0; color:#856404;'>
                                    <strong>⚠️ Security Alert:</strong><br>
                                    If you didn’t change your password, please contact our support team immediately.
                                </p>
                            </div>
                            <p style='color:#777; font-size:13px;'>This is an automated notification. Please do not reply.</p>
                        </div>
                        <div style='background-color:#f0f0f0; text-align:center; padding:15px; font-size:12px; color:#999;'>
                            &copy; 2025 AppleMusic. All rights reserved.
                        </div>
                    </div>
                </body>
                </html>";
        await SendEmailAsync(email, name, subject, htmlBody);
    }
}
