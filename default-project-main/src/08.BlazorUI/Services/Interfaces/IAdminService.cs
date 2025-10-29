using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public interface IAdminService
{
    Task<List<CourseDto>> GetAllCourseAsync();
    Task<CourseDto?> CreateCourseAsync(AuthenticationHeaderValue authorization, CreateCourseDto request);
    Task<CourseDto?> UpdateCourseAsync(AuthenticationHeaderValue authorization, int id, UpdateCourseDto request);
    Task<bool> DeleteCourseAsync(AuthenticationHeaderValue authorization, int id);
    Task<List<CategoryDto>> GetAllCategoryAsync();
    Task<CategoryDto?> CreateCategoryAsync(AuthenticationHeaderValue authorization, CreateCategoryDto request);
    Task<CategoryDto?> UpdateCategoryAsync(AuthenticationHeaderValue authorization, int id, UpdateCategoryDto request);
    Task<bool> DeleteCategoryAsync(AuthenticationHeaderValue authorization, int id);
    Task<PaymentDto?> CreatePaymentMethodAsync(AuthenticationHeaderValue authorization, CreatePaymentDto payment);
    Task<PaymentDto?> UpdatePaymentMethodAsync(AuthenticationHeaderValue authorization, int id, UpdatePaymentDto payment);
    Task<bool> DeletePaymentMethodAsync(AuthenticationHeaderValue authorization, int id);
}