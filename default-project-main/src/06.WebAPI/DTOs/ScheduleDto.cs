namespace MyApp.WebAPI.DTOs
{
    public class ScheduleDto
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public string CourseName { get; set; } = string.Empty;
        public int CourseId { get; set; }
    
    }

    public class CreateScheduleDto
    {

        public DateOnly Date { get; set; }
        public int CourseId { get; set; }
    }
    public class UpdateScheduleDto
    { 
     
        public DateOnly Date { get; set; }
        public int CourseId { get; set; }
    }
}