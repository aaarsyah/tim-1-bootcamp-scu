using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    /// <summary>
    /// Enrollment entity - Junction table with additional properties
    /// Represents a student's enrollment in a course
    /// Demonstrates Many-to-Many relationship with extra data (Grade, Status, Date)
    /// </summary>
    public class Enrollment
    {
        /// <summary>
        /// Primary Key - Auto-increment ID
        /// </summary>
        public int Id { get; set; }
        
        // ========== FOREIGN KEYS ==========
        
        /// <summary>
        /// Foreign Key - Student who enrolled
        /// </summary>
        public int StudentId { get; set; }
        
        /// <summary>
        /// Foreign Key - Course being enrolled in
        /// </summary>
        public int CourseId { get; set; }
        
        // ========== ENROLLMENT DETAILS ==========
        
        /// <summary>
        /// When the student enrolled in this course
        /// </summary>
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Final grade for this course (nullable until course is completed)
        /// Scale: 0.00 - 100.00
        /// </summary>
        [Column(TypeName = "decimal(5,2)")]
        [Range(0.0, 100.0)]
        public decimal? Grade { get; set; }
        
        /// <summary>
        /// Letter grade (A, B, C, D, F)
        /// Computed from numeric grade
        /// </summary>
        [MaxLength(2)]
        public string? LetterGrade { get; set; }
        
        /// <summary>
        /// Current enrollment status
        /// </summary>
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
        
        /// <summary>
        /// Semester when enrolled (e.g., "Fall 2024", "Spring 2025")
        /// </summary>
        [MaxLength(50)]
        public string Semester { get; set; } = string.Empty;
        
        /// <summary>
        /// Academic year
        /// </summary>
        public int AcademicYear { get; set; }
        
        /// <summary>
        /// Optional notes about this enrollment
        /// </summary>
        [MaxLength(500)]
        public string? Notes { get; set; }
        
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
        /// Navigation property to Student
        /// </summary>
        public virtual Student Student { get; set; } = null!;
        
        /// <summary>
        /// Navigation property to Course
        /// </summary>
        public virtual Course Course { get; set; } = null!;
        
        // ========== HELPER METHODS ==========
        
        /// <summary>
        /// Calculate letter grade from numeric grade
        /// </summary>
        public void CalculateLetterGrade()
        {
            if (!Grade.HasValue)
            {
                LetterGrade = null;
                return;
            }

            LetterGrade = Grade.Value switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
        }
        
        /// <summary>
        /// Check if enrollment is completed
        /// </summary>
        [NotMapped]
        public bool IsCompleted => Status == EnrollmentStatus.Completed && Grade.HasValue;
        
        /// <summary>
        /// Check if enrollment is in progress
        /// </summary>
        [NotMapped]
        public bool IsInProgress => Status == EnrollmentStatus.Active;
    }
    
    /// <summary>
    /// Enrollment status enumeration
    /// Demonstrates enum usage with EF Core
    /// </summary>
    public enum EnrollmentStatus
    {
        /// <summary>
        /// Currently enrolled and active
        /// </summary>
        Active = 1,
        
        /// <summary>
        /// Course completed with grade
        /// </summary>
        Completed = 2,
        
        /// <summary>
        /// Dropped the course (no grade penalty)
        /// </summary>
        Dropped = 3,
        
        /// <summary>
        /// Withdrawn from course (may have grade impact)
        /// </summary>
        Withdrawn = 4,
        
        /// <summary>
        /// Failed the course
        /// </summary>
        Failed = 5,
        
        /// <summary>
        /// Audit only (no grade)
        /// </summary>
        Audit = 6
    }
}
