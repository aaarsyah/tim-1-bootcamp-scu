namespace MyApp.Shared.DTOs;

/// <summary>
/// Product data transfer object
/// </summary>
public class InvoiceDetailDto
{
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