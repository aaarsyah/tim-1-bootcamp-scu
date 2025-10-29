using MyApp.Base;

namespace MyApp.Domain.Models;

/// <summary>
/// MyClass: Representasi sebuah jadwal kelas yang diikuti pengguna<br />
/// Many-to-One dengan User (Many MyClass, One user)
/// Many-to-One dengan Schedule (Many MyClass, One Schedule)
/// </summary>
public class MyClass : BaseEntity
{
    /// <summary>
    /// UserId (foreign key): Id pengguna yang terkait
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Virtual field untuk User: Pengguna yang terkait
    /// </summary>
    public virtual User User { get; set; } = null!;
    /// <summary>
    /// ScheduleId (foreign key): Id jadwal kelas yang terkait
    /// </summary>
    public int ScheduleId { get; set; }
    /// <summary>
    /// Virtual field untuk Schedule: Jadwal kelas yang terkait
    /// </summary>
    public virtual Schedule Schedule { get; set; } = null!;

}