using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    /// <summary>
    /// Student entity - Represents a student in the school system
    /// Demonstrates One-to-Many (with Enrollments) and One-to-One (with StudentProfile) relationships
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Primary Key - Auto-increment ID
        /// EF Core convention: Property named "Id" or "EntityNameId" is automatically configured as PK
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Student's first name - Required field
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        /// <summary>
        /// Student's last name - Required field
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        /// <summary>
        /// Student's email address - Required and must be unique
        /// Will be configured with unique index in Fluent API
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Student's date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        
        /// <summary>
        /// Grade Point Average (0.00 - 4.00)
        /// Using decimal for precision in academic calculations
        /// </summary>
        [Column(TypeName = "decimal(3,2)")]
        [Range(0.0, 4.0)]
        public decimal GPA { get; set; } = 0.00m;
        
        /// <summary>
        /// Optional nationality field
        /// Added to demonstrate migration with new column
        /// </summary>
        [MaxLength(100)]
        public string? Nationality { get; set; }
        
        /// <summary>
        /// Expected or actual graduation date
        /// </summary>
        public DateTime? GraduationDate { get; set; }
        
        /// <summary>
        /// Soft delete flag - true if student is active
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Audit trail - when student record was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Audit trail - when student record was last updated
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // ========== NAVIGATION PROPERTIES ==========
        
        /// <summary>
        /// One-to-Many relationship with Enrollments
        /// One student can have many enrollments (enrolled in multiple courses)
        /// virtual keyword enables lazy loading (if configured)
        /// </summary>
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        
        /// <summary>
        /// One-to-One relationship with StudentProfile
        /// One student has one profile with additional personal information
        /// </summary>
        public virtual StudentProfile? Profile { get; set; }
        
        // ========== COMPUTED PROPERTIES (Not Mapped) ==========
        
        /// <summary>
        /// Full name computed property - not stored in database
        /// [NotMapped] attribute tells EF to ignore this property
        /// </summary>
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        
        /// <summary>
        /// Calculate age from date of birth
        /// </summary>
        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}
