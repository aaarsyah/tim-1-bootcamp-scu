using AutoMapper;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models.Entities;
using MyApp.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyApp.WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserService(AppleMusicDbContext context, IMapper mapper, ILogger<UserService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

   
        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var user = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(user);
        }

   
        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(c => c.Id == id);

            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

      
        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("User created: {UserName} with ID: {UserId}", 
                user.Name, user.Id);

            return _mapper.Map<UserDto>(user);
        }

     
        public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            _mapper.Map(updateUserDto, user);
            user.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("User updated: {UserId}", id);

            return _mapper.Map<UserDto>(user);
        }

      
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("User deleted: {UserId}", id);
            
            return true;
        }

     
        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(c => c.Id == id);
        }
    }
}