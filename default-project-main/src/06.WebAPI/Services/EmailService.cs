using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace MyApp.WebAPI.Services
{

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
            //_smtpUser = _configuration["EmailSettings:SmtpUser"] ?? "addindaarsyah@gmail.com";
            _smtpUser = string.Empty; // di-set empty untuk kepentingan demo
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
                <body style='font-family: Arial, sans-serif;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #333;'>Hi {name},</h2>
                        <p>Thank you for registering with AppleMusic!</p>
                        <p>Please click the button below to confirm your email address:</p>
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{confirmationLink}' style='background-color: #4CAF50; color: white; padding: 14px 28px; text-decoration: none; border-radius: 4px; display: inline-block;'>
                                Confirm Email
                            </a>
                        </div>
                        <p style='color: #666; font-size: 12px;'>If the button doesn't work, copy and paste this link into your browser:</p>
                        <p style='color: #666; font-size: 12px; word-break: break-all;'>{confirmationLink}</p>
                        <hr style='margin: 30px 0; border: none; border-top: 1px solid #eee;'>
                        <p style='color: #999; font-size: 12px;'>If you didn't create an account, you can safely ignore this email.</p>
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
                <body style='font-family: Arial, sans-serif;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #333;'>Hi {name},</h2>
                        <p>We received a request to reset your password for your AppleMusic account.</p>
                        <p>Click the button below to reset your password:</p>
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{resetLink}' style='background-color: #2196F3; color: white; padding: 14px 28px; text-decoration: none; border-radius: 4px; display: inline-block;'>
                                Reset Password
                            </a>
                        </div>
                        <p style='color: #ff6b6b; font-weight: bold;'>⚠️ This link will expire in 1 hour.</p>
                        <p style='color: #666; font-size: 12px;'>If the button doesn't work, copy and paste this link into your browser:</p>
                        <p style='color: #666; font-size: 12px; word-break: break-all;'>{resetLink}</p>
                        <hr style='margin: 30px 0; border: none; border-top: 1px solid #eee;'>
                        <p style='color: #999; font-size: 12px;'>If you didn't request a password reset, please ignore this email or contact support if you have concerns.</p>
                        <p style='color: #999; font-size: 12px;'>For security reasons, never share this link with anyone.</p>
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
                <body style='font-family: Arial, sans-serif;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #333;'>Hi {name},</h2>
                        <p>Your password has been changed successfully.</p>
                        <p>If you made this change, you can safely ignore this email.</p>
                        <div style='background-color: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0;'>
                            <p style='margin: 0; color: #856404;'>
                                <strong>⚠️ Security Alert</strong><br>
                                If you didn't change your password, please contact our support team immediately.
                            </p>
                        </div>
                        <p style='color: #666; font-size: 12px;'>This is an automated security notification.</p>
                    </div>
                </body>
                </html>";

            await SendEmailAsync(email, name, subject, htmlBody);
        }

        public async Task SendWelcomeEmailAsync(string email, string name)
        {
            var subject = "Welcome to AppleMusic!";
            var htmlBody = $@"
                <html>
                <body style='font-family: Arial, sans-serif;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #333;'>Welcome {name}! 🎉</h2>
                        <p>Thank you for joining AppleMusic!</p>
                        <p>We're excited to have you on board. Here are a few things you can do:</p>
                        <ul style='line-height: 1.8;'>
                            <li>Complete your profile</li>
                            <li>Explore our features</li>
                            <li>Connect with other users</li>
                        </ul>
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='https://localhost:7069' style='background-color: #4CAF50; color: white; padding: 14px 28px; text-decoration: none; border-radius: 4px; display: inline-block;'>
                                Get Started
                            </a>
                        </div>
                        <hr style='margin: 30px 0; border: none; border-top: 1px solid #eee;'>
                        <p style='color: #999; font-size: 12px;'>Need help? Contact our support team at support@myapp.com</p>
                    </div>
                </body>
                </html>";

            await SendEmailAsync(email, name, subject, htmlBody);
        }
    }
}
