namespace MyApp.Shared.DTOs
{
    public class MyClassDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ScheduleId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public DateOnly Date { get; set; }

        public string CourseName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;


    }

    public class CreateMyClassDto
    {

        public int UserId { get; set; }

        public int ScheduleId { get; set; }
        
    }
    public class UpdateMyClassDto
    {
        public int UserId { get; set; }
        
        public int ScheduleId { get; set; }
    }
}