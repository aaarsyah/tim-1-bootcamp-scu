namespace MyApp.Shared.DTOs;

public class PaymentMethodDto
{
    public int Id { get; set; }
  
    public string Name { get; set; } = string.Empty;

    public string LogoUrl { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}

public class CreatePaymentMethodRequestDto
{ 
    public string Name { get; set; } = string.Empty;

    public string LogoUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
public class UpdatePaymentRequestDto
{ 
    public string Name { get; set; } = string.Empty;

    public string LogoUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}