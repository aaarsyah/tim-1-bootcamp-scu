using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    /// <summary>
    /// Instructor entity - Represents a teacher/professor
    /// Demonstrates Many-to-One (with Department) and Many-to-Many (with Courses) relationships
    /// </summary>
    public class Instructor
    {
        /// <summary>
        /// Primary Key - Auto-increment ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Instructor's first name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        /// <summary>
        /// Instructor's last name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        /// <summary>
        /// Email address - must be unique
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Phone number
        /// </summary>
        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        
        /// <summary>
        /// Office location
        /// </summary>
        [MaxLength(100)]
        public string? OfficeLocation { get; set; }
        
        /// <summary>
        /// Academic title/rank (Professor, Associate Professor, Assistant Professor, Lecturer)
        /// </summary>
        [MaxLength(50)]
        public string? Title { get; set; }
        
        /// <summary>
        /// Specialization or field of expertise
        /// </summary>
        [MaxLength(200)]
        public string? Specialization { get; set; }
        
        /// <summary>
        /// Date when instructor was hired
        /// </summary>
        public DateTime HireDate { get; set; }
        
        /// <summary>
        /// Instructor's salary
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }
        
        /// <summary>
        /// Employment status (Full-time, Part-time, Adjunct)
        /// </summary>
        public EmploymentType EmploymentType { get; set; } = EmploymentType.FullTime;
        
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
        /// Foreign Key - Department this instructor belongs to
        /// </summary>
        public int DepartmentId { get; set; }
        
        // ========== NAVIGATION PROPERTIES ==========
        
        /// <summary>
        /// Many-to-One relationship with Department
        /// Many instructors belong to one department
        /// </summary>
        public virtual Department Department { get; set; } = null!;
        
        /// <summary>
        /// Many-to-Many relationship with Courses
        /// One instructor can teach multiple courses
        /// One course can have multiple instructors
        /// </summary>
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        
        // ========== COMPUTED PROPERTIES ==========
        
        /// <summary>
        /// Full name computed property
        /// </summary>
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        
        /// <summary>
        /// Years of service
        /// </summary>
        [NotMapped]
        public int YearsOfService
        {
            get
            {
                var years = DateTime.Today.Year - HireDate.Year;
                if (HireDate.Date > DateTime.Today.AddYears(-years)) years--;
                return years;
            }
        }
        
        /// <summary>
        /// Number of courses currently teaching
        /// </summary>
        [NotMapped]
        public int CurrentCourseLoad => Courses?.Count(c => c.IsActive) ?? 0;
    }
    
    /// <summary>
    /// Employment type enumeration
    /// </summary>
    public enum EmploymentType
    {
        /// <summary>
        /// Full-time employee
        /// </summary>
        FullTime = 1,
        
        /// <summary>
        /// Part-time employee
        /// </summary>
        PartTime = 2,
        
        /// <summary>
        /// Adjunct/temporary instructor
        /// </summary>
        Adjunct = 3,
        
        /// <summary>
        /// Visiting professor
        /// </summary>
        Visiting = 4,
        
        /// <summary>
        /// Contract-based
        /// </summary>
        Contract = 5
    }
}
