namespace MyApp.WebAPI.Models.Entities
{
    /// <summary>
    /// CartItem: Representasi sebuah barang belanjaan dalam keranjang pembelian<br />
    /// Many-to-One dengan User (Many CartItem, One user)
    /// Many-to-One dengan Schedule (Many CartItem, One Schedule)
    /// </summary>
    public class CartItem : BaseEntity
    {
        /// <summary>
        /// User (foreign key): Pengguna yang terkait
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User: Pengguna yang terkait
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

        // /// <summary>
        // /// Virtual field untuk Schedules: Jadwal-jadwal kelas yang terkait
        // /// </summary>
        // public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        // /// <summary>
        // /// Virtual field untuk Category: Kategori kelas pelajaran yang terkait
        // /// </summary>
        // public virtual Category Category { get; set; } = null!;

        // /// <summary>
        // /// Virtual field untuk Course: get course name, price
        // /// </summary>
        // public virtual Course Course { get; set; } = null!;
        
    }
}
