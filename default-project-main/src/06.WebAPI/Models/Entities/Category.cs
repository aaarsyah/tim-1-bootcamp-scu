namespace MyApp.WebAPI.Models.Entities
{
    /// <summary>
    /// Entity class untuk merepresentasikan Category dalam database
    /// Class ini akan di-map ke tabel "Categories" oleh Entity Framework
    /// One-to-Many relationship dengan Product (1 Category -> Many Products)
    /// </summary>
    public class Category
    {
     
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty; // Default empty string untuk avoid null

        public string LongName { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}