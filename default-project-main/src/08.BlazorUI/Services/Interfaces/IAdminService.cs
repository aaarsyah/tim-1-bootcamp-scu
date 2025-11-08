using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public interface IAdminService
{
    Task<List<CourseDto>> GetAllCourseAsync();
    Task<CourseDto?> CreateCourseAsync(AuthenticationHeaderValue authorization, CreateCourseRequestDto request);
    Task<CourseDto?> UpdateCourseAsync(AuthenticationHeaderValue authorization, int id, UpdateCourseRequestDto request);
    Task<bool> DeleteCourseAsync(AuthenticationHeaderValue authorization, int id);
    Task<List<CategoryDto>> GetAllCategoryAsync();
    Task<CategoryDto?> CreateCategoryAsync(AuthenticationHeaderValue authorization, CreateCategoryRequestDto request);
    Task<CategoryDto?> UpdateCategoryAsync(AuthenticationHeaderValue authorization, int id, UpdateCategoryRequestDto request);
    Task<bool> DeleteCategoryAsync(AuthenticationHeaderValue authorization, int id);
    Task<PaymentMethodDto?> CreatePaymentMethodAsync(AuthenticationHeaderValue authorization, CreatePaymentMethodRequestDto request);
    Task<PaymentMethodDto?> UpdatePaymentMethodAsync(AuthenticationHeaderValue authorization, int id, UpdatePaymentMethodRequestDto request);
    Task<bool> DeletePaymentMethodAsync(AuthenticationHeaderValue authorization, int id);
}