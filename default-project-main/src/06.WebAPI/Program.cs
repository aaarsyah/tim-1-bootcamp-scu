// Import Entity Framework Core untuk database operations
using Microsoft.EntityFrameworkCore;
// Import CourseDbContext dari folder Data
using MyApp.WebAPI.Data;
// Import extension methods dari folder Extensions
using MyApp.WebAPI.Extensions;
// Import custom middleware dari folder Middleware
using MyApp.WebAPI.Middleware;
// Import FluentValidation core library
using FluentValidation;
// Import FluentValidation ASP.NET Core integration
using FluentValidation.AspNetCore;
// Import System.Reflection untuk assembly operations
using System.Reflection;

// Buat WebApplicationBuilder - pattern builder untuk konfigurasi aplikasi
// args adalah command line arguments yang diterima aplikasi
var builder = WebApplication.CreateBuilder(args);


// ========== KONFIGURASI SERVICES (DEPENDENCY INJECTION) ==========

// Tambahkan Controllers ke DI container
// Ini akan mendaftarkan semua classes yang inherit dari ControllerBase
builder.Services.AddControllers();

// Daftarkan Database Context dengan SQL Server database
// Pilih provider berdasarkan environment atau configuration
builder.Services.AddSqlServerDatabase(builder.Configuration);

// Daftarkan AutoMapper untuk object-to-object mapping
// Akan scan assembly untuk mencari Profile classes
builder.Services.AddAutoMapperProfiles();

// Konfigurasi FluentValidation
// AddFluentValidationAutoValidation: otomatis validate input sebelum masuk ke controller
builder.Services.AddFluentValidationAutoValidation();
// Daftarkan semua validator classes ke DI container
builder.Services.AddValidators();

// Daftarkan business services (CategoryService, CourseService)
builder.Services.AddApplicationServices();

// Konfigurasi CORS untuk mengizinkan cross-origin requests
builder.Services.AddCorsPolicy();

// ========== KONFIGURASI SWAGGER/OPENAPI ==========

// Tambahkan API Explorer untuk Swagger
// Diperlukan untuk generate OpenAPI specification
builder.Services.AddEndpointsApiExplorer();

// Konfigurasi Swagger UI dan OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    // Definisikan dokumen OpenAPI
    c.SwaggerDoc("v1", new() { 
        Title = "Course API- Apple Music",          // Nama API
        Version = "v1",                 // Versi API
        Description = "A simple e-commerce Course API for Apple Music Website", // Deskripsi API
    });
    
    // Include XML comments untuk dokumentasi yang lebih detail
    // XML comments adalah komentar /// di atas methods
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // Cek apakah file XML comments ada, jika ada include ke Swagger
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// ========== BUILD APLIKASI ==========

// Build aplikasi dengan semua konfigurasi yang sudah didefinisikan
var app = builder.Build();

// ========== AUTO-MIGRATE ALL DATABASES ==========

// Automatically discover and migrate all DbContext instances
// continueOnError: false = stop on first error (recommended for Courseion)
// continueOnError: true = try to migrate all contexts even if one fails
try
{
    var migrationResult = await app.MigrateAllDatabasesAsync(
        continueOnError: app.Environment.IsDevelopment() // Only continue on error in dev
    );
    
    // In Courseion, you might want to fail fast if migrations fail
    if (!app.Environment.IsDevelopment() && !migrationResult.IsSuccessful)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogCritical("🛑 Application startup aborted due to migration failures in Courseion");
        throw new InvalidOperationException("Database migration failed in Courseion environment");
    }
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "💥 Failed to run database migrations");
    
    // In Courseion, you typically want to stop the application
    // In development, you might want to continue without database
    if (!app.Environment.IsDevelopment())
    {
        throw; // Re-throw in Courseion
    }
    
    logger.LogWarning("⚠️ Continuing without database in development mode");
}

// ========== KONFIGURASI HTTP REQUEST PIPELINE ==========

// Jika dalam development mode, aktifkan Swagger
if (app.Environment.IsDevelopment())
{
    // Aktifkan Swagger middleware untuk generate OpenAPI spec
    app.UseSwagger();
    
    // Aktifkan Swagger UI untuk interface web
    app.UseSwaggerUI(c =>
    {
        // Endpoint untuk mengakses OpenAPI specification
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course API V1");
        // RoutePrefix = string.Empty artinya Swagger UI ada di root URL (/)
        c.RoutePrefix = string.Empty; // Swagger UI at root
    });
}

// ========== MIDDLEWARE PIPELINE (URUTAN PENTING!) ==========

// Tambahkan custom exception handling middleware di posisi pertama
// Ini akan catch semua unhandled exceptions dan return response yang konsisten
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Redirect HTTP ke HTTPS
app.UseHttpsRedirection();

// Aktifkan CORS dengan policy "AllowAll" yang sudah dikonfigurasi
app.UseCors("AllowAll");

// Middleware untuk authentication (login/logout)
app.UseAuthentication();

// Middleware untuk authorization (permissions/roles)
app.UseAuthorization();

// Map semua controllers ke routes
// Ini akan mendaftarkan semua controller methods sebagai endpoints
app.MapControllers();

// ========== START APLIKASI ==========

// Jalankan aplikasi - method ini akan block thread dan listen untuk requests
app.Run();