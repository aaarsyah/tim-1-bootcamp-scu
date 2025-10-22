namespace MyApp.BlazorUI.Models
{
    public class UserItem
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string Name { get; set; }  = "";
        public DateTime? BirthDate { get; set; }
        public Role UserRole { get; set; } = Role.User;
        public UserStatus AllUser { get; set; } = UserStatus.Active;
    }

    public enum UserStatus
    {
        Active,
        InActive
    }

    public enum Role
    {
        Admin,
        User
    }
}