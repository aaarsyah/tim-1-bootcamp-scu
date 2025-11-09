namespace MyApp.Base;

/// <summary>
/// Kelas untuk entity dasar
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Id: Primary key<br />
    /// Catatan: Jangan gunakan ini
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// RefId: Reference Id<br />
    /// Catatan: Gunakan ini untuk akses API
    /// </summary>
    public Guid RefId { get; set; } = Guid.NewGuid();
}
/// <summary>
/// Kelas untuk entity yang memiliki audit field (tanggal create/update dan pelaku create/update)
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    /// <summary>
    /// CreatedAt: Tanggal dan waktu entity dibuat
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// UpdatedAt: Tanggal dan waktu entity diubah
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}