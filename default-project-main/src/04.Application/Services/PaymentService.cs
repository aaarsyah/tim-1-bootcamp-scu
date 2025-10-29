using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using MyApp.Base.Exceptions;
using MyApp.Shared.DTOs;
using MyApp.Domain.Models;
using Microsoft.Extensions.Logging;

namespace MyApp.WebAPI.Services;

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
        var payments = await _context.PaymentMethods
            .ToListAsync();
        return _mapper.Map<IEnumerable<PaymentDto>>(payments);
    }


    public async Task<PaymentDto> GetPaymentByIdAsync(int id)
    {
        var payment = await _context.PaymentMethods
            .FirstOrDefaultAsync(c => c.Id == id);
        if (payment == null)
        {
            throw new NotFoundException("PaymentMethod Id", id);
        }
        return _mapper.Map<PaymentDto>(payment);
    }

  
    public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
    {
        var payment = _mapper.Map<PaymentMethod>(createPaymentDto);

        // === Upload Image (jika ImageUrl berisi base64 dari client) ===
        if (!string.IsNullOrWhiteSpace(createPaymentDto.LogoUrl) &&
            createPaymentDto.LogoUrl.StartsWith("data:image"))
        {
            // Contoh: data:image/png;base64,AAAA...
            var base64Data = createPaymentDto.LogoUrl.Substring(createPaymentDto.LogoUrl.IndexOf(",") + 1);
            var bytes = Convert.FromBase64String(base64Data);

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}.png";
            var filePath = Path.Combine(uploadsFolder, fileName);
            await File.WriteAllBytesAsync(filePath, bytes);

            // Simpan URL publik ke database
            payment.LogoUrl = $"/img/{fileName}";
        }

        _context.PaymentMethods.Add(payment);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Payment created: {PaymentName} with ID: {PaymentId}", 
            payment.Name, payment.Id);

        return _mapper.Map<PaymentDto>(payment);
    }

 
    public async Task<PaymentDto> UpdatePaymentAsync(int id, UpdatePaymentDto updatePaymentDto)
    {
        var payment = await _context.PaymentMethods
            .FindAsync(id);
        if (payment == null)
        {
            throw new NotFoundException("PaymentMethod Id", id);
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
            throw new NotFoundException("PaymentMethod Id", id);
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