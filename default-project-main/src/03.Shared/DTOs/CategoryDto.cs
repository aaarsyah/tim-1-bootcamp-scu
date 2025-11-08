namespace MyApp.Shared.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public Guid RefId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LongName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    

    public DateTime CreatedAt { get; set; }
    
    public string ImageUrl { get; set; } = string.Empty;
}

/// <summary>
/// Create category request
/// </summary>
public class CreateCategoryRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string LongName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
public class UpdateCategoryRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string LongName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}