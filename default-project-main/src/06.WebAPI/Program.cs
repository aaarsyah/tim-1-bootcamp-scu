// Import Entity Framework Core untuk database operations
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyApp.Application.Feature.Authentications.Commands;
using MyApp.Application.Mappings;
using MyApp.Infrastructure.Configuration;
// Import AppleMusicDbContext dari folder Data
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Infrastructure.Data.Repositories.Interfaces;

// Import extension methods dari folder Extensions
using MyApp.WebAPI.Extensions;
// Import custom middleware dari folder Middleware
using MyApp.WebAPI.Middleware;
using MyApp.WebAPI.Services;
using Serilog;
using Serilog.Events;
// Import FluentValidation ASP.NET Core integration
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
// Import System.Reflection untuk assembly operations
using System.Reflection;
using System.Text;

// ===== STEP 1: CONFIGURE SERILOG LOGGING =====
// Purpose: Setup structured logging before anything else
// Logs go to: Console (for development) and File (for production)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Log Information and above
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Less noise from framework
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext() // Include contextual properties (like CorrelationId)
    .Enrich.WithProperty("Application", "AppleMusicAPI")
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day, // New file each day
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
        retainedFileCountLimit: 30) // Keep last 30 days
    .CreateLogger();

try
{
    
    Log.Information("Starting  AppleMusic API");
    // Buat WebApplicationBuilder - pattern builder untuk konfigurasi aplikasi
    // args adalah command line arguments yang diterima aplikasi
    var builder = WebApplication.CreateBuilder(args);

    // TODO: Pasang license key (gratis) untuk AutoMapper dan MediatR
    builder.Logging.AddFilter("LuckyPennySoftware.AutoMapper.License", LogLevel.None);
    builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);

    // ===== STEP 2: USE SERILOG FOR LOGGING =====
    builder.Host.UseSerilog();

    // ===== STEP 3: CONFIGURE JWT SETTINGS =====
    var jwtSettings = new JwtSettings();
    builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
    builder.Services.AddSingleton(jwtSettings);

    // ===== STEP 4: CONFIGURE DATABASE =====
    // Purpose: Setup Entity Framework with SQL Server
    // Connection string from appsettings.json
    builder.Services.AddDbContext<AppleMusicDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            sqlOptions =>
            {
                // Enable retry on transient failures (network issues, etc)
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            }));

    // ===== STEP 5: CONFIGURE IDENTITY =====
    // Purpose: Setup ASP.NET Core Identity for user management
    /*builder.Services.AddIdentity<User, Role>(options =>
    {
        // Password settings
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings (protect against brute force attacks)
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false; // set true untuk demo

        options.User.AllowedUserNameCharacters += " "; // Izinkan huruf spasi dalam Username
    })
    .AddEntityFrameworkStores<AppleMusicDbContext>()
    .AddDefaultTokenProviders();*/

    // ===== STEP 6: CONFIGURE JWT AUTHENTICATION =====
    // Purpose: Setup JWT Bearer authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = jwtSettings.ValidateIssuer,
            ValidateAudience = jwtSettings.ValidateAudience,
            ValidateLifetime = jwtSettings.ValidateLifetime,
            ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ClockSkew = TimeSpan.FromMinutes(jwtSettings.ClockSkew)
        };

        // Logging JWT events for debugging
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Log.Warning("JWT authentication failed: {Exception}", context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var userId = context.Principal?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                Log.Information("JWT token validated for user: {UserId}", userId);
                return Task.CompletedTask;
            }
        };
    });

    // ===== STEP 7: CONFIGURE AUTHORIZATION =====
    // Purpose: Setup authorization policies
    builder.Services.AddAuthorization(AuthorizationPolicies.ConfigurePolicies);

    builder.Services.AddValidators();

    //DI Service
    builder.Services.AddApplicationServices();

    // Register Repositori dan Unit-Of-Work
    builder.Services
        .AddApplicationRepositories()
        .AddApplicationUnitOfWork();

    // Register MediatR for CQRS
    builder.Services.AddMediatR(cfg => {
        cfg.LicenseKey = string.Empty; // TODO: Pasang license key (nanti saja kalau ada waktu)
        cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly);
    }
    );

    // ===== STEP 9: ADD CONTROLLERS =====
    builder.Services.AddControllers();

    builder.Services.AddSqlServerDatabase(builder.Configuration);

    builder.Services.AddAutoMapper(cfg => {
        cfg.LicenseKey = string.Empty; // TODO: Pasang license key (nanti saja kalau ada waktu)
        cfg.AddProfile<MappingProfile>();
    });



    builder.Services.AddFluentValidationAutoValidation(configuration =>
    {
        // Isi konfigurasi di sini
    });


    builder.Services.AddCorsPolicy();

    // ===== STEP 10: ADD API DOCUMENTATION WITH JWT SUPPORT =====
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new()
        {
            Title = "API - Apple Music",     // Nama API
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

        // Add JWT Authentication to Swagger
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          Example: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
    });

    // // Register Caching
    // builder.Services.AddMemoryCache();

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


    // ===== STEP 11: CONFIGURE MIDDLEWARE PIPELINE =====
    // Order matters! Each middleware processes in order

    // Swagger (API documentation) - Only in development
    if (app.Environment.IsDevelopment())
    {
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

    // Add Correlation ID to each request
    app.UseMiddleware<CorrelationIdMiddleware>();

    // Ini akan catch semua unhandled exceptions dan return response yang konsisten
    // app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();

    // IMPORTANT: Authentication must come before Authorization
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllers();

    // ===== STEP 12: INITIALIZE DATABASE =====
    // Purpose: Create database and seed initial data
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppleMusicDbContext>();
        
        try
        {
            // Create database if it doesn't exist and apply migrations
            Log.Information("Ensuring database is created...");
            context.Database.EnsureCreated();
            Log.Information("Database ready with Identity tables and default roles");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error creating database");
            throw;
        }
    }

    // ===== STEP 13: START APPLICATION =====
    Log.Information("Application started successfully with Authentication & Authorization");
    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}