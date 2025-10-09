using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public class MemberService : IMemberService
    {
        private readonly List<MemberItem> _members = new();
        private int _nextId = 1;

        public MemberService()
        {
            SeedData();
        }

        private void SeedData()
        {
            var sampleMember = new List<MemberItem>
            {
                new MemberItem
                {
                    Id = _nextId++,
                    Email = "nurimankamis@gmail.com",
                    NoInvoice = "DLA000002",
                    Date = new DateTime(2025, 3, 22),
                    TotalCourse = 3,
                    TotalPrice = 1450000,
                    //MemberStatus = ...
                },
                new MemberItem
                {
                    Id = _nextId++,
                    Email = "yusrilmansur@gmail.com",
                    NoInvoice = "DLA000003",
                    Date = new DateTime(2025, 5, 25),
                    TotalCourse = 2,
                    TotalPrice = 500000,
                    //MemberStatus = ...
                }
            };

            _members.AddRange(sampleMember);
        }

        public Task<List<MemberItem>> GetMembersAsync()
        {
            return Task.FromResult(_members);
        }
    }
}
