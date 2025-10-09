using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Exceptions;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models.Entities;
using System.Globalization;
using System.Numerics;

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
    public class CartItemService : ICartItemService
    {
        private readonly AppleMusicDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CartItemService> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        public CartItemService(
            AppleMusicDbContext context,
            IMapper mapper,
            ILogger<CartItemService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Checkout barang yang dibeli pengguna<br />
        /// <br />
        /// TRANSACTION FLOW:<br />
        /// 1. BEGIN database transaction<br />
        /// 2. Validasi apakah user ada<br />
        /// 3. Validasi apakah user aktif<br />
        /// 4. Validasi apakah semua barang yang dicheckout termasuk dalam keranjang pembeli<br />
        /// 5. Validate apakah payment method ada dan tersedia<br />
        /// 6. Hapus barang yang dicheckout dari keranjang<br />
        /// 7. Buat invoice beserta detilnya<br />
        /// 8. COMMIT transaction<br />
        /// <br />
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
                throw new ValidationException("Checked out items cannot be empty");
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
                    var user = await _context.User
                        .Where(a => a.Id == request.UserId)
                        .FirstOrDefaultAsync();
                    if (user == null)
                    {
                        throw new ValidationException(
                            $"Invalid UserId {request.UserId} ");
                    }
                    // ===== STEP 3 =====
                    if (!user.IsActive)
                    {
                        throw new PermissionException(
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
                            throw new ValidationException(
                                $"ScheduleId {itemcartid} not found"); // TODO: Support for multiple items? Or just return invalid message?
                        }
                        items.Add(item);
                    }
                    // ===== STEP 5 =====
                    var paymentmethod = await _context.Payment
                        .Where(a => a.Id == request.PaymentMethodId)
                        .FirstOrDefaultAsync();
                    if (paymentmethod == null)
                    {
                        throw new ValidationException(
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
                        RefCode = GenerateInvoiceId(),
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
                    // ===== ROLLBACK ON ERROR =====
                    // Any exception -> undo all changes
                    await transaction.RollbackAsync();
                    _logger.LogError("Checkout successful, transaction rolled back");
                    throw; // Re-throw to be handled by middleware
                }
            });
        }

        public async Task<IEnumerable<CartItemResponseDto>> GetAllCartItemAsync()
        {
            var categories = await _context.CartItems
                .Include(c => c.Schedule)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CartItemResponseDto>>(categories);
        }

        public async Task<IEnumerable<CartItemResponseDto>> GetCartItemByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
        public async Task<CartItemResponseDto> AddCartItemAsync(int userId, int scheduleid)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Generate unique transaction ID<br />
        /// Format: TXN{yyyyMMddHHmmss}{random 6 digits}<br />
        /// Example: TXN20251005123059012345
        /// </summary>
        private string GenerateInvoiceId()
        {
            return $"TXN{DateTime.UtcNow:yyyyMMddHHmmss}{Random.Shared.Next(0, 999999):D6}";
        }
        //private string GenerateInvoiceId_GUID()
        //{
        //    var a = new BigInteger(Guid.NewGuid().ToByteArray().Concat(new byte[] { 0 }).ToArray()).ToString("D", CultureInfo.InvariantCulture);
        //    return $"TXN{a}";
        //}
    }
}
