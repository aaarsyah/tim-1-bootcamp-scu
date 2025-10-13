using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models.Entities;

namespace MyApp.WebAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentService(AppleMusicDbContext context, IMapper mapper, ILogger<PaymentService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

   
        public async Task<IEnumerable<PaymentDto>> GetAllPaymentAsync()
        {
            var payments = await _context.PaymentMethods.ToListAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

   
        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.PaymentMethods
                .FirstOrDefaultAsync(c => c.Id == id);
            if (payment == null)
            {
                throw new NotFoundException($"PaymentMethod Id {id} not found");
            }
            return _mapper.Map<PaymentDto>(payment);
        }

      
        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var payment = _mapper.Map<PaymentMethod>(createPaymentDto);
            
            _context.PaymentMethods.Add(payment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Payment created: {PaymentName} with ID: {PaymentId}", 
                payment.Name, payment.Id);

            return _mapper.Map<PaymentDto>(payment);
        }

     
        public async Task<PaymentDto?> UpdatePaymentAsync(int id, UpdatePaymentDto updatePaymentDto)
        {
            var payment = await _context.PaymentMethods
                .FindAsync(id);
            if (payment == null)
            {
                throw new NotFoundException($"PaymentMethod Id {id} not found");
            }

            _mapper.Map(updatePaymentDto, payment);
            payment.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Payment updated: {PaymentId}", id);

            return _mapper.Map<PaymentDto>(payment);
        }

      
        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _context.PaymentMethods
                .FindAsync(id);
            if (payment == null)
            {
                throw new NotFoundException($"PaymentMethod Id {id} not found");
            }

            _context.PaymentMethods.Remove(payment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Payment deleted: {PaymentId}", id);
            
            return true;
        }

     
        public async Task<bool> PaymentExistsAsync(int id)
        {
            return await _context.PaymentMethods.AnyAsync(c => c.Id == id);
        }
    }
}