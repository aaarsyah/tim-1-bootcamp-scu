using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Models.Entities;

namespace MyApp.WebAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string email, string name, string confirmationLink);
        Task SendPasswordResetAsync(string email, string name, string resetLink);
        Task SendPasswordChangedNotificationAsync(string email, string name);
        Task SendWelcomeEmailAsync(string email, string name);
    }
}