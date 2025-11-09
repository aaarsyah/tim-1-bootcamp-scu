namespace MyApp.Shared.DTOs;

public class MyClassDto
{
    public Guid RefId { get; set; }
    public Guid UserRefId { get; set; }
    public Guid ScheduleRefId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public string CourseName { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;


}

public class CreateMyClassRequestDto
{

    public int UserId { get; set; }

    public int ScheduleId { get; set; }
    
}
public class UpdateMyClassRequestDto
{
    public int UserId { get; set; }
    
    public int ScheduleId { get; set; }
}