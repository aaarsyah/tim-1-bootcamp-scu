namespace MyApp.WebAPI.Models
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
        /// User: Pengguna yang terkait
        /// </summary>
        public User User { get; set; } = null!;
        /// <summary>
        /// Schedule: Jadwal kelas yang terkait
        /// </summary>
        public Schedule Schedule { get; set; } = null!;
    }
}
