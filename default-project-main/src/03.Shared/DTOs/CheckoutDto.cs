using System.ComponentModel.DataAnnotations;

namespace MyApp.Shared.DTOs;

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
    [Required(ErrorMessage = "ItemCartIds is required")]
    [MinLength(1, ErrorMessage = "ItemCartIds must contain at least one item")]
    public List<int> ItemCartIds { get; set; } = null!;
    [Required(ErrorMessage = "PaymentMethodId is required")]
    public int? PaymentMethodId { get; set; }
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

    public long TotalPrice { get; set; }

    public int TotalCourse { get; set; }
}
public class CartItemResponseDto
{
    /// <summary>
    /// Id: Primary key
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// User (foreign key): Pengguna yang terkait
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// User: Pengguna yang terkait
    /// </summary>
    public string UserName { get; set; } = string.Empty;
    /// <summary>
    /// Schedule (foreign key): Jadwal kelas yang terkait
    /// </summary>
    public int ScheduleId { get; set; }

    /// <summary>
    /// Nama Course
    /// </summary>
    public string CourseName { get; set; } = string.Empty;

    /// <summary>
    /// Nama Kategori Kelas
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    public long Price { get; set; }

    public DateOnly Date { get; set; }

    /// <summary>
    /// Product image URL
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

}
