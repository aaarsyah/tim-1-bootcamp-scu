using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    /// <summary>
    /// Department entity - Represents an academic department
    /// Demonstrates One-to-Many relationships with both Products and Instructors
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Primary Key - Auto-increment ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Department name - Must be unique
        /// Example: "Computer Science", "Mathematics", "Physics"
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Short department code - Must be unique
        /// Example: "CS", "MATH", "PHYS"
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// Optional department description
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }
        
        /// <summary>
        /// When the department was established
        /// </summary>
        public DateTime EstablishedDate { get; set; }
        
        /// <summary>
        /// Department budget (optional)
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Budget { get; set; }
        
        /// <summary>
        /// Department building/location
        /// </summary>
        [MaxLength(200)]
        public string? Location { get; set; }
        
        /// <summary>
        /// Soft delete flag
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Audit trail - creation timestamp
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Audit trail - last update timestamp
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // ========== NAVIGATION PROPERTIES ==========
        
        /// <summary>
        /// One-to-Many relationship with Products
        /// One department has many Products
        /// </summary>
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        
        /// <summary>
        /// One-to-Many relationship with Instructors
        /// One department has many instructors
        /// </summary>
        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        
        // ========== COMPUTED PROPERTIES ==========
        
        /// <summary>
        /// Total number of active Products
        /// </summary>
        [NotMapped]
        public int ActiveProductsCount => Products?.Count(c => c.IsActive) ?? 0;
        
        /// <summary>
        /// Total number of instructors
        /// </summary>
        [NotMapped]
        public int InstructorCount => Instructors?.Count ?? 0;
    }
}
