using MyApp.Base;

namespace MyApp.Domain.Models;

/// <summary>
/// Course entity - Represents an academic course
/// Demonstrates Many-to-One (with Department), One-to-Many (with Enrollments),
/// and Many-to-Many (with Instructors) relationships
/// </summary>
public class Course : AuditableEntity
{
    /// <summary>
    /// Name: Nama kelas pelajaran
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Description: Deskripsi kelas pelajaran<br />
    /// Catatan: Ditampilkan pada page Detail Kelas
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// ImageUrl: URL gambar kelas pelajaran
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
    /// <summary>
    /// Price: Harga kelas dalam IDR
    /// </summary>
    public long Price { get; set; }
    /// <summary>
    /// IsActive: Apakah kelas pelajaran aktif?
    /// Catatan: Digunakan pada page Admin
    /// </summary>
    public bool IsActive { get; set; } = true;
    /// <summary>
    /// CategoryId (foreign key): Id kategori kelas pelajaran yang terkait
    /// </summary>
    public int CategoryId { get; set; }
    /// <summary>
    /// Virtual field untuk Category: Kategori kelas pelajaran yang terkait
    /// </summary>
    public virtual Category Category { get; set; } = null!;
    /// <summary>
    /// Virtual field untuk Schedules: Jadwal-jadwal kelas yang terkait
    /// </summary>
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}