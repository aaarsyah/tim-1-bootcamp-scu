// Import Entity Framework Core untuk database operations
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyApp.WebAPI.Models.Entities;
using System.Text.Json;


// Import Models untuk entities
using Xunit.Sdk;

namespace MyApp.WebAPI.Data
{
    // Untuk membuat: dotnet ef migrations add InitialCreate
    /// Application Database Context with Identity Support
    /// Purpose: Central point for database operations and configuration

    public class AppleMusicDbContext : IdentityDbContext<User, Role, int>
    {
        /// <summary>
        /// Constructor - Menerima DbContextOptions untuk dependency injection
        /// Options akan dikonfigurasi di Program.cs dengan connection string dan provider
        /// </summary>
        /// /// <param name="options">Database context options dari DI container</param>
        public AppleMusicDbContext(DbContextOptions<AppleMusicDbContext> options) : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
    
        public DbSet<Category> Categories { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public override DbSet<User> Users { get; set; }
        public DbSet<MyClass> MyClasses { get; set; } //Participans = MyClass
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
   
        /// <summary>
        /// DbSet untuk Invoices table
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }
        /// <summary>
        /// DbSet untuk InvoiceDetails table
        /// </summary>
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Course
            ConfigureCourse(modelBuilder);
            ConfigureCategory(modelBuilder);
            ConfigureSchedule(modelBuilder);
            // User
            ConfigureUser(modelBuilder);
            ConfigureCartItem(modelBuilder);
            ConfigureMyClass(modelBuilder);
            // Invoice
            ConfigureInvoice(modelBuilder);
            ConfigureInvoiceDetail(modelBuilder);
            // Payment Method
            ConfigurePaymentMethod(modelBuilder);
            // Data seeding
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Configure Course entity dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ConfigureCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(150);
                entity.Property(e => e.Description)
                        .HasMaxLength(3000);
                entity.Property(e => e.ImageUrl)
                        .HasMaxLength(256);
                entity.Property(e => e.Price)
                        .IsRequired()
                        .HasColumnType("numeric(10)");
                entity.Property(e => e.IsActive)
                        .IsRequired()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);
                entity.Property(e => e.CreatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                // Relationship dengan Category
                entity.HasOne(e => e.Category)
                        .WithMany(e => e.Courses)
                        .HasForeignKey(e => e.CategoryId)
                        .OnDelete(DeleteBehavior.Restrict); // Larang penghapusan Category bila ada Courses yang terhubung
                // Relationship dengan Schedule
                entity.HasMany(e => e.Schedules)
                        .WithOne(e => e.Course)
                        .HasForeignKey(e => e.CourseId)
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua Schedule yang terhubung bila Courses dihapus
                // Constraint
                entity.ToTable(e => e.HasCheckConstraint(
                        "CK_Price", "[Price] >= 0"));
            });
        }

        /// <summary>
        /// Configure Category entity dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(20);
                entity.Property(e => e.LongName)
                        .IsRequired()
                        .HasMaxLength(50);
                entity.Property(e => e.Description)
                        .HasMaxLength(1000);
                entity.Property(e => e.ImageUrl)
                        .HasMaxLength(256);
                entity.Property(e => e.IsActive)
                        .IsRequired()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);
                entity.Property(e => e.CreatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                // Tak usah configure relationship sama Courses lagi
            });
        }

        /// <summary>
        /// Configure Schedule entity dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ConfigureSchedule(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedules");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                entity.Property(e => e.Date)
                        .IsRequired()
                        .HasDefaultValueSql("CONVERT (DATE, GETUTCDATE())");
                // Tak usah configure relationship sama Courses lagi
            });
        }

        /// <summary>
        /// Configure User entity dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        
        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            // ===== USER CONFIGURATION =====
            // Configure User entity for Identity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                entity.Property(e => e.RefreshToken)
                        .HasMaxLength(100); // 64 bytes * 8 / 6 = 85.33 characters long in Base64
                entity.Property(e => e.RefreshTokenExpiryTime)
                        .IsRequired();
                entity.Property(e => e.EmailConfirmationToken)
                        .HasMaxLength(100); // 64 bytes * 8 / 6 = 85.33 characters long in Base64
                entity.Property(e => e.EmailConfirmationTokenExpiry)
                        .IsRequired();
                entity.Property(e => e.UserName)
                        .IsRequired() // UserName bisa null di code namun diharuskan dalam database
                        .HasMaxLength(30);
                entity.Property(e => e.NormalizedUserName)
                        .IsRequired() // UserName bisa null di code namun diharuskan dalam database
                        .HasMaxLength(30);
                entity.Property(e => e.Email)
                        .IsRequired()  // Email bisa null di code namun diharuskan dalam database
                        .HasMaxLength(70);
                entity.Property(e => e.NormalizedEmail)
                        .IsRequired()  // Email bisa null di code namun diharuskan dalam database
                        .HasMaxLength(70);
                entity.Property(e => e.EmailConfirmed)
                        .IsRequired()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);
                entity.Property(e => e.CreatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                // Relationship dengan ItemCart
                entity.HasMany<CartItem>()
                        .WithOne(e => e.User)
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua CartItem yang terhubung bila User dihapus
                // Relationship dengan Participant
                entity.HasMany<MyClass>()
                        .WithOne(e => e.User)
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua Participant yang terhubung bila User dihapus
                // Relationship dengan Invoice
                entity.HasMany<Invoice>()
                        .WithOne(e => e.User)
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.SetNull); // Putuskan semua Invoice yang terhubung bila User dihapus
                // Constraint
                entity.ToTable(e => e.HasCheckConstraint(
                        "CK_Email", "[Email] LIKE '%@%.%'"));
            });

            // ===== ROLE CONFIGURATION =====
            // Configure Role entity for Identity
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Description)
                .HasMaxLength(500);
            });

            // ===== USER CLAIM CONFIGURATION =====
            // Configure UserClaim relationship
            modelBuilder.Entity<UserClaim>(entity =>
            {
                // Relationship dengan User
                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserClaims)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        /// <summary>
        /// Configure CartItem entity dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ConfigureCartItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("CartItems");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                // Tak usah configure relationship sama User lagi
                // Relationship dengan Schedule
                entity.HasOne(e => e.Schedule)
                        .WithMany()
                        .HasForeignKey(e => e.ScheduleId)
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua CartItem yang terhubung bila Schedule dihapus
            });
        }
        /// <summary>
        /// Configure Participant entity dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ConfigureMyClass(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyClass>(entity =>
            {
                entity.ToTable("MyClasses");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                // Tak usah configure relationship sama User lagi
                // Relationship dengan Schedule
                entity.HasOne(e => e.Schedule)
                        .WithMany()
                        .HasForeignKey(e => e.ScheduleId)
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua Participant yang terhubung bila Schedule dihapus
            });
        }
        /// <summary>
        /// Configure Invoice entity dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ConfigureInvoice(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoices");
                // Primary key
                entity.HasKey(e => e.Id);
                // Unique Index
                entity.HasIndex(e => e.RefCode).IsUnique();
                // Properties
                entity.Property(e => e.RefCode)
                        .IsRequired()
                        .HasMaxLength(32);
                entity.Property(e => e.CreatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                // Tak usah configure relationship sama User lagi
                // Relationship dengan PaymentMethod
                entity.HasOne(e => e.PaymentMethod)
                        .WithMany()
                        .HasForeignKey(e => e.PaymentMethodId)
                        .OnDelete(DeleteBehavior.SetNull); // Putuskan semua InvoiceDetails yang terhubung bila Invoice dihapus
                // Relationship dengan InvoiceDetails
                entity.HasMany(e => e.InvoiceDetails)
                        .WithOne()
                        .HasForeignKey(e => e.InvoiceId)
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua InvoiceDetails yang terhubung bila Invoice dihapus
            });
        }
        /// <summary>
        /// Configure InvoiceDetail entity dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ConfigureInvoiceDetail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.ToTable("InvoiceDetail");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                // Tak usah configure relationship sama Invoice lagi
                // Relationship dengan Schedule
                entity.HasOne(e => e.Schedule)
                        .WithMany()
                        .HasForeignKey(e => e.ScheduleId)
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua Participant yang terhubung bila Schedule dihapus
                //TODO: diskusi bagaimana dengan invoice bilamana course/schedule dihapus
            });
        }
        /// <summary>
        /// Configure PaymentMethod entity dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ConfigurePaymentMethod(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethods");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(20);
                entity.Property(e => e.LogoUrl)
                        .HasMaxLength(256);
                entity.Property(e => e.CreatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                // Tak usah configure relationship sama Invoice lagi
            });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            string placeholder = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            var seedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Drum",
                    LongName = "Drummer class",
                    Description = placeholder,
                    ImageUrl = "img/Class1.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 2,
                    Name = "Piano",
                    LongName = "Pianist class",
                    Description = placeholder,
                    ImageUrl = "img/Class2.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 3,
                    Name = "Gitar",
                    LongName = "Guitarist class",
                    Description = placeholder,
                    ImageUrl = "img/Class3.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 4,
                    Name = "Bass",
                    LongName = "Bassist class",
                    Description = placeholder,
                    ImageUrl = "img/Class4.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 5,
                    Name = "Biola",
                    LongName = "Violinist class",
                    Description = placeholder,
                    ImageUrl = "img/Class5.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 6,
                    Name = "Menyanyi",
                    LongName = "Singer class",
                    Description = placeholder,
                    ImageUrl = "img/Class6.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 7,
                    Name = "Flute",
                    LongName = "Flutist class",
                    Description = placeholder,
                    ImageUrl = "img/Class7.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 8,
                    Name = "Saxophone",
                    LongName = "Saxophonist class",
                    Description = placeholder,
                    ImageUrl = "img/Class8.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                }
            );
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Name = "Kursus Drummer Special Coach (Eno Netral)",
                    Description = placeholder,
                    ImageUrl = "img/Landing1.svg",
                    Price = 8500000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 1
                },
                new Course
                {
                    Id = 2,
                    Name = "[Beginner] Guitar class for kids",
                    Description = placeholder,
                    ImageUrl = "img/Landing2.svg",
                    Price = 1600000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 3
                },
                new Course
                {
                    Id = 3,
                    Name = "Biola Mid-Level Course",
                    Description = placeholder,
                    ImageUrl = "img/Landing3.svg",
                    Price = 3000000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 5
                },
                new Course
                {
                    Id = 4,
                    Name = "Drummer for kids (Level Basic/1)",
                    Description = placeholder,
                    ImageUrl = "img/Landing4.svg",
                    Price = 2200000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 1
                },
                new Course
                {
                    Id = 5,
                    Name = "Kursu Piano : From Zero to Pro (Full Package)",
                    Description = placeholder,
                    ImageUrl = "img/Landing5.svg",
                    Price = 11650000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 2
                },
                new Course
                {
                    Id = 6,
                    Name = "Expert Level Saxophone",
                    Description = placeholder,
                    ImageUrl = "img/Landing6.svg",
                    Price = 7350000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CategoryId = 8
                }
            );
            modelBuilder.Entity<Schedule>().HasData(
                new Schedule
                {
                    Id = 1,
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 2,
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 3,
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 4,
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 5,
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 6,
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 7,
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 8,
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 9,
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 10,
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 11,
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 12,
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 13,
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 14,
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 15,
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 16,
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 17,
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 18,
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 19,
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 20,
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 21,
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 22,
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 23,
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 24,
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 25,
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 26,
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 27,
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 28,
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 29,
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 30,
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 31,
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 6
                },
                new Schedule
                {
                    Id = 32,
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 6
                },
                new Schedule
                {
                    Id = 33,
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 6
                },
                new Schedule
                {
                    Id = 34,
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 6
                },
                new Schedule
                {
                    Id = 35,
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 6
                },
                new Schedule
                {
                    Id = 36,
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 6
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    ConcurrencyStamp = "fa1d2205-2888-40cc-89ab-6cc77359b442", // GUID dibuat static supaya database seed tidak berubah-ubah
                    UserName = "Super Admin",
                    NormalizedUserName = "SUPER ADMIN",
                    Email = "admin@applemusic.com",
                    NormalizedEmail = "ADMIN@APPLEMUSIC.COM",
                    EmailConfirmed = true,
                    SecurityStamp = string.Empty, // akan error jika null
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 2,
                    ConcurrencyStamp = "0090b440-14cd-4b62-a18a-8bb7385dda8f", // GUID dibuat static supaya database seed tidak berubah-ubah
                    UserName = "Nur Imam Iskandar",
                    NormalizedUserName = "NUR IMAM ISKANDAR",
                    Email = "nurimamiskandar@gmail.com",
                    NormalizedEmail = "NURIMAMISKANDAR@GMAIL.COM",
                    EmailConfirmed = true,
                    SecurityStamp = string.Empty, // akan error jika null
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                }
                ,
                new User
                {
                    Id = 3,
                    ConcurrencyStamp = "33462271-0ff4-44bb-92bc-d21292725a8a", // GUID dibuat static supaya database seed tidak berubah-ubah
                    UserName = "Iskandar",
                    NormalizedUserName = "ISKANDAR",
                    Email = "imam.stmik15@gmail.com",
                    NormalizedEmail = "IMAM.STMIK15@GMAIL.COM",
                    EmailConfirmed = true,
                    SecurityStamp = string.Empty, // akan error jika null
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 4,
                    ConcurrencyStamp = "14a2685e-9aba-48f8-8245-48ca94320551", // GUID dibuat static supaya database seed tidak berubah-ubah
                    UserName = "Dummy User",
                    NormalizedUserName = "DUMMY USER",
                    Email = "iniemaildummysaya@gmail.com",
                    NormalizedEmail = "INIEMAILDUMMYSAYA@GMAIL.COM",
                    EmailConfirmed = false,
                    SecurityStamp = string.Empty, // akan error jika null
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 5,
                    ConcurrencyStamp = "8f45a10e-8bed-407d-ba76-5e443d458c72", // GUID dibuat static supaya database seed tidak berubah-ubah
                    UserName = "yusri sahrul",
                    NormalizedUserName = "YUSRI SAHRUL",
                    Email = "yusrisahrul.works@gmail.com",
                    NormalizedEmail = "YUSRISAHRUL.WORKS@GMAIL.COM",
                    EmailConfirmed = true,
                    SecurityStamp = string.Empty, // akan error jika null
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 6,
                    ConcurrencyStamp = "aef5f553-4d33-4539-9c51-94f9eb2e8624", // GUID dibuat static supaya database seed tidak berubah-ubah
                    UserName = "yusri sahrul test",
                    NormalizedUserName = "YUSRI SAHRUL TEST",
                    Email = "yusribootcamp@gmail.com",
                    NormalizedEmail = "YUSRIBOOTCAMP@GMAIL.COM",
                    EmailConfirmed = true,
                    SecurityStamp = string.Empty, // akan error jika null
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            //Seeding ROLE
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "Administrator with management access",
                    CreatedAt = seedDate,
                    ConcurrencyStamp = "b2c3d4e5-f6a7-8901-bcde-f12345678901"
                },
                new Role
                {
                    Id = 2,
                    Name = "User",
                    NormalizedName = "USER",
                    Description = "Standard user with basic access",
                    CreatedAt = seedDate,
                    ConcurrencyStamp = "d4e5f6a7-b8c9-0123-def1-234567890123"
                }
            );


            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod
                {
                    Id = 1,
                    Name = "Gopay",
                    LogoUrl = "img/Payment1.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new PaymentMethod
                {
                    Id = 2,
                    Name = "OVO",
                    LogoUrl = "img/Payment2.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new PaymentMethod
                {
                    Id = 3,
                    Name = "DANA",
                    LogoUrl = "img/Payment3.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new PaymentMethod
                {
                    Id = 4,
                    Name = "Mandiri",
                    LogoUrl = "img/Payment4.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new PaymentMethod
                {
                    Id = 5,
                    Name = "BCA",
                    LogoUrl = "img/Payment5.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                },
                new PaymentMethod
                {
                    Id = 6,
                    Name = "BNI",
                    LogoUrl = "img/Payment6.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }

        /// <summary>
        /// Override SaveChangesAsync untuk implement audit trail functionality
        /// Automatically update UpdatedAt timestamps saat entities di-modify
        /// 
        /// ChangeTracker Features:
        /// - Tracks entity state changes (Added, Modified, Deleted, etc.)
        /// - Allows custom logic sebelum save ke database
        /// - Useful untuk audit trails, soft deletes, validation, etc.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token untuk async operations</param>
        /// <returns>Number of affected rows</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ========== AUDIT TRAIL UNTUK PRODUCTS ==========

            // Get semua Product entities yang sedang di-track dan dalam state Modified
            var modifiedCourse = ChangeTracker.Entries<Course>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in modifiedCourse)
            {
                // Update timestamp saat entity di-modify
                entry.Entity.UpdatedAt = DateTime.UtcNow;

                // Optional: Log changes untuk debugging atau audit purposes
                // LogEntityChanges(entry);
            }

            // ========== AUDIT TRAIL UNTUK CATEGORIES ==========

            var modifiedCategories = ChangeTracker.Entries<Category>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in modifiedCategories)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            // ========== ADDITIONAL BUSINESS LOGIC ==========

            // Contoh: Validate business rules sebelum save
            // ValidateBusinessRules();

            // Contoh: Handle soft deletes
            // HandleSoftDeletes();

            // Call base SaveChangesAsync untuk actually persist changes ke database
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Helper method untuk log entity changes (optional, untuk debugging)
        /// Berguna untuk tracking changes dalam development environment
        /// </summary>
        /// <param name="entry">Entity entry yang berubah</param>
        private void LogEntityChanges(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        {
            if (entry.State == EntityState.Modified)
            {
                foreach (var property in entry.Properties)
                {
                    if (property.IsModified)
                    {
                        var originalValue = property.OriginalValue;
                        var currentValue = property.CurrentValue;
                        // Log: Property {property.Metadata.Name} changed from {originalValue} to {currentValue}
                    }
                }
            }
        }
    }
}