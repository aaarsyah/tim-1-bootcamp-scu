using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    /// <summary>
    /// StudentProfile entity - Additional student information
    /// Demonstrates One-to-One relationship with Student
    /// Uses the same ID as Student (shared primary key pattern)
    /// </summary>
    public class StudentProfile
    {
        /// <summary>
        /// Primary Key - Same as Student ID (One-to-One relationship)
        /// This is also the Foreign Key to Student
        /// </summary>
        [Key]
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        
        // ========== CONTACT INFORMATION ==========
        
        /// <summary>
        /// Primary phone number
        /// </summary>
        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        
        /// <summary>
        /// Alternative phone number
        /// </summary>
        [Phone]
        [MaxLength(20)]
        public string? AlternatePhone { get; set; }
        
        /// <summary>
        /// Street address
        /// </summary>
        [MaxLength(255)]
        public string? Address { get; set; }
        
        /// <summary>
        /// City
        /// </summary>
        [MaxLength(100)]
        public string? City { get; set; }
        
        /// <summary>
        /// State or province
        /// </summary>
        [MaxLength(50)]
        public string? State { get; set; }
        
        /// <summary>
        /// Postal/ZIP code
        /// </summary>
        [MaxLength(10)]
        public string? ZipCode { get; set; }
        
        /// <summary>
        /// Country
        /// </summary>
        [MaxLength(100)]
        public string? Country { get; set; }
        
        // ========== EMERGENCY CONTACT ==========
        
        /// <summary>
        /// Emergency contact person's name
        /// </summary>
        [MaxLength(100)]
        public string? EmergencyContactName { get; set; }
        
        /// <summary>
        /// Emergency contact relationship (Parent, Spouse, Sibling, etc.)
        /// </summary>
        [MaxLength(50)]
        public string? EmergencyContactRelationship { get; set; }
        
        /// <summary>
        /// Emergency contact phone number
        /// </summary>
        [Phone]
        [MaxLength(20)]
        public string? EmergencyContactPhone { get; set; }
        
        // ========== ADDITIONAL INFORMATION ==========
        
        /// <summary>
        /// Blood type (A+, A-, B+, B-, AB+, AB-, O+, O-)
        /// </summary>
        [MaxLength(3)]
        public string? BloodType { get; set; }
        
        /// <summary>
        /// Medical conditions or allergies
        /// </summary>
        [MaxLength(500)]
        public string? MedicalNotes { get; set; }
        
        /// <summary>
        /// Student ID card number
        /// </summary>
        [MaxLength(50)]
        public string? StudentIdCardNumber { get; set; }
        
        /// <summary>
        /// Profile photo URL or path
        /// </summary>
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }
        
        /// <summary>
        /// Student biography or about section
        /// </summary>
        [MaxLength(1000)]
        public string? Biography { get; set; }
        
        /// <summary>
        /// Hobbies and interests
        /// </summary>
        [MaxLength(500)]
        public string? Interests { get; set; }
        
        /// <summary>
        /// Social media links (JSON or comma-separated)
        /// </summary>
        [MaxLength(500)]
        public string? SocialMediaLinks { get; set; }
        
        /// <summary>
        /// Audit trail - when profile was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Audit trail - when profile was last updated
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // ========== NAVIGATION PROPERTIES ==========
        
        /// <summary>
        /// One-to-One relationship with Student
        /// Navigation property back to the student
        /// </summary>
        public virtual Student Student { get; set; } = null!;
        
        // ========== COMPUTED PROPERTIES ==========
        
        /// <summary>
        /// Full address formatted as single string
        /// </summary>
        [NotMapped]
        public string FullAddress
        {
            get
            {
                var parts = new[] { Address, City, State, ZipCode, Country }
                    .Where(p => !string.IsNullOrWhiteSpace(p));
                return string.Join(", ", parts);
            }
        }
        
        /// <summary>
        /// Check if profile has complete contact information
        /// </summary>
        [NotMapped]
        public bool HasCompleteContactInfo =>
            !string.IsNullOrWhiteSpace(PhoneNumber) &&
            !string.IsNullOrWhiteSpace(Address) &&
            !string.IsNullOrWhiteSpace(City) &&
            !string.IsNullOrWhiteSpace(State);
        
        /// <summary>
        /// Check if emergency contact information is complete
        /// </summary>
        [NotMapped]
        public bool HasEmergencyContact =>
            !string.IsNullOrWhiteSpace(EmergencyContactName) &&
            !string.IsNullOrWhiteSpace(EmergencyContactPhone);
    }
}
