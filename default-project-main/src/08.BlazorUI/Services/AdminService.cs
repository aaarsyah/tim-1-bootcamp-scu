using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MyApp.BlazorUI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace MyApp.BlazorUI.Services
{
    public interface IAdminService
    {
        Task<List<CourseItem>> GetCourseAsync();
        Task<CourseItem> CreateCourseAsync(CourseItem course);
        Task<CourseItem> UpdateCourseAsync(CourseItem course);
        Task<bool> DeleteCourseAsync(int id);
        Task<List<PaymentItem>> GetPaymentAsync();
        Task<PaymentItem> CreatePaymentAsync(PaymentItem payment);
        Task<PaymentItem> UpdatePaymentAsync(PaymentItem payment);
        Task<bool> DeletePaymentAsync(int id);
    }
    public class AdminService : IAdminService
    {
        private readonly List<CourseItem> _course = new();
        private readonly List<PaymentItem> _payment = new();
        private int _nextCourseId = 1;
        private int _nextPaymentId = 1;

        private readonly IHttpClientFactory _factory;

        public AdminService(IHttpClientFactory factory)
        {
            _factory = factory;
            SeedData(); //TODO: Remove
        }

        public async Task<List<CourseItem>> GetCourseAsync()
        {
            //GetAllCourses
            await Task.Delay(100);
            return _course.OrderBy(t => t.Id).ToList();
        }

        public async Task<CourseItem> CreateCourseAsync(CourseItem course)
        {
            await Task.Delay(100);
            course.Id = _nextCourseId++;
            _course.Add(course);
            return course;
        }

        public async Task<CourseItem> UpdateCourseAsync(CourseItem course)
        {
            await Task.Delay(100);
            var existingCourse = _course.FirstOrDefault(t => t.Id == course.Id);
            if (existingCourse != null)
            {
                var index = _course.IndexOf(existingCourse);
                _course[index] = existingCourse;
            }
            return course;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            await Task.Delay(100);
            var course = _course.FirstOrDefault(t => t.Id == id);
            if (course != null)
            {
                _course.Remove(course);
                return true;
            }
            return false;
        }

        public async Task<List<PaymentItem>> GetPaymentAsync()
        {
            await Task.Delay(100);
            return _payment.OrderBy(t => t.Id).ToList();
        }

        public async Task<PaymentItem> CreatePaymentAsync(PaymentItem payment)
        {
            await Task.Delay(100);
            payment.Id = _nextPaymentId++;
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
            var sampleCourse = new List<CourseItem>
            {
                new CourseItem
                {
                    Id = _nextCourseId++,
                    Name = "Drum Class For Beginner",
                    Description = "Drum Class",
                    Picture = "img/Class1.svg",
                    CategoryId = 1,
                    Harga = 1000000,
                    AllCourse = CourseStatus.Active
                },
                new CourseItem
                {
                    Id = _nextCourseId++,
                    Name = "Piano Class For Beginner",
                    Description = "Piano Class",
                    Picture = "img/Class2.svg",
                    CategoryId = 2,
                    Harga = 2000000,
                    AllCourse = CourseStatus.Active
                },
                new CourseItem
                {
                    Id = _nextCourseId++,
                    Name = "Gitar Class For Beginner",
                    Description = "Gitar Class",
                    Picture = "img/Class4.svg",
                    CategoryId = 3,
                    Harga = 2500000,
                    AllCourse = CourseStatus.Active
                },
                new CourseItem
                {
                    Id = _nextCourseId++,
                    Name = "Bass Class For Beginner",
                    Description = "Bass Class",
                    Picture = "img/Class3.svg",
                    CategoryId = 3,
                    Harga = 1500000,
                    AllCourse = CourseStatus.Active
                }
            };
            _course.AddRange(sampleCourse);
            var samplePayment = new List<PaymentItem>
            {
                new PaymentItem
                {
                    Id = _nextPaymentId++,
                    Name = "Gopay",
                    Logo = "img/Payment1.svg",
                    AllPayment = PaymentStatus.Active
                },
                new PaymentItem
                {
                    Id = _nextPaymentId++,
                    Name = "OVO",
                    Logo = "img/Payment2.svg",
                    AllPayment = PaymentStatus.Active
                },
                new PaymentItem
                {
                    Id = _nextPaymentId++,
                    Name = "Dana",
                    Logo = "img/Payment3.svg",
                    AllPayment = PaymentStatus.Active
                },
                new PaymentItem
                {
                    Id = _nextPaymentId++,
                    Name = "Mandiri",
                    Logo = "img/Payment4.svg",
                    AllPayment = PaymentStatus.Active
                },
                new PaymentItem
                {
                    Id = _nextPaymentId++,
                    Name = "BCA",
                    Logo = "img/Payment5.svg",
                    AllPayment = PaymentStatus.Active
                },
                new PaymentItem
                {
                    Id = _nextPaymentId++,
                    Name = "BNI",
                    Logo = "img/Payment6.svg",
                    AllPayment = PaymentStatus.Active
                }
            };
            _payment.AddRange(samplePayment);
        }
    }
}
