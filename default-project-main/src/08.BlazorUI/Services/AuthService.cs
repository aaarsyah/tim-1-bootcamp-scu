using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace MyApp.BlazorUI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
        Task<bool> RegisterAsync(RegisterRequestDto request);
        Task<bool> ConfirmEmailAsync(ConfirmEmailRequestDto request);
        Task<bool> ChangePasswordAsync(AuthenticationHeaderValue authorization, ChangePasswordRequestDto request);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto request);
        Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);
        Task<UserDto?> GetCurrentUserAsync(AuthenticationHeaderValue authorization);
        Task LogoutAsync(AuthenticationHeaderValue authorization);
        Task<bool> IsLoggedInAsync();
        Task<bool> IsAdminAsync();
        Task<AuthenticationHeaderValue?> GetAccessTokenAsync();
    }
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _factory;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

        public AuthService(
            IHttpClientFactory factory,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authStateProvider)
        {
            _factory = factory;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }
        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var _httpClient = _factory.CreateClient("WebAPI");
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDto>>();

                    if (apiResponse?.StatusCode == "SUCCESS" && apiResponse.Data != null)
                    {
                        await SetTokensAsync(apiResponse.Data.AccessToken, apiResponse.Data.RefreshToken);

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
            var _httpClient = _factory.CreateClient("WebAPI");
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
            var _httpClient = _factory.CreateClient("WebAPI");
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
        public async Task<bool> ChangePasswordAsync(AuthenticationHeaderValue authorization, ChangePasswordRequestDto request)
        {
            var _httpClient = _factory.CreateClient("WebAPI");
            _httpClient.DefaultRequestHeaders.Authorization = authorization;
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
            var _httpClient = _factory.CreateClient("WebAPI");
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
            var _httpClient = _factory.CreateClient("WebAPI");
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
        public async Task<UserDto?> GetCurrentUserAsync(AuthenticationHeaderValue authorization)
        {
            var _httpClient = _factory.CreateClient("WebAPI");
            _httpClient.DefaultRequestHeaders.Authorization = authorization;
            try
            {
                var response = await _httpClient.GetAsync("api/auth/me");

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
        public async Task LogoutAsync(AuthenticationHeaderValue authorization)
        {
            var _httpClient = _factory.CreateClient("WebAPI");
            _httpClient.DefaultRequestHeaders.Authorization = authorization;
            try
            {
                var response = await _httpClient.PostAsync("api/auth/logout", null);

                if (!response.IsSuccessStatusCode)
                {
                    // mungkin error karena token invalid, kita buang tokennya
                    await ClearTokensAsync();

                    ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout(); // Bagaimanapun juga user tidak ter-login
                    return;
                }
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
                if (apiResponse?.StatusCode != "SUCCESS")
                {
                    // mungkin error karena token invalid, kita buang tokennya
                    await ClearTokensAsync();

                    ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout(); // Bagaimanapun juga user tidak ter-login
                    return;
                }
                //Berhasil
                await ClearTokensAsync();

                ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
                return;
            }
            catch
            {
                // lost connection? Don't logout users yet.
                return;
            }

        }
        public async Task<bool> IsLoggedInAsync()
        {
            var _httpClient = _factory.CreateClient("WebAPI");
            //Set Authorization header to include the access token
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                _httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);
            }
            return await ((CustomAuthStateProvider)_authStateProvider).isLoggedInAsync();
        }
        public async Task<bool> IsAdminAsync()
        {
            var role = (await ((CustomAuthStateProvider)_authStateProvider).GetAuthenticationStateAsync())
                .User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            if (role == null)
            {
                return false;
            }
            Console.WriteLine($"Test role: {role}");
            return role.Contains("Admin");
        }
        /// <summary>
        /// Dapatkan akses token. Bila token expired, akan otomatis refresh token dan me-return token baru.<br />
        /// Catatan: Hanya panggil function ini di luar dari pre-rendering (tidak dalam OnInitialized()/OnInitializedAsync(), tapi dalam OnAfterRender()/OnAfterRenderAsync())<br />
        /// Karena fungsi ini mengakses local storage untuk set token baru, dan local storage hanya dapat diakses di luar pre-rendering mode.
        /// </summary>
        /// <returns>AuthenticationHeaderValue yang bisa digunakan untuk meng-set _httpClient.DefaultRequestHeaders.Authorization, atau null bila token tidak ada.</returns>
        public async Task<AuthenticationHeaderValue?> GetAccessTokenAsync()
        {
            // Dapatkan token
            var token = await _localStorage.GetItemAsync<string>("authToken");

            // Cek apakah token ada
            if (string.IsNullOrWhiteSpace(token))
            {
                await ClearTokensAsync();
                return null; // token tidak ada
            }
            // Dapatkan expiry dari token
            var expiry = _jwtSecurityTokenHandler.ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
            if (expiry == null)
            {
                await ClearTokensAsync();
                return null; // token ada namun tidak ada expiry (invalid)
            }
            // Dapatkan expiry dari token
            var expiryDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry));
            // Cek apakah token sudah expired (atau 2 menit sebelum expired)
            if (expiryDateTime < DateTimeOffset.UtcNow.AddMinutes(2))
            {
                // Dapatkan refresh token
                var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
                if (string.IsNullOrWhiteSpace(refreshToken))
                {
                    await ClearTokensAsync();
                    return null; // refresh token tidak ada, tidak bisa refresh token
                }
                // Dapatkan token baru dengan refresh token
                var _httpClient = _factory.CreateClient("WebAPI");
                try
                {
                    var request = new RefreshTokenRequestDto
                    {
                        AccessToken = token,
                        RefreshToken = refreshToken
                    };
                    var response = await _httpClient.PostAsJsonAsync("api/auth/refresh-token", request);

                    if (!response.IsSuccessStatusCode)
                    {
                        return null; // API menolak refresh (mungkin token invalid)
                    }
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDto>>();

                    if (apiResponse?.StatusCode != "SUCCESS" || apiResponse.Data == null)
                    {
                        return null; // hasil keluaran API invalid (mungkin token invalid)
                    }
                    await SetTokensAsync(apiResponse.Data.AccessToken, apiResponse.Data.RefreshToken);
                    return new AuthenticationHeaderValue("Bearer", apiResponse.Data.AccessToken);
                }
                catch
                {
                    await ClearTokensAsync();
                    return null;
                }
            }

            return new AuthenticationHeaderValue("Bearer", token);
        }
        private async Task ClearTokensAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
        }
        private async Task SetTokensAsync(string accessToken, string refreshToken)
        {
            await _localStorage.SetItemAsync("authToken", accessToken);
            await _localStorage.SetItemAsync("refreshToken", refreshToken);
        }
    }
}
