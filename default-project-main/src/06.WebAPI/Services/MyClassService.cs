using AutoMapper;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models.Entities;
using MyApp.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyApp.WebAPI.Services
{
    public class MyClassService : IMyClassService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<MyClassService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public MyClassService(AppleMusicDbContext context, IMapper mapper, ILogger<MyClassService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

   
        public async Task<IEnumerable<MyClassDto>> GetAllMyClassAsync()
        {
            var myclass = await _context.MyClasses.ToListAsync();
            //Query Response
                var myClasses = await _context.MyClasses
                                .Include(m => m.Schedule)
                                    .ThenInclude(s => s.Course)
                                        .ThenInclude(c => c.Category)
                                .ToListAsync();
            return _mapper.Map<IEnumerable<MyClassDto>>(myclass);
        }

   
        public async Task<MyClassDto?> GetMyClassByIdAsync(int id)
        {
            var myclass = await _context.MyClasses
                        .Include(m => m.Schedule)
                            .ThenInclude(s => s.Course)
                                .ThenInclude(c => c.Category)
                        .FirstOrDefaultAsync(c => c.Id == id); // filter di database

            return myclass != null ? _mapper.Map<MyClassDto>(myclass) : null;
        }

      
        public async Task<MyClassDto> CreateMyClassAsync(CreateMyClassDto createMyClassDto)
        {
             // Validate userId exists
            var userExists = await _context.Users.AnyAsync(c => c.Id == createMyClassDto.UserId);
            if (!userExists)
            {
                //Eror Handling
                throw new ArgumentException($"User with ID {createMyClassDto.UserId} does not exist");
            }

            // Validate ScheduleId exists       
            var scheduleExists = await _context.Schedules.AnyAsync(s => s.Id == createMyClassDto.ScheduleId);
            if (!scheduleExists)
            {
                throw new ArgumentException($"Schedule with ID {createMyClassDto.ScheduleId} does not exist");
            }

            var myclass = _mapper.Map<MyClass>(createMyClassDto);
            
            _context.MyClasses.Add(myclass);
            await _context.SaveChangesAsync();

            //Reload
            if (createMyClassDto.UserId != myclass.UserId)
            {
                await _context.Entry(myclass).Reference(p => p.User).LoadAsync();
            }

            if (createMyClassDto.ScheduleId != myclass.ScheduleId)
            {
                await _context.Entry(myclass).Reference(p => p.Schedule).LoadAsync();
            }

            
            _logger.LogInformation("MyClass created ID: {MyClassId}", 
                myclass.Id);

            return _mapper.Map<MyClassDto>(myclass);
        }

     
        public async Task<MyClassDto?> UpdateMyClassAsync(int id, UpdateMyClassDto updateMyClassDto)
        {
            var myclass = await _context.MyClasses.FindAsync(id);
            if (myclass == null) return null;

            // Validate user exists if changed
            if (updateMyClassDto.UserId != myclass.UserId)
            {
                var userExists = await _context.Users.AnyAsync(c => c.Id == updateMyClassDto.UserId);
                if (!userExists)
                {
                    //Eror Handling
                    throw new ArgumentException($"User with ID {updateMyClassDto.UserId} does not exist");
                }
            }

            // Validate ScheduleId exists       
            var scheduleExists = await _context.Schedules.AnyAsync(s => s.Id == updateMyClassDto.ScheduleId);
            if (!scheduleExists)
            {
                throw new ArgumentException($"Schedule with ID {updateMyClassDto.ScheduleId} does not exist");
            }
            
            await _context.SaveChangesAsync();

            // Reload user if changed
            if (updateMyClassDto.UserId != myclass.UserId)
            {
                await _context.Entry(myclass).Reference(p => p.User).LoadAsync();
            }

            if (updateMyClassDto.ScheduleId != myclass.ScheduleId)
            {
                await _context.Entry(myclass).Reference(p => p.Schedule).LoadAsync();
            }
            
            _logger.LogInformation("MyClass updated: {MyClassId}", id);

            return _mapper.Map<MyClassDto>(myclass);
        }

      
        public async Task<bool> DeleteMyClassAsync(int id)
        {
            var myclass = await _context.MyClasses.FindAsync(id);
            if (myclass == null) return false;

            _context.MyClasses.Remove(myclass);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("MyClass deleted: {MyClassId}", id);
            
            return true;
        }

     
        public async Task<bool> MyClassExistsAsync(int id)
        {
            return await _context.MyClasses.AnyAsync(c => c.Id == id);
        }
    }
}