namespace MyApp.Shared.DTOs;

/// <summary>
/// Product data transfer object
/// </summary>
public class CourseDto
{
    /// <summary>
    /// Product ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    public long Price { get; set; }


    /// <summary>
    /// Product image URL
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Whether product is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// When product was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Category ID
    /// </summary>
    public int CategoryId { get; set; }


    /// <summary>
    /// Category name
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    public List<DateOnly> ScheduleDates { get; set; } = new();

    //ScheduleId
    public List<ScheduleDto> Schedules { get; set; } = new();
    
}

/// <summary>
/// Create product request
/// </summary>
public class CreateCourseRequestDto
{
    /// <summary>
    /// Product name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Product price
    /// </summary>
    public long Price { get; set; }


    /// <summary>
    /// Product image URL
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    /// <summary>
    /// Category ID
    /// </summary>
    public int CategoryId { get; set; }
}

/// <summary>
/// Update product request
/// </summary>
public class UpdateCourseRequestDto
{
    /// <summary>
    /// Product name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Product description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Product price
    /// </summary>
    public long Price { get; set; }
    
    
    /// <summary>
    /// Product image URL
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether product is active
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Category ID
    /// </summary>
    public int CategoryId { get; set; }
}