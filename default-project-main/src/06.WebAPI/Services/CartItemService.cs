using AutoMapper;
using Azure.Core;
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
                    var user = await _context.Users
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
                    var paymentmethod = await _context.PaymentMethods
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
        /// <summary>
        /// Get all items in all users<br />
        /// Use case:<br />
        /// None<br />
        /// </summary>
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
        /// <summary>
        /// Tambankan course ke dalam cart user<br />
        /// Tujuan: Supaya user bisa membeli courses<br />
        /// <br />
        /// Langkah:<br />
        /// 1. Validasi apakah user ada<br />
        /// 2. Validasi apakah user aktif<br />
        /// 3. Validasi apakah schedule ada<br />
        /// 4. Tambahkan schedule ke cart user<br />
        /// <br />
        /// Returns: true
        /// Throws: NotFoundException, InsufficientBalanceException, BusinessLogicException
        /// </summary>
        public async Task<bool> AddCourseToCartAsync(int userId, int scheduleid)
        {
            // ===== STEP 1 =====
            var user = await _context.Users
                .Where(a => a.Id == userId)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ValidationException(
                    $"Invalid UserId {userId} ");
            }
            // ===== STEP 2 =====
            if (!user.IsActive)
            {
                throw new PermissionException(
                    $"UserId {user.Id} not active");
            }
            // ===== STEP 3 =====
            var schedule = await _context.Schedules
                .Where(a => a.Id == scheduleid)
                .FirstOrDefaultAsync();
            if (schedule == null)
            {
                throw new ValidationException(
                    $"ScheduleId {scheduleid} not found");
            }
            // ===== STEP 4 =====
            _context.CartItems.Add(new CartItem
            {
                UserId = user.Id,
                ScheduleId = schedule.Id
            });
            // ===== STEP 8 =====
            // All operations succeeded, make permanent
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Tambankan course ke dalam cart user<br />
        /// Tujuan: Supaya user bisa membeli courses<br />
        /// <br />
        /// Langkah:<br />
        /// 1. Validasi apakah user ada<br />
        /// 2. Validasi apakah user aktif<br />
        /// 3. Validasi apakah schedule ada di keranjang user<br />
        /// 4. Hapus schedule dari cart user<br />
        /// <br />
        /// Returns: Transaction details with unique ID
        /// Throws: NotFoundException, InsufficientBalanceException, BusinessLogicException
        /// </summary>
        public async Task<bool> RemoveCourseFromCartAsync(int userId, int cartItemId)
        {
            // ===== STEP 1 =====
            var user = await _context.Users
                .Where(a => a.Id == userId)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ValidationException(
                    $"Invalid UserId {userId} ");
            }
            // ===== STEP 2 =====
            if (!user.IsActive)
            {
                throw new PermissionException(
                    $"UserId {user.Id} not active");
            }
            // ===== STEP 3 =====
            var cartitem = await _context.CartItems
                .Where(a => a.Id == cartItemId && a.UserId == userId)
                .FirstOrDefaultAsync();
            if (cartitem == null)
            {
                throw new ValidationException(
                    $"cartItemId {cartItemId} not found");
            }
            // ===== STEP 4 =====
            _context.CartItems.Remove(cartitem);
            // ===== STEP 8 =====
            // All operations succeeded, make permanent
            await _context.SaveChangesAsync();
            return true;
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
