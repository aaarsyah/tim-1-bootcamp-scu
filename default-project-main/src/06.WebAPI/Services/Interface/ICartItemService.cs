using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Models;
using MyApp.WebAPI.Models.DTOs;
using MyApp.WebAPI.Models.Entities;

namespace MyApp.WebAPI.Services
{
    /// <summary>
    /// Transaction Service Interface
    /// Purpose: Define contract for transaction operations
    /// 
    /// Why interface?
    /// 1. Dependency Injection - Easier testing with mocks
    /// 2. Loose Coupling - Can swap implementations
    /// 3. Multiple Implementations - Different strategies for different scenarios
    /// 4. Clear Contract - Documents what service provides
    /// </summary>
    public interface ICartItemService
    {
        /// <summary>
        /// Process courses for checkout
        /// Purpose: Execute course checkout with full transaction support
        /// 
        /// What it does:
        /// 1. Validasi apakah user ada
        /// 2. Validasi apakah user aktif
        /// 3. Validasi apakah semua barang yang dicheckout termasuk dalam keranjang pembeli
        /// 4. Validate apakah payment method ada dan tersedia
        /// 5. Hapus barang yang dicheckout dari keranjang
        /// 6. Buat invoice
        /// 7. All in ONE database transaction (ACID)
        /// 
        /// Returns: Transaction details with unique ID
        /// Throws: NotFoundException, InsufficientBalanceException, BusinessLogicException
        /// </summary>
        Task<CheckoutResponseDto> CheckoutItemsAsync(CheckoutRequestDto request);
        ///
        Task<IEnumerable<CartItemResponseDto>> GetAllCartItemAsync();
        Task<IEnumerable<CartItemResponseDto>> GetCartItemByIdAsync(int userId);
        /// <summary>
        /// Add item to cart
        /// </summary>
        Task<CartItemResponseDto> AddCartItemAsync(int userId, int scheduleid);
    }
}