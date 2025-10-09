using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models.Entities;

namespace MyApp.WebAPI.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> GetAllScheduleAsync();
     
        Task<ScheduleDto?> GetScheduleByIdAsync(int id);
   
        Task<ScheduleDto> CreateScheduleAsync(CreateScheduleDto createScheduleDto);
        
        Task<ScheduleDto?> UpdateScheduleAsync(int id, UpdateScheduleDto updateScheduleDto);
        

        Task<bool> DeleteScheduleAsync(int id);
     
        Task<bool> ScheduleExistsAsync(int id);
    }
}