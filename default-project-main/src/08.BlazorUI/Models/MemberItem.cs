namespace MyApp.BlazorUI.Models
{
    public class MemberItem
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NoInvoice { get; set; }
        public DateTime Date { get; set; }
        public int TotalCourse { get; set; }
        public int TotalPrice { get; set; }
        public MemberStatus AllMember { get; set; } = MemberStatus.Active;
    }

    public enum MemberStatus
    {
        Active,
        InActive
    }
}
