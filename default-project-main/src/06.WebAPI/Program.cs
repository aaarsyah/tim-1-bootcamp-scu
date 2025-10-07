using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Untuk sementara AddDBContext diletakkan disini
// TODO: Pindahkan ke ServiceCollectionExtensions.cs nanti
builder.Services.AddDbContext<AppleMusicDbContext>(options =>
{
    // Ambil connection string dari appsettings.json
    // Jika tidak ada, gunakan LocalDB default untuk development
    var connectionString = builder.Configuration.GetConnectionString("LocalSqlServer");

    // Configure SQL Server dengan advanced options
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        // ========== CONNECTION RESILIENCE ==========

        // Enable automatic retry untuk transient failures
        // Berguna untuk handle network issues, timeouts, dll
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,                    // Retry maksimal 3 kali
            maxRetryDelay: TimeSpan.FromSeconds(30), // Max delay 30 detik between retries
            errorNumbersToAdd: null);                // Additional SQL error numbers untuk retry

        // ========== COMMAND CONFIGURATION ==========

        // Set command timeout untuk queries (30 seconds default)
        // Increase jika ada long-running queries/migrations
        sqlOptions.CommandTimeout(60); // 60 seconds timeout

        // ========== MIGRATION CONFIGURATION ==========

        // Specify assembly untuk migrations (jika migrations di assembly terpisah)
        // sqlOptions.MigrationsAssembly("WebApplication1.Migrations");

        // Set migration history table name (default: __EFMigrationsHistory)
        // sqlOptions.MigrationsHistoryTable("EFMigrationsHistory", "dbo");
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
