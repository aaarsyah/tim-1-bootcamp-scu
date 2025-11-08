namespace MyApp.Shared.DTOs;

public class ScheduleDto
{
    public Guid RefId { get; set; }
    public DateOnly Date { get; set; }

    public string CourseName { get; set; } = string.Empty;
    public int CourseId { get; set; }

}

public class CreateScheduleRequestDto
{

    public DateOnly Date { get; set; }
    public int CourseId { get; set; }
}
public class UpdateScheduleRequestDto
{ 
 
    public DateOnly Date { get; set; }
    public int CourseId { get; set; }
}