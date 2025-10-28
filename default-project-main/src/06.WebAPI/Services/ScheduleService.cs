using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Models;
using MyApp.Shared.DTOs;
using MyApp.WebAPI.Models.Entities;

namespace MyApp.WebAPI.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ScheduleService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScheduleService(AppleMusicDbContext context, IMapper mapper, ILogger<ScheduleService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

   
        public async Task<IEnumerable<ScheduleDto>> GetAllScheduleAsync()
        {
            var schedule = await _context.Schedules
                .Include(s => s.Course)   // include Course supaya CourseName bisa diakses
                .ToListAsync();
            return _mapper.Map<IEnumerable<ScheduleDto>>(schedule);
        }

   
        public async Task<ScheduleDto> GetScheduleByIdAsync(int id)
        {
            var schedule = await _context.Schedules
                .Include(s => s.Course)   // include Course supaya CourseName bisa diakses
                .FirstOrDefaultAsync(s => s.Id == id);
            if (schedule == null)
            {
                throw new NotFoundException("Schedule Id", id);
            }
            return _mapper.Map<ScheduleDto>(schedule);
        }

      
        public async Task<ScheduleDto> CreateScheduleAsync(CreateScheduleDto createScheduleDto)
        {
            // Validate course exists
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == createScheduleDto.CourseId);
            if (!courseExists)
            {
                throw new ValidationException($"Course with ID {createScheduleDto.CourseId} does not exist");
            }

            var schedule = _mapper.Map<Schedule>(createScheduleDto);
            
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Schedule created with ID: {ScheduleId}", 
                schedule.Id);

            return _mapper.Map<ScheduleDto>(schedule);
        }


        public async Task<ScheduleDto> UpdateScheduleAsync(int id, UpdateScheduleDto updateScheduleDto)
        {
            var schedule = await _context.Schedules
                        .Include(p => p.Course)
                        .FirstOrDefaultAsync(p => p.Id == id); //update Schedule by ScheduleId

            if (schedule == null)
            {
                throw new NotFoundException("Schedule Id", id);
            }

            // Validate course exists if changed
            if (updateScheduleDto.CourseId != schedule.CourseId)
            {
                var courseExists = await _context.Courses.AnyAsync(c => c.Id == updateScheduleDto.CourseId);
                if (!courseExists)
                {
                    //Eror Handling
                    throw new ValidationException($"Course with ID {updateScheduleDto.CourseId} does not exist");
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
            var schedule = await _context.Schedules
                .FindAsync(id);
            if (schedule == null)
            {
                throw new NotFoundException("Schedule Id", id);
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Schedule deleted: {ScheduleId}", id);
            
            return true;
        }

     
        public async Task<bool> ScheduleExistsAsync(int id)
        {
            return await _context.Schedules.AnyAsync(c => c.Id == id);
        }
    }
}