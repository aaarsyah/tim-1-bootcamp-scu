using MyApp.WebAPI.Models.Dtos;


public class InvoiceResponseDto
{
    public int Id { get; set; }
    public string RefCode { get; set; }
    public DateTime CreatedAt { get; set; }

    public int? UserId { get; set; }
    public UserDto User { get; set; }

    public int? PaymentMethodId { get; set; }
    public PaymentMethodDto PaymentMethod { get; set; }

    public int JumlahKursus { get; set; }
    public decimal TotalHarga { get; set; }

    public List<InvoiceDetailDto> InvoiceDetails { get; set; }
}
