namespace MyApp.WebAPI.Models
{
    /// <summary>
    /// Schedule: Representasi sebuah jadwal kelas<br />
    /// Many-to-One dengan Course (Many Schedules, One Course)
    /// Many-to-One dengan Schedule (Many CartItem, One Schedule)
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// Id: Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date: Tanggal mulai kelas
        /// </summary>
        public DateOnly Date { get; set; }
        /// <summary>
        /// Course: Kelas pelajaran
        /// </summary>
        public Course Course { get; set; } = null!;
    }
}
