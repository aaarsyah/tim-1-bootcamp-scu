using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public interface IUserService
    {
        Task<List<UserItem>> GetUserAsync();
        Task<UserItem> CreateUserAsync(UserItem user);
        Task<UserItem> UpdateUserAsync(UserItem user);
        Task<bool> DeleteUserAsync(int id);
    }
}