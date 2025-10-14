using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Data;
using MyApp.WebAPI.Services;
using MyApp.WebAPI.Configuration;
using FluentValidation;
using System.Reflection;


namespace MyApp.WebAPI.Extensions
{
    /// <summary>
    /// Extension methods untuk IServiceCollection - memudahkan konfigurasi dependency injection
    /// Static class yang berisi method-method untuk mendaftarkan services ke DI container
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Method untuk mendaftarkan business services ke DI container
        /// </summary>
        /// <param name="services">IServiceCollection dari ASP.NET Core DI container</param>
        /// <returns>IServiceCollection untuk method chaining</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Daftarkan CategoryService dengan lifetime Scoped (satu instance per HTTP request)
            // Interface ICategoryService akan di-resolve ke implementasi CategoryService
            services.AddScoped<ICategoryService, CategoryService>();
            
            // Daftarkan ProductService dengan lifetime Scoped
            // Interface IProductService akan di-resolve ke implementasi ProductService
            // Purpose: Setup dependency injection

            services.AddScoped<ICourseService, CourseService>();

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IMyClassService, MyClassService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();


            // Return services untuk method chaining (builder pattern)
            return services;
        }

        /// <summary>
        /// Method untuk konfigurasi database context dan connection
        /// </summary>
        /// <param name="services">IServiceCollection container</param>
        /// <param name="configuration">IConfiguration untuk membaca appsettings.json</param>
        /// <returns>IServiceCollection untuk method chaining</returns>
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Daftarkan ProductDbContext ke DI container dengan In-Memory database
            // In-Memory database: data disimpan di RAM, hilang ketika aplikasi restart
            // Cocok untuk demo dan testing, tidak untuk Production
            services.AddDbContext<AppleMusicDbContext>(options =>
                options.UseInMemoryDatabase("AppleMusicDb")); // "CourseApiDb" adalah nama database
            
            // ALTERNATIF: Untuk SQL Server (Production), uncomment baris berikut:
            // services.AddDbContext<ProductDbContext>(options =>
            //     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            // GetConnectionString akan membaca connection string dari appsettings.json
            
            return services;
        }

        /// <summary>
        /// Method untuk konfigurasi SQL Server database dengan Entity Framework Core
        /// Code First approach dengan advanced configuration dan migration support
        /// Recommended untuk Production applications
        /// </summary>
        /// <param name="services">IServiceCollection container</param>
        /// <param name="configuration">IConfiguration untuk membaca connection strings</param>
        /// <returns>IServiceCollection untuk method chaining</returns>
        public static IServiceCollection AddSqlServerDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Daftarkan ProductDbContext dengan SQL Server provider
            services.AddDbContext<AppleMusicDbContext>(options =>
            {
                // Ambil connection string dari appsettings.json
                // Jika tidak ada, gunakan LocalDB default untuk development
                var connectionString = configuration.GetConnectionString("DefaultConnection") 
                    ?? "Server=localhost\\SQLEXPRESS;Database=AppleMusicDb;Trusted_Connection=True;TrustServerCertificate=True";
                
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
                    // sqlOptions.MigrationsAssembly("MyApp.WebAPI.Migrations");
                    
                    // Set migration history table name (default: __EFMigrationsHistory)
                    // sqlOptions.MigrationsHistoryTable("EFMigrationsHistory", "dbo");
                });
                
                // ========== DEVELOPMENT SETTINGS ==========
                
                // Hanya aktifkan detailed logging dalam development environment
                var serviceProvider = services.BuildServiceProvider();
                var environment = serviceProvider.GetService<IWebHostEnvironment>();
                
                if (environment?.IsDevelopment() == true)
                {
                    // Log sensitive data (parameter values) - DEVELOPMENT ONLY
                    options.EnableSensitiveDataLogging();
                    
                    // Enable detailed error messages
                    options.EnableDetailedErrors();
                    
                    // Log SQL queries ke console untuk debugging
                    options.LogTo(Console.WriteLine, new[] 
                    { 
                        Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuted,
                        Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandError
                    }, LogLevel.Information);
                    
                    // Enable service validation untuk catch DI configuration errors
                    options.EnableServiceProviderCaching();
                    options.EnableThreadSafetyChecks();
                }
                
                // ========== PERFORMANCE SETTINGS ==========
                
                // Configure tracking behavior untuk better performance
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // Global no-tracking
            });
            
            
            return services;
        }

        /// <summary>
        /// Method untuk mendaftarkan FluentValidation validators
        /// </summary>
        /// <param name="services">IServiceCollection container</param>
        /// <returns>IServiceCollection untuk method chaining</returns>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            // Scan assembly saat ini dan daftarkan semua classes yang implement AbstractValidator
            // Assembly.GetExecutingAssembly() = assembly dimana code ini dijalankan
            // Akan menemukan: CreateCategoryDtoValidator, UpdateCategoryDtoValidator, dll
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            return services;
        }

        /// <summary>
        /// Method untuk konfigurasi CORS (Cross-Origin Resource Sharing)
        /// </summary>
        /// <param name="services">IServiceCollection container</param>
        /// <returns>IServiceCollection untuk method chaining</returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            // Tambahkan CORS services ke DI container
            services.AddCors(options =>
            {
                // Definisikan policy bernama "AllowAll"
                options.AddPolicy("AllowAll", policy =>
                {
                    // Izinkan request dari origin manapun (domain manapun)
                    // PERHATIAN: Untuk Production, ganti dengan domain spesifik
                    policy.AllowAnyOrigin()
                          // Izinkan semua HTTP methods (GET, POST, PUT, DELETE, dll)
                          .AllowAnyMethod()
                          // Izinkan semua headers dalam request
                          .AllowAnyHeader();
                });
            });
            
            return services;
        }

        /// <summary>
        /// Method untuk menambahkan AutoMapper ke DI container
        /// </summary>
        /// <param name="services">IServiceCollection container</param>
        /// <returns>IServiceCollection untuk method chaining</returns>
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            // Scan assembly saat ini untuk mencari classes yang inherit dari Profile
            // Akan menemukan MappingProfile yang berisi mapping configuration
            // AutoMapper digunakan untuk convert antara Entity dan DTO
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            return services;
        }

        /// <summary>
        /// Method untuk konfigurasi SQLite database dengan Entity Framework Core
        /// Cross-platform alternative untuk SQL Server, ideal untuk development dan small applications
        /// SQLite advantages: Zero configuration, self-contained, file-based database
        /// </summary>
        /// <param name="services">IServiceCollection container</param>
        /// <param name="configuration">IConfiguration untuk membaca settings</param>
        /// <returns>IServiceCollection untuk method chaining</returns>
        public static IServiceCollection AddSqliteDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppleMusicDbContext>(options =>
            {
                // SQLite connection string - database will be created as file
                var connectionString = configuration.GetConnectionString("SqliteConnection") 
                    ?? "Data Source=AppleMusicDb.sqlite";
                
                // Configure SQLite dengan optimizations
                options.UseSqlite(connectionString, sqliteOptions =>
                {
                    // Set command timeout
                    sqliteOptions.CommandTimeout(30);
                    
                    // Configure migration assembly jika diperlukan
                    // sqliteOptions.MigrationsAssembly("MyApp.WebAPI.Migrations");
                });
                
                // Development settings
                var serviceProvider = services.BuildServiceProvider();
                var environment = serviceProvider.GetService<IWebHostEnvironment>();
                
                if (environment?.IsDevelopment() == true)
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                    options.LogTo(Console.WriteLine, LogLevel.Information);
                }
            });
            
            return services;
        }

        /// <summary>
        /// Method untuk konfigurasi database provider secara dynamic
        /// Allows switching between SQLite, SQL Server, dan In-Memory berdasarkan configuration
        /// Useful untuk different environments (Development, Testing, Production)
        /// </summary>
        /// <param name="services">IServiceCollection container</param>
        /// <param name="configuration">IConfiguration untuk membaca settings</param>
        /// <returns>IServiceCollection untuk method chaining</returns>
        public static IServiceCollection AddDynamicDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Read database provider dari configuration
            var databaseProvider = configuration.GetValue<string>("DatabaseProvider") ?? "SqlServer";
            
            switch (databaseProvider.ToLower())
            {
                case "sqlite":
                    Console.WriteLine("🗄️ Using SQLite database provider");
                    return services.AddSqliteDatabase(configuration);
                
                case "sqlserver":
                    Console.WriteLine("🗄️ Using SQL Server database provider");
                    return services.AddSqlServerDatabase(configuration);
                
                case "inmemory":
                    Console.WriteLine("🗄️ Using In-Memory database provider");
                    return services.AddDatabaseContext(configuration);
                
                default:
                    Console.WriteLine($"⚠️ Unknown database provider '{databaseProvider}', defaulting to SQL Server");
                    return services.AddSqlServerDatabase(configuration);
            }
        }
    } // End of ServiceCollectionExtensions class
} // End of MyApp.WebAPI.Extensions namespace