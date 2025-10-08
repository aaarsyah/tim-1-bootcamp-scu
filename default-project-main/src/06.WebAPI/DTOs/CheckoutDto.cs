using System.ComponentModel.DataAnnotations;

namespace MyApp.WebAPI.DTOs
{
    /// <summary>
    /// Transfer Request DTO
    /// Purpose: Receive money transfer requests from API clients
    /// 
    /// Why use DTOs?
    /// 1. Decouple API contract from database entities
    /// 2. Control what data is exposed/accepted
    /// 3. Add validation rules specific to API
    /// 4. Prevent over-posting attacks
    /// </summary>
    public class CheckoutRequestDto
    {
        [Required(ErrorMessage = "UserId is required")]
        public int? UserId { get; set; }
        [Required(ErrorMessage = "ItemCartIds is required")]
        public List<int> ItemCartIds { get; set; } = null!;
        [Required(ErrorMessage = "PaymentMethodId is required")]
        public int PaymentMethodId { get; set; }
    }
    public class CheckoutResponseDto
    {
        public int InvoiceId { get; set; }
        /// <summary>
        /// When transaction was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When transaction was processed (completed or failed)
        /// Null if still pending
        /// </summary>
        public DateTime? ProcessedAt { get; set; }
    }
}
