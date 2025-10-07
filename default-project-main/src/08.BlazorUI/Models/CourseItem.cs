namespace MyApp.BlazorUI.Models
{
    public class CourseItem
    {
        public int Id { get; set; } 
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Picture { get; set; } = "";
        public int CategoryId { get; set; }
        public int Harga { get; set; }
        public DateTime Jadwal { get; set; } = DateTime.UtcNow;


        public CourseStatus AllCourse { get; set; } = CourseStatus.Active;
    }

    public enum CourseStatus
    {
        Active,
        InActive
    }

}