namespace MyApp.Shared.DTOs;

/// <summary>
/// Product data transfer object
/// </summary>
public class InvoiceDetailDto
{
    /// <summary>
    /// Invoice Detail ID
    /// </summary>
    public int Id { get; set; }

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

    public DateOnly ScheduleDate { get; set; }
    
}

/// <summary>
/// Create Invoice request
/// </summary>
public class CreateInvoiceDetailDto
{
    /// <summary>
    /// RefCode
    /// </summary>
    public string RefCode { get; set; } = string.Empty;

    /// <summary>
    /// TanggalBeli
    /// </summary>
    public DateTime CreatedAt { get; set; }
}