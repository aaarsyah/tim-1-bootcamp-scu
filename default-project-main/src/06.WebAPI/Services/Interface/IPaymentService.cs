using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models.Entities;


namespace MyApp.WebAPI.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentAsync();
     
        Task<PaymentDto?> GetPaymentByIdAsync(int id);
   
        Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto);
        
        Task<PaymentDto?> UpdatePaymentAsync(int id, UpdatePaymentDto updatePaymentDto);
        

        Task<bool> DeletePaymentAsync(int id);
     
        Task<bool> PaymentExistsAsync(int id);
    }
}