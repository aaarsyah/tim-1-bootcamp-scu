namespace MyApp.Shared.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
      
        public string Name { get; set; } = string.Empty;
  
        public string LogoUrl { get; set; } = string.Empty;
 
        public DateTime CreatedAt { get; set; }
  
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CreatePaymentDto
    { 
        public string Name { get; set; } = string.Empty;
  
        public string LogoUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
    public class UpdatePaymentDto
    { 
        public string Name { get; set; } = string.Empty;
  
        public string LogoUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}