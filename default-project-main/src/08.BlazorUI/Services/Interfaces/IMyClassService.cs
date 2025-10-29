using MyApp.Shared.DTOs;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public interface IMyClassService
{
    Task<List<MyClassDto>> GetOwnMyClasses(AuthenticationHeaderValue authorization);
}