using MyApp.Shared.DTOs;

namespace MyApp.WebAPI.Services;
public interface IScheduleService
{
    Task<IEnumerable<ScheduleDto>> GetAllScheduleAsync();
    Task<ScheduleDto> GetScheduleByIdAsync(int id);
    Task<ScheduleDto> CreateScheduleAsync(CreateScheduleRequestDto createScheduleDto);
    Task<ScheduleDto> UpdateScheduleAsync(int id, UpdateScheduleRequestDto updateScheduleDto);
    Task<bool> DeleteScheduleAsync(int id);
    Task<bool> ScheduleExistsAsync(int id);
}