namespace MyApp.WebAPI.Models
{
    /// <summary>
    /// Participant: Representasi sebuah pengguna yang mengikuti sebuah kelas di satu jadwal<br />
    /// Many-to-One dengan User (Many Participant, One user)
    /// Many-to-One dengan Schedule (Many Participant, One Schedule)
    /// </summary>
    public class Participant
    {
        /// <summary>
        /// Id: Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User: Pengguna yang terkait
        /// </summary>
        public User User { get; set; } = null;
        /// <summary>
        /// Schedule: Jadwal kelas yang terkait
        /// </summary>
        public Schedule Schedule { get; set; } = null;
    }
}
