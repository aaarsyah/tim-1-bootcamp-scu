namespace MyApp.WebAPI.Models.Entities
{
    /// <summary>
    /// CartItem: Representasi sebuah barang belanjaan dalam keranjang pembelian<br />
    /// Many-to-One dengan User (Many CartItem, One user)
    /// Many-to-One dengan Schedule (Many CartItem, One Schedule)
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Id: Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User (foreign key): Pengguna yang terkait
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User: Pengguna yang terkait
        /// </summary>
        public virtual User User { get; set; } = null!;
        /// <summary>
        /// Schedule (foreign key): Jadwal kelas yang terkait
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// Schedule: Jadwal kelas yang terkait
        /// </summary>
        public virtual Schedule Schedule { get; set; } = null!;
        
    }
}
