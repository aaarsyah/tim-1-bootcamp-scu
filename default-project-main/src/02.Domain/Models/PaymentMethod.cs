using MyApp.Base;

namespace MyApp.Domain.Models;

/// <summary>
/// PaymentMethod: Representasi sebuah metode pembayaran<br />
/// One-to-Many dengan Invoice (One PaymentMethod, Many Invoice)
/// </summary>
public class PaymentMethod : AuditableEntity
{
    /// <summary>
    /// Name: Nama metode pembayaran
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// LogoUrl: URL logo metode pembayaran
    /// </summary>
    public string LogoUrl { get; set; } = string.Empty;
    /// <summary>
    /// IsActive: Apakah metode pembayaran aktif?
    /// Catatan: Digunakan pada page Admin
    /// </summary>
    public bool IsActive { get; set; } = true;
}