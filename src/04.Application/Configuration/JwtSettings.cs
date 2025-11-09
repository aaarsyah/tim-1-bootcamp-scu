namespace MyApp.Application.Configuration
{
    /// <summary>
    /// JWT Configuration Settings
    /// Menyimpan konfigurasi JWT untuk authentication dan authorization
    /// </summary>
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int AccessTokenExpirationMinutes { get; set; } //Set di appsettings.json
        public int RefreshTokenExpirationDays { get; set; }
        public int RefreshTokenRememberMeExpirationDays { get; set; }
        public bool ValidateIssuer { get; set; } = true;
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; } = true;
        public int ClockSkew { get; set; } = 5; // minutes
    }
}
