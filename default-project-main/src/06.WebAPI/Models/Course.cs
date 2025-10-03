namespace MyApp.WebAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public Category Category { get; set; } = null;
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        //TODO: Add image URL
    }
}
