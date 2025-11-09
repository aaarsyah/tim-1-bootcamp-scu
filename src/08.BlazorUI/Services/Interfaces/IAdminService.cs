using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public interface IAdminService
{
    Task<List<CourseDto>> GetAllCourseAsync();
    Task<CourseDto?> CreateCourseAsync(AuthenticationHeaderValue authorization, CreateCourseRequestDto request);
    Task<CourseDto?> UpdateCourseAsync(AuthenticationHeaderValue authorization, Guid refId, UpdateCourseRequestDto request);
    Task<bool> DeleteCourseAsync(AuthenticationHeaderValue authorization, Guid refId);
    Task<List<CategoryDto>> GetAllCategoryAsync();
    Task<CategoryDto?> CreateCategoryAsync(AuthenticationHeaderValue authorization, CreateCategoryRequestDto request);
    Task<CategoryDto?> UpdateCategoryAsync(AuthenticationHeaderValue authorization, Guid refId, UpdateCategoryRequestDto request);
    Task<bool> DeleteCategoryAsync(AuthenticationHeaderValue authorization, Guid refId);
    Task<PaymentMethodDto?> CreatePaymentMethodAsync(AuthenticationHeaderValue authorization, CreatePaymentMethodRequestDto request);
    Task<PaymentMethodDto?> UpdatePaymentMethodAsync(AuthenticationHeaderValue authorization, Guid refId, UpdatePaymentMethodRequestDto request);
    Task<bool> DeletePaymentMethodAsync(AuthenticationHeaderValue authorization, Guid refId);
}