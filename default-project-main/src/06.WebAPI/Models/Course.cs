using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    /// <summary>
    /// Course entity - Represents an academic course
    /// Demonstrates Many-to-One (with Department), One-to-Many (with Enrollments),
    /// and Many-to-Many (with Instructors) relationships
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Primary Key - Auto-increment ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Course code - Unique identifier like "CS101", "MATH201"
        /// Must be unique across all courses
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string CourseCode { get; set; } = string.Empty;
        
        /// <summary>
        /// Course title/name
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// Detailed course description
        /// </summary>
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Credit hours for this course
        /// Typically 1-4 credits
        /// </summary>
        [Range(1, 6)]
        public int Credits { get; set; }
        
        /// <summary>
        /// Course capacity - maximum number of students
        /// </summary>
        [Range(1, 500)]
        public int Capacity { get; set; } = 30;
        
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
        
        // ========== FOREIGN KEYS ==========
        
        /// <summary>
        /// Foreign Key - Department this course belongs to
        /// Every course must belong to a department
        /// </summary>
        public int DepartmentId { get; set; }
        
        // ========== NAVIGATION PROPERTIES ==========
        
        /// <summary>
        /// Many-to-One relationship with Department
        /// Many courses belong to one department
        /// </summary>
        public virtual Department Department { get; set; } = null!;
        
        /// <summary>
        /// One-to-Many relationship with Enrollments
        /// One course can have many student enrollments
        /// </summary>
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        
        /// <summary>
        /// Many-to-Many relationship with Instructors
        /// One course can be taught by multiple instructors
        /// One instructor can teach multiple courses
        /// Junction table: CourseInstructor (created automatically by EF Core)
        /// </summary>
        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        
        // ========== COMPUTED PROPERTIES ==========
        
        /// <summary>
        /// Current enrollment count
        /// </summary>
        [NotMapped]
        public int EnrolledStudentsCount => Enrollments?.Count(e => e.Status == EnrollmentStatus.Active) ?? 0;
        
        /// <summary>
        /// Check if course is full
        /// </summary>
        [NotMapped]
        public bool IsFull => EnrolledStudentsCount >= Capacity;
        
        /// <summary>
        /// Available seats
        /// </summary>
        [NotMapped]
        public int AvailableSeats => Capacity - EnrolledStudentsCount;
    }
}
