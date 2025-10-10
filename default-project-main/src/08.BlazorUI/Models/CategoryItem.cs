namespace MyApp.BlazorUI.Models
{
    public class CategoryItem
    {
        public int Id { get; set; } 
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Picture { get; set; } = "";
        public CategoryStatus AllCategory { get; set; } = CategoryStatus.Active;
    }

    public enum CategoryStatus
    {
        Active,
        InActive
    }

}