namespace MyApp.WebAPI.Models.Dtos
{
    public class InvoiceDetailsDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public decimal Price { get; set; }

        public ScheduleDto Schedule { get; set; }
    }
}
