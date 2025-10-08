using AutoMapper;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyApp.WebAPI.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly CourseDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ScheduleService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScheduleService(CourseDbContext context, IMapper mapper, ILogger<ScheduleService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

   
        public async Task<IEnumerable<ScheduleDto>> GetAllScheduleAsync()
        {
            var schedule = await _context.Schedule
                        .Include(s => s.Course)   // include Course supaya CourseName bisa diakses
                        .ToListAsync();
            return _mapper.Map<IEnumerable<ScheduleDto>>(schedule);
        }

   
        public async Task<ScheduleDto?> GetScheduleByIdAsync(int id)
        {
            var schedule = await _context.Schedule
                        .Include(s => s.Course)   // include Course supaya CourseName bisa diakses
                        .FirstOrDefaultAsync(s => s.Id == id);

            return schedule != null ? _mapper.Map<ScheduleDto>(schedule) : null;
        }

      
        public async Task<ScheduleDto> CreateScheduleAsync(CreateScheduleDto createScheduleDto)
        {
            // Validate course exists
            var courseExists = await _context.Course.AnyAsync(c => c.Id == createScheduleDto.CourseId);
            if (!courseExists)
            {
                //Eror Handling
                throw new ArgumentException($"Course with ID {createScheduleDto.CourseId} does not exist");
            }

            var schedule = _mapper.Map<Schedule>(createScheduleDto);
            
            _context.Schedule.Add(schedule);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Schedule created with ID: {ScheduleId}", 
                schedule.Id);

            return _mapper.Map<ScheduleDto>(schedule);
        }


        public async Task<ScheduleDto?> UpdateScheduleAsync(int id, UpdateScheduleDto updateScheduleDto)
        {
            var schedule = await _context.Schedule
                        .Include(p => p.Course)
                        .FirstOrDefaultAsync(p => p.Id == id); //update Schedule by ScheduleId

            if (schedule == null) return null;

            // Validate course exists if changed
            if (updateScheduleDto.CourseId != schedule.CourseId)
            {
                var courseExists = await _context.Course.AnyAsync(c => c.Id == updateScheduleDto.CourseId);
                if (!courseExists)
                {
                    //Eror Handling
                    throw new ArgumentException($"Course with ID {updateScheduleDto.CourseId} does not exist");
                }
            }

            _mapper.Map(updateScheduleDto, schedule);
            schedule.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            // Reload course if changed
            if (updateScheduleDto.CourseId != schedule.CourseId)
            {
                await _context.Entry(schedule).Reference(p => p.Course).LoadAsync();
            }
            
            _logger.LogInformation("Schedule updated: {ScheduleId}", id);

            return _mapper.Map<ScheduleDto>(schedule);
        }

      
        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null) return false;

            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Schedule deleted: {ScheduleId}", id);
            
            return true;
        }

     
        public async Task<bool> ScheduleExistsAsync(int id)
        {
            return await _context.Schedule.AnyAsync(c => c.Id == id);
        }
    }
}