using AutoMapper;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly CourseDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentService(CourseDbContext context, IMapper mapper, ILogger<PaymentService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

   
        public async Task<IEnumerable<PaymentDto>> GetAllPaymentAsync()
        {
            var payments = await _context.Payment.ToListAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

   
        public async Task<PaymentDto?> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payment
            .FirstOrDefaultAsync(c => c.Id == id);

            return payment != null ? _mapper.Map<PaymentDto>(payment) : null;
        }

      
        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var payment = _mapper.Map<PaymentMethod>(createPaymentDto);
            
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Payment created: {PaymentName} with ID: {PaymentId}", 
                payment.Name, payment.Id);

            return _mapper.Map<PaymentDto>(payment);
        }

     
        public async Task<PaymentDto?> UpdatePaymentAsync(int id, UpdatePaymentDto updatePaymentDto)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null) return null;

            _mapper.Map(updatePaymentDto, payment);
            payment.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Payment updated: {PaymentId}", id);

            return _mapper.Map<PaymentDto>(payment);
        }

      
        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null) return false;

            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Payment deleted: {PaymentId}", id);
            
            return true;
        }

     
        public async Task<bool> PaymentExistsAsync(int id)
        {
            return await _context.Payment.AnyAsync(c => c.Id == id);
        }
    }
}