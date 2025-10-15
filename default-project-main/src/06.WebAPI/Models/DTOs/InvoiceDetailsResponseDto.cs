using MyApp.WebAPI.Models.Dtos;


namespace MyApp.WebAPI.Models.Dtos
{
    public class InvoiceDetailResponseDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ScheduleId { get; set; }
        public decimal Price { get; set; }

        public ScheduleDto Schedule { get; set; }
    }
}
