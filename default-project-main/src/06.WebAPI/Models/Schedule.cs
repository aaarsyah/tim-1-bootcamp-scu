namespace MyApp.WebAPI.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
