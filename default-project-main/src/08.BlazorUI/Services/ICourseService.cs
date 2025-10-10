using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public interface ICourseService
    {
        Task<List<CourseItem>> GetCourseAsync();
        Task<CourseItem> CreateCourseAsync(CourseItem course);
        Task<CourseItem> UpdateCourseAsync(CourseItem course);
        Task<bool> DeleteCourseAsync(int id);
    }
}