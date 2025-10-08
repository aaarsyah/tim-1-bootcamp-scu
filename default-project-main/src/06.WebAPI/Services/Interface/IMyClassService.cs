using MyApp.WebAPI.DTOs;

namespace MyApp.WebAPI.Services
{
    public interface IMyClassService
    {
        Task<IEnumerable<MyClassDto>> GetAllMyClassAsync();
     
        Task<MyClassDto?> GetMyClassByIdAsync(int id);
   
        Task<MyClassDto> CreateMyClassAsync(CreateMyClassDto createMyClassDto);
        
        Task<MyClassDto?> UpdateMyClassAsync(int id, UpdateMyClassDto updateMyClassDto);
        

        Task<bool> DeleteMyClassAsync(int id);
     
        Task<bool> MyClassExistsAsync(int id);
    }
}