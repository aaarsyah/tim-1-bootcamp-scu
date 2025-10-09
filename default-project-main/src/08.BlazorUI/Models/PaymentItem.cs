namespace MyApp.BlazorUI.Models
{
    public class PaymentItem
    {
        public int Id { get; set; } 
        public string Name { get; set; } = "";
        public string Logo { get; set; } = "";
        public PaymentStatus AllPayment { get; set; } = PaymentStatus.Active;
    }

    public enum PaymentStatus
    {
        Active,
        InActive
    }

}