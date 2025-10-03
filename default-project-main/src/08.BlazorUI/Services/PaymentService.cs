using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly List<PaymentItem> _payment = new();
        private int _nextId = 1;

        public PaymentService()
        {
            SeedData();
        }

        public async Task<List<PaymentItem>> GetPaymentAsync()
        {
            await Task.Delay(100); 
            return _payment.OrderBy(t => t.Id).ToList();
        }

        public async Task<PaymentItem> CreatePaymentAsync(PaymentItem payment)
        {
            await Task.Delay(100);
            payment.Id = _nextId++;
            _payment.Add(payment);
            return payment;
        }

        public async Task<PaymentItem> UpdatePaymentAsync(PaymentItem payment)
        {
            await Task.Delay(100);
            var existingPayment = _payment.FirstOrDefault(t => t.Id == payment.Id);
            if (existingPayment != null)
            {
                var index = _payment.IndexOf(existingPayment);
                _payment[index] = existingPayment;
            }
            return payment;
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            await Task.Delay(100);
            var payment = _payment.FirstOrDefault(t => t.Id == id);
            if (payment != null)
            {
                _payment.Remove(payment);
                return true;
            }
            return false;
        }

        private void SeedData()
        {
            var samplePayment = new List<PaymentItem>
            {
                new PaymentItem
                {
                    Id = _nextId++,
                    Name = "Gopay",
                    Logo = "img/Payment1.svg",
                    AllPayment = PaymentStatus.Active
                },
                new PaymentItem
                {
                    Id = _nextId++,
                    Name = "OVO",
                    Logo = "img/Payment2.svg",
                    AllPayment = PaymentStatus.Active
                },
                new PaymentItem
                {
                    Id = _nextId++,
                    Name = "Dana",
                    Logo = "img/Payment3.svg",
                    AllPayment = PaymentStatus.Active
                },
                new PaymentItem
                {
                    Id = _nextId++,
                    Name = "Mandiri",
                    Logo = "img/Payment4.svg",
                    AllPayment = PaymentStatus.Active
                },
                new PaymentItem
                {
                    Id = _nextId++,
                    Name = "BCA",
                    Logo = "img/Payment5.svg",
                    AllPayment = PaymentStatus.Active
                },
                new PaymentItem
                {
                    Id = _nextId++,
                    Name = "BNI",
                    Logo = "img/Payment6.svg",
                    AllPayment = PaymentStatus.Active
                }
            };
            _payment.AddRange(samplePayment);
        }
    }
}
