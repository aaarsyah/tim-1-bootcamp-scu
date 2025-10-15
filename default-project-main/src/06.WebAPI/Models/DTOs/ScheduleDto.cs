namespace MyApp.WebAPI.Models.Dtos
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        // Tambahkan properti lain sesuai kebutuhan
    }
}
