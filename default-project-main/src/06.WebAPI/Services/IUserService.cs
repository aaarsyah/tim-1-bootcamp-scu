using MyApp.WebAPI.DTOs;

namespace MyApp.WebAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync();
     
        Task<UserDto?> GetUserByIdAsync(int id);
   
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        
        Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        

        Task<bool> DeleteUserAsync(int id);
     
        Task<bool> UserExistsAsync(int id);
    }
}