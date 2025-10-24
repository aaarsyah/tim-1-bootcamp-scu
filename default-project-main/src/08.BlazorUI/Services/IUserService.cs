using MyApp.BlazorUI.Models;
using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync(AuthenticationHeaderValue authorization);
        Task<UserItem> CreateUserAsync(UserItem user);
        Task<UserItem> UpdateUserAsync(UserItem user);
        Task<bool> DeleteUserAsync(int id);
    }
}
