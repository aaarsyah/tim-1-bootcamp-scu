using MyApp.BlazorUI.Models;

namespace MyApp.BlazorUI.Services
{
    public interface IMemberService
    {
        Task<List<MemberItem>> GetMembersAsync();
    }
}
