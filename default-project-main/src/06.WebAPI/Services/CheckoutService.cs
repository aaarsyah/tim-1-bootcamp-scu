using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Transaction Service Implementation
    /// Purpose: Handle all money transfer operations with database transactions
    /// 
    /// Key Features:
    /// 1. ACID Transactions - All or nothing operations
    /// 2. Validation - Business rules enforcement
    /// 3. Audit Trail - Automatic logging via DbContext
    /// 4. Error Handling - Custom exceptions for specific scenarios
    /// </summary>
    public class CheckoutService : ICheckoutService
    {
        private readonly AppleMusicDbContext _context;
        private readonly ILogger<CheckoutService> _logger;

        public CheckoutService(
            AppleMusicDbContext context,
            ILogger<CheckoutService> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Checkout barang yang dibeli pengguna
        /// 
        /// TRANSACTION FLOW:
        /// 1. BEGIN database transaction
        /// 2. Validasi apakah user ada
        /// 3. Validasi apakah user aktif
        /// 4. Validasi apakah semua barang yang dicheckout termasuk dalam keranjang pembeli
        /// 5. Validate apakah payment method ada dan tersedia
        /// 6. Hapus barang yang dicheckout dari keranjang
        /// 7. Buat invoice beserta detilnya
        /// 8. COMMIT transaction
        /// 
        /// If ANY step fails -> ROLLBACK (nothing saved)
        /// </summary>
        public async Task<CheckoutResponseDto> CheckoutItemsAsync(CheckoutRequestDto request)
        {
            _logger.LogInformation(
                "Checking out {ItemCartIds.Count} items for {UserId}",
                request.ItemCartIds.Count,
                request.UserId);
            // ===== VALIDATION: Cart item is not empty =====
            // Business Rule: Cannot check out empty cart
            if (request.ItemCartIds.Count == 0)
            {
                throw new InvalidOperationException("Cannot check out empty cart!");
            }
            // ===== USE EXECUTION STRATEGY WITH RETRY =====
            // Purpose: Make transaction compatible with retry logic
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                // ===== STEP 1 =====
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // ===== STEP 2 =====
                    var user = await _context.Users
                        .Where(a => a.Id == request.UserId)
                        .FirstOrDefaultAsync();
                    if (user == null)
                    {
                        throw new Exception(
                            $"UserId {request.UserId} not found");
                    }
                    // ===== STEP 3 =====
                    if (!user.IsActive)
                    {
                        throw new Exception(
                            $"UserId {user.Id} not active");
                    }
                    // ===== STEP 4 =====
                    List<CartItem> items = new List<CartItem>();
                    foreach (var itemcartid in request.ItemCartIds)
                    {
                        var item = await _context.CartItems
                            .Where(a => a.UserId == user.Id && a.ScheduleId == itemcartid)
                            .FirstOrDefaultAsync();
                        if (item == null)
                        {
                            throw new Exception(
                                $"ScheduleId {itemcartid} not found");
                        }
                        items.Add(item);
                        
                    }
                    // ===== STEP 5 =====
                    var paymentmethod = await _context.PaymentMethods
                        .Where(a => a.Id == request.PaymentMethodId)
                        .FirstOrDefaultAsync();
                    if (paymentmethod == null)
                    {
                        throw new Exception(
                            $"PaymentMethodId {request.PaymentMethodId} not found");
                    }
                    // ===== STEP 6 =====
                    foreach (var item in items)
                    {
                        _context.CartItems.Remove(item);
                    }
                    // ===== STEP 7 =====
                    var invoice = new Invoice
                    {
                        CreatedAt = DateTime.UtcNow,
                        UserId = user.Id,
                        PaymentMethodId = paymentmethod.Id,
                    };
                    _context.Invoices.Add(invoice);
                    await _context.SaveChangesAsync(); // Save transaction record to generate Id
                    foreach (CartItem item in items)
                    {
                        _context.InvoiceDetails.Add(new InvoiceDetail
                        {
                            InvoiceId = invoice.Id, // Id generated from saving transaction
                            ScheduleId = item.ScheduleId
                        });
                    }

                    await _context.SaveChangesAsync();

                    // ===== STEP 8 =====
                    // All operations succeeded, make permanent
                    await transaction.CommitAsync();

                    _logger.LogInformation(
                        "Checkout successful. Invoice ID: {invoiceId}",
                        invoice.Id);

                    // ===== RETURN RESPONSE =====
                    return new CheckoutResponseDto
                    {
                        InvoiceId = invoice.Id,
                        //Status = "Completed", // TODO: Tambah column Status di Invoice?
                        CreatedAt = DateTime.UtcNow
                    };
                }
                catch (Exception)
                {
                    throw; // Re-throw to be handled by middleware
                }
            });
        }
    }
}
