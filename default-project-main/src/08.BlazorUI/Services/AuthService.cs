using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
        Task<bool> RegisterAsync(RegisterRequestDto request);
        Task<bool> ConfirmEmailAsync(ConfirmEmailRequestDto request);
        Task<bool> ChangePasswordAsync(ChangePasswordRequestDto request);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto request);
        Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);
        Task<UserDto?> GetCurrentUserAsync();
        Task LogoutAsync();
        Task<bool> IsLoggedIn();
        Task<bool> IsAdmin();
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }
        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDto>>();

                    if (apiResponse?.StatusCode == "SUCCESS" && apiResponse.Data != null)
                    {
                        await _localStorage.SetItemAsync("authToken", apiResponse.Data.AccessToken);
                        await _localStorage.SetItemAsync("refreshToken", apiResponse.Data.RefreshToken);

                        _httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", apiResponse.Data.AccessToken);

                        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(apiResponse.Data.AccessToken);

                        return apiResponse.Data;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                return apiResponse?.StatusCode == "SUCCESS";
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> ConfirmEmailAsync(ConfirmEmailRequestDto request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/confirm-email", request);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                return apiResponse?.StatusCode == "SUCCESS";
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> ChangePasswordAsync(ChangePasswordRequestDto request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/change-password", request);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                return apiResponse?.StatusCode == "SUCCESS";
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/forgot-password", request);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                return apiResponse?.StatusCode == "SUCCESS";
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/reset-password", request);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                return apiResponse?.StatusCode == "SUCCESS";
            }
            catch
            {
                return false;
            }
        }
        public async Task<UserDto?> GetCurrentUserAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/auth/profile");

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
                    return apiResponse?.Data;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task LogoutAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/auth/logout");

                if (!response.IsSuccessStatusCode)
                {
                    // mungkin error karena token invalid, kita buang tokennya
                    await _localStorage.RemoveItemAsync("authToken");
                    await _localStorage.RemoveItemAsync("refreshToken");
                    _httpClient.DefaultRequestHeaders.Authorization = null;

                    ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout(); // Bagaimanapun juga user tidak ter-login
                    return;
                }
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
                if (apiResponse?.StatusCode != "SUCCESS")
                {
                    // mungkin error karena token invalid, kita buang tokennya
                    await _localStorage.RemoveItemAsync("authToken");
                    await _localStorage.RemoveItemAsync("refreshToken");
                    _httpClient.DefaultRequestHeaders.Authorization = null;

                    ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout(); // Bagaimanapun juga user tidak ter-login
                    return;
                }
                //Berhasil
                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.RemoveItemAsync("refreshToken");
                _httpClient.DefaultRequestHeaders.Authorization = null;

                ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
                return;
            }
            catch
            {
                // lost connection? Don't logout users yet.
                return;
            }

        }
        public async Task<bool> IsLoggedIn()
        {
            return await ((CustomAuthStateProvider)_authStateProvider).isLoggedInAsync();
        }
        public async Task<bool> IsAdmin()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
                return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var handler = new JwtSecurityTokenHandler();
            var claims = handler.ReadJwtToken(token).Claims;
            var isadmin = claims.FirstOrDefault(c => c.Type == "role")?.Value;
            if (isadmin == null)
            {
                return false;
            }
            Console.WriteLine(isadmin);
            return isadmin.Contains("Admin");
        }
    }
}
