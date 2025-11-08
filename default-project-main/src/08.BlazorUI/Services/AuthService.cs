using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace MyApp.BlazorUI.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _factory;
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public AuthService(
        IHttpClientFactory factory,
        ILocalStorageService localStorage,
        AuthenticationStateProvider authStateProvider)
    {
        _factory = factory;
        _localStorage = localStorage;
        //_authStateProvider = authStateProvider;
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

                if (apiResponse?.Data != null)
                {
                    await SetTokensAsync(apiResponse.Data.AccessToken, apiResponse.Data.RefreshToken);

                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", apiResponse.Data.AccessToken);

                    //((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(apiResponse.Data.AccessToken);

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
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                if (problemDetails == null)
                {
                    // Unknown error
                    return false;
                }
                return false;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return apiResponse != null;
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
            return apiResponse != null;
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
            return apiResponse != null;
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
            return apiResponse != null;
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
            return apiResponse != null;
        }
        catch
        {
            return false;
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
                // mungkin error karena token invalid, buang token dan anggap user sudah logout
                await ClearTokensAsync();

                //((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
                return;
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
            if (apiResponse == null)
            {
                // mungkin error karena token invalid, buang token dan anggap user sudah logout
                await ClearTokensAsync();

                //((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
                return;
            }
            //Berhasil
            await ClearTokensAsync();

            //((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
            return;
        }
        catch
        {
            // mungkin error karena putus koneksi? harusnya tidak akan terjadi ini
            return;
        }

    }
    private async Task<string?> GetAccessTokenAsync()
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

        // Uncomment line ini untuk debug
        Console.WriteLine($"Now: {DateTimeOffset.UtcNow} (+ 2 minutes = {DateTimeOffset.UtcNow.AddMinutes(2)}) Expiry time: {expiryDateTime}");

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

                if (apiResponse?.Data == null)
                {
                    return null; // hasil keluaran API invalid (mungkin token invalid)
                }
                await SetTokensAsync(apiResponse.Data.AccessToken, apiResponse.Data.RefreshToken);
                return apiResponse.Data.AccessToken;
            }
            catch
            {
                await ClearTokensAsync();
                return null;
            }
        }
        return token;
    }
    public async Task<bool> IsLoggedInAsync()
    {
        return await GetAccessTokenAsync() != null;
    }
    public async Task<bool> IsAdminAsync()
    {
        var token = await GetAccessTokenAsync();
        if (token == null)
        {
            return false;
        }
        var role = _jwtSecurityTokenHandler.ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "role")?.Value;
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
    public async Task<AuthenticationHeaderValue?> GetAuthAsync()
    {
        var a = await GetAccessTokenAsync();
        if (a == null)
        {
            return null;
        }
        return new AuthenticationHeaderValue("Bearer", a);
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
