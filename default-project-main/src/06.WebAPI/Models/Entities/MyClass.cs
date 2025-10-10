namespace MyApp.WebAPI.Models.Entities
{
    /// <summary>
    /// Participant: Representasi sebuah pengguna yang mengikuti sebuah kelas di satu jadwal<br />
    /// Many-to-One dengan User (Many Participant, One user)
    /// Many-to-One dengan Schedule (Many Participant, One Schedule)
    /// </summary>
    public class MyClass
    {
        /// <summary>
        /// Id: Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Foreign key ke User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Foreign key ke Schedule
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// User: Pengguna yang terkait (Navigasi)
        /// </summary>
        public virtual User User { get; set; } = null!;
        public virtual Schedule Schedule { get; set; } = null!;
        /// <summary>
        /// Schedule: Jadwal kelas yang terkait (Navigasi)
        /// </summary>
    
    }
}