using MyApp.Shared.DTOs;

namespace MyApp.WebAPI.Services;

public interface IMyClassService
{
    Task<IEnumerable<MyClassDto>> GetMyClassesByUserIdAsync(int userId);
 
    Task<MyClassDto> GetMyClassByIdAsync(int id);

    Task<MyClassDto> CreateMyClassAsync(CreateMyClassDto createMyClassDto);
    
    Task<MyClassDto> UpdateMyClassAsync(int id, UpdateMyClassDto updateMyClassDto);
    
    Task<bool> DeleteMyClassAsync(int id);
 
    Task<bool> MyClassExistsAsync(int id);
}