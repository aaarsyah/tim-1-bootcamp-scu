using MyApp.Base;

namespace MyApp.Domain.Models;

/// <summary>
/// Schedule: Representasi sebuah jadwal kelas<br />
/// Many-to-One dengan Course (Many Schedules, One Course)
/// Many-to-One dengan Schedule (Many CartItem, One Schedule)
/// </summary>
public class Schedule : BaseEntity
{
    /// <summary>
    /// Date: Tanggal mulai kelas
    /// </summary>
    public DateOnly Date { get; set; }
    /// <summary>
    /// CreatedAt: Tangal pembuatan kelas pelajaran<br />
    /// Catatan: Digunakan pada page Admin
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// UpdatedAt: Tangal perubahan kelas pelajaran<br />
    /// Catatan: Digunakan pada page Admin
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    /// <summary>
    /// CourseId (foreign key): Kelas pelajaran yang terkait
    /// </summary>
    public int CourseId { get; set; }
    /// <summary>
    /// Course: Kelas pelajaran yang terkait
    /// </summary>
    public virtual Course Course { get; set; } = null!;
}