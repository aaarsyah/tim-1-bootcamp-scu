// Import Entity Framework Core untuk database operations
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Models.Entities;
using System.Text.Json;


// Import Models untuk entities
using Xunit.Sdk;

namespace MyApp.WebAPI.Data
{
    // Untuk membuat: dotnet ef migrations add InitialCreate
    /// Application Database Context with Identity Support
    /// Purpose: Central point for database operations and configuration

    public class AppleMusicDbContext : DbContext
    {
        private const int MAX_USERNAME_LENGTH = 64;
        private const int MAX_EMAIL_LENGTH = 256; // Mengikuti jawaban ini: https://stackoverflow.com/a/7717596
        private const int MAX_URL_LENGTH = 256;
        private const int MAX_DESCRIPTION_LENGTH = 4000;
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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }

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
            ConfigureUserRole(modelBuilder);
            ConfigureCartItem(modelBuilder);
            ConfigureMyClass(modelBuilder);
            // Invoice
            ConfigureInvoice(modelBuilder);
            ConfigureInvoiceDetail(modelBuilder);
            // Payment Method
            ConfigurePaymentMethod(modelBuilder);
            // Audit Log
            ConfigureAuditLog(modelBuilder);
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
                // Base Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId)
                    .IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                // Properties
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(e => e.Description)
                        .HasMaxLength(MAX_DESCRIPTION_LENGTH);
                entity.Property(e => e.ImageUrl)
                        .HasMaxLength(MAX_URL_LENGTH);
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
                // Base Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId)
                    .IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                // Properties
                entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(20);
                entity.Property(e => e.LongName)
                        .IsRequired()
                        .HasMaxLength(50);
                entity.Property(e => e.Description)
                        .HasMaxLength(MAX_DESCRIPTION_LENGTH);
                entity.Property(e => e.ImageUrl)
                        .HasMaxLength(MAX_URL_LENGTH);
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
                // Base Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId)
                    .IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
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
                // Auditable Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId).IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(MAX_USERNAME_LENGTH)
                    .HasDefaultValue("System");
                entity.Property(e => e.UpdatedAt);
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(MAX_USERNAME_LENGTH);
                // Properties
                entity.Property(e => e.IsActive)
                        .IsRequired()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);
                entity.Property(e => e.RefreshToken)
                        .HasMaxLength(100); // 64 bytes * 8 / 6 = 85.33 characters long in Base64
                entity.Property(e => e.RefreshTokenExpiry)
                        .IsRequired();
                entity.Property(e => e.EmailConfirmationToken)
                        .HasMaxLength(100); // 64 bytes * 8 / 6 = 85.33 characters long in Base64
                entity.Property(e => e.EmailConfirmationTokenExpiry)
                        .IsRequired();
                entity.Property(e => e.PasswordResetToken)
                        .HasMaxLength(100); // 64 bytes * 8 / 6 = 85.33 characters long in Base64
                entity.Property(e => e.PasswordResetTokenExpiry)
                        .IsRequired();
                entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(MAX_USERNAME_LENGTH);
                entity.Property(e => e.Email)
                        .IsRequired()
                        .HasMaxLength(MAX_EMAIL_LENGTH);
                entity.Property(e => e.PasswordHash)
                        .IsRequired()
                        .HasMaxLength(256); // berapa panjangnya ya?
                entity.Property(e => e.EmailConfirmed)
                        .IsRequired()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);
                entity.Property(e => e.FailedLoginAttempts)
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);
                entity.Property(e => e.LockoutEnd)
                        .HasDefaultValueSql(null);
                entity.Property(e => e.LastLoginAt)
                        .HasDefaultValueSql(null);

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

            
        }
        /// <summary>
        /// Configure Role entity dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>

        private void ConfigureUserRole(ModelBuilder modelBuilder)
        {
            // ===== ROLE CONFIGURATION =====
            // Configure Role entity for Identity
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
                // Auditable Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId).IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(MAX_USERNAME_LENGTH)
                    .HasDefaultValue("System");
                entity.Property(e => e.UpdatedAt);
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(MAX_USERNAME_LENGTH);
                // Properties
                entity.Property(e => e.Name)
                    .HasMaxLength(30);
                entity.Property(e => e.Description)
                    .HasMaxLength(300);
            });
            // ===== USER ROLE CONFIGURATION =====
            // Configure User role many-to-many
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");
                // Auditable Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId).IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(MAX_USERNAME_LENGTH)
                    .HasDefaultValue("System");
                entity.Property(e => e.UpdatedAt);
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(MAX_USERNAME_LENGTH);
                // Relationship dengan ItemCart
                entity.HasOne(e => e.User)
                    .WithMany(e => e.UserRoles)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Larang penghapusan User bila ada UserRoles yang terhubung
                // Relationship dengan Participant
                entity.HasOne(e => e.Role)
                    .WithMany(e => e.UserRoles)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Restrict); // Larang penghapusan Role bila ada UserRoles yang terhubung
            });
            // ===== USER CLAIM CONFIGURATION =====
            // Configure UserClaim relationship
            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.ToTable("UserClaims");
                // Auditable Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId).IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(MAX_USERNAME_LENGTH)
                    .HasDefaultValue("System");
                entity.Property(e => e.UpdatedAt);
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(MAX_USERNAME_LENGTH);
                // Relationship dengan User
                entity.HasOne(e => e.User)
                    .WithMany(e2 => e2.UserClaims)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable("RoleClaims");
                // Auditable Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId).IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(MAX_USERNAME_LENGTH)
                    .HasDefaultValue("System");
                entity.Property(e => e.UpdatedAt);
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(MAX_USERNAME_LENGTH);
                // Relationship dengan User
                entity.HasOne(e => e.Role)
                    .WithMany(u => u.RoleClaims)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);  // Larang penghapusan Role bila ada RoleClaims yang terhubung
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
                // Base Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId)
                    .IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
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
                // Base Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId)
                    .IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
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
                // Base Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId)
                    .IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                // Properties
                entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(20);
                entity.Property(e => e.LogoUrl)
                        .HasMaxLength(MAX_URL_LENGTH);
                entity.Property(e => e.CreatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                // Tak usah configure relationship sama Invoice lagi
            });
        }


        /// <summary>
        /// Configure Audit log dengan advanced Fluent API features
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>

        private void ConfigureAuditLog(ModelBuilder modelBuilder)
        {
            // ===== ROLE CONFIGURATION =====
            // Configure Role entity for Identity
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLogs");
                // Base Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId)
                    .IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                // Properties
                entity.Property(e => e.Action)
                    .HasMaxLength(300);
                entity.Property(e => e.TimeOfAction)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.EntityName)
                    .IsRequired()
                    .HasMaxLength(MAX_USERNAME_LENGTH);
                entity.Property(e => e.OldValues)
                    .HasMaxLength(300);
                entity.Property(e => e.NewValues)
                    .HasMaxLength(300);
                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(300);
                entity.Property(e => e.UserAgent)
                    .IsRequired()
                    .HasMaxLength(300);
                // Relationship dengan User
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.EntityId)
                    .OnDelete(DeleteBehavior.SetNull); // Putuskan semua AuditLog yang terhubung bila User dihapus
            });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            string placeholder = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            var seedDate = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc);
            
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    RefId = new Guid("a2f2a74c-9819-4051-852e-93e859c54661"),
                    Name = "Drum",
                    LongName = "Drummer class",
                    Description = placeholder,
                    ImageUrl = "img/Class1.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System"
                },
                new Category
                {
                    Id = 2,
                    RefId = new Guid("407c4bf0-7f0c-44fc-b3ad-9a4f18a75f29"),
                    Name = "Piano",
                    LongName = "Pianist class",
                    Description = placeholder,
                    ImageUrl = "img/Class2.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System"
                },
                new Category
                {
                    Id = 3,
                    RefId = new Guid("90aa4952-165f-4225-98e4-5a569b83aa8c"),
                    Name = "Gitar",
                    LongName = "Guitarist class",
                    Description = placeholder,
                    ImageUrl = "img/Class3.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System"
                },
                new Category
                {
                    Id = 4,
                    RefId = new Guid("3b470c4e-d847-43b1-9784-a8cc2082cf9e"),
                    Name = "Bass",
                    LongName = "Bassist class",
                    Description = placeholder,
                    ImageUrl = "img/Class4.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System"
                },
                new Category
                {
                    Id = 5,
                    RefId = new Guid("9b648b45-327b-44ce-853a-c4ce17b7fd20"),
                    Name = "Biola",
                    LongName = "Violinist class",
                    Description = placeholder,
                    ImageUrl = "img/Class5.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System"
                },
                new Category
                {
                    Id = 6,
                    RefId = new Guid("e4ea4121-bf90-4ba9-bc87-2c106c6acbbe"),
                    Name = "Menyanyi",
                    LongName = "Singer class",
                    Description = placeholder,
                    ImageUrl = "img/Class6.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System"
                },
                new Category
                {
                    Id = 7,
                    RefId = new Guid("595b8600-4d24-44dc-9e40-6dfd87892cfa"),
                    Name = "Flute",
                    LongName = "Flutist class",
                    Description = placeholder,
                    ImageUrl = "img/Class7.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System"
                },
                new Category
                {
                    Id = 8,
                    RefId = new Guid("e17ec48c-b705-4448-b916-9867d292e517"),
                    Name = "Saxophone",
                    LongName = "Saxophonist class",
                    Description = placeholder,
                    ImageUrl = "img/Class8.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System"
                }
            );
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    RefId = new Guid("2467c3ca-ea46-4720-b590-aeb81ff50ea1"),
                    Name = "Kursus Drummer Special Coach (Eno Netral)",
                    Description = placeholder,
                    ImageUrl = "img/Landing1.svg",
                    Price = 8500000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 1
                },
                new Course
                {
                    Id = 2,
                    RefId = new Guid("d38adc18-7b11-46a1-9417-a2f64b2adcd2"),
                    Name = "[Beginner] Guitar class for kids",
                    Description = placeholder,
                    ImageUrl = "img/Landing2.svg",
                    Price = 1600000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 3
                },
                new Course
                {
                    Id = 3,
                    RefId = new Guid("b298d7f3-5f95-403a-bbc5-60f6d4124dd1"),
                    Name = "Biola Mid-Level Course",
                    Description = placeholder,
                    ImageUrl = "img/Landing3.svg",
                    Price = 3000000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 5
                },
                new Course
                {
                    Id = 4,
                    RefId = new Guid("17bdb4a1-c77f-4625-a00f-30620c7f3928"),
                    Name = "Drummer for kids (Level Basic/1)",
                    Description = placeholder,
                    ImageUrl = "img/Landing4.svg",
                    Price = 2200000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 1
                },
                new Course
                {
                    Id = 5,
                    RefId = new Guid("eccb915d-a7dd-4762-9732-8aeb7f2bcdd9"),
                    Name = "Kursu Piano : From Zero to Pro (Full Package)",
                    Description = placeholder,
                    ImageUrl = "img/Landing5.svg",
                    Price = 11650000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 2
                },
                new Course
                {
                    Id = 6,
                    RefId = new Guid("2c47a426-06f2-4f9f-bfee-45438d7c46ee"),
                    Name = "Expert Level Saxophone",
                    Description = placeholder,
                    ImageUrl = "img/Landing6.svg",
                    Price = 7350000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 8
                }
            );
            modelBuilder.Entity<Schedule>().HasData(
                new Schedule
                {
                    Id = 1,
                    RefId = new Guid("7e0ea9d3-10e6-4762-a4b9-5569e398f03b"),
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 2,
                    RefId = new Guid("692dfbe2-4ed6-4eb8-9cc8-395820d3ad05"),
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 3,
                    RefId = new Guid("332ffdfb-b8d6-4e06-823c-ec2111c4afa9"),
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 4,
                    RefId = new Guid("9be1b178-ada5-48a6-a580-13a606b8a3c1"),
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 5,
                    RefId = new Guid("abf7194c-d954-407d-af3b-8ebfb946d8f3"),
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 6,
                    RefId = new Guid("b2bc0584-af24-4896-a5cc-7642cabff8d9"),
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 1
                },
                new Schedule
                {
                    Id = 7,
                    RefId = new Guid("336e590c-a8ec-49ac-af12-6f08c837e93d"),
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 8,
                    RefId = new Guid("392a3fce-9a3b-41f3-8d2d-5a1547a0b337"),
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 9,
                    RefId = new Guid("d59b88dc-9880-412f-8702-fa36ab470805"),
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 10,
                    RefId = new Guid("80dd8f4a-6d80-4f37-b201-0855f20a5620"),
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 11,
                    RefId = new Guid("06c3d0ff-45e6-46f5-94b7-f76b5aed1a23"),
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 12,
                    RefId = new Guid("ebd1b0d8-72a7-4a17-ae78-65b3d8f54db1"),
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 2
                },
                new Schedule
                {
                    Id = 13,
                    RefId = new Guid("797150ca-baa3-400b-b88f-e3d11b086a76"),
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 14,
                    RefId = new Guid("d3f704ec-3ba8-4cfa-95c2-d99ce4a71c15"),
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 15,
                    RefId = new Guid("dc795a78-c93b-4499-a412-07a036e87ee4"),
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 16,
                    RefId = new Guid("3b9a7f70-e5f6-4dcb-a6af-72c4b3d305f4"),
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 17,
                    RefId = new Guid("7aa4ec20-b332-4329-8aa4-b8b76e097cd9"),
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 18,
                    RefId = new Guid("b1a31b4c-17ea-47ba-948c-e282a019f510"),
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 3
                },
                new Schedule
                {
                    Id = 19,
                    RefId = new Guid("1984127d-79e4-4221-954b-127718857bc0"),
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 20,
                    RefId = new Guid("1fd6f30b-a51f-4de7-ab35-e309cc988c20"),
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 21,
                    RefId = new Guid("deb1a633-2e59-4ebc-b55e-d65170b94207"),
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 22,
                    RefId = new Guid("8b3e9524-7183-41b8-ab76-3bef3a804bfd"),
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 23,
                    RefId = new Guid("571039cd-eb6e-468a-af34-3fe5a2f19d1e"),
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 24,
                    RefId = new Guid("e0f2ae70-0d8f-42bc-b003-53b70f0e59e3"),
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 4
                },
                new Schedule
                {
                    Id = 25,
                    RefId = new Guid("989ce1b3-83f7-49c5-833b-13e40facbb08"),
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 26,
                    RefId = new Guid("562fd502-1fd0-4fe6-b013-effb8435d3b4"),
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 27,
                    RefId = new Guid("50d7486f-de76-43c9-87f0-3fc55c65289b"),
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 28,
                    RefId = new Guid("f545d7cf-26ce-4532-98b5-f17b980307c7"),
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 29,
                    RefId = new Guid("761b4c9a-60fb-4809-b1b0-66b37c7d3c59"),
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 30,
                    RefId = new Guid("02176419-f3d0-4c9a-b6c7-60f8f84fc203"),
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 5
                },
                new Schedule
                {
                    Id = 31,
                    RefId = new Guid("92dfcd88-5914-4575-b2ac-bce04478c74a"),
                    Date = new DateOnly(2022, 10, 25),
                    CourseId = 6
                },
                new Schedule
                {
                    Id = 32,
                    RefId = new Guid("8bc96c9b-25da-44e4-a83b-1bada63f6b81"),
                    Date = new DateOnly(2022, 10, 26),
                    CourseId = 6
                },
                new Schedule
                {
                    Id = 33,
                    RefId = new Guid("1d64c39e-9857-4e86-9668-8c8b38b2f0d8"),
                    Date = new DateOnly(2022, 10, 27),
                    CourseId = 6
                },
                new Schedule
                {
                    Id = 34,
                    RefId = new Guid("e577aaaf-dea3-4aaf-9a07-4143686e56ba"),
                    Date = new DateOnly(2022, 10, 28),
                    CourseId = 6
                },
                new Schedule
                {
                    Id = 35,
                    RefId = new Guid("6929f28a-4f23-4d42-bd27-a5ab1eca0eda"),
                    Date = new DateOnly(2022, 10, 29),
                    CourseId = 6
                },
                new Schedule
                {
                    Id = 36,
                    RefId = new Guid("4dcce0fa-717d-440e-b5b7-ce7b59f781b4"),
                    Date = new DateOnly(2022, 10, 30),
                    CourseId = 6
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    RefId = new Guid("f37e30ef-bacd-4023-be66-da243fc25964"),
                    Name = "Super Admin",
                    Email = "admin@applemusic.com",
                    EmailConfirmed = true,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new User
                {
                    Id = 2,
                    RefId = new Guid("5c24001d-62ba-45cf-ad61-b91f38fea0bc"),
                    Name = "Nur Imam Iskandar",
                    Email = "nurimamiskandar@gmail.com",
                    EmailConfirmed = true,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                }
                ,
                new User
                {
                    Id = 3,
                    RefId = new Guid("17039ada-1855-41f1-9bec-15c24acada86"),
                    Name = "Iskandar",
                    Email = "imam.stmik15@gmail.com",
                    EmailConfirmed = true,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new User
                {
                    Id = 4,
                    RefId = new Guid("e33a410d-c70e-4fd7-91bd-e629911c929f"),
                    Name = "Dummy User",
                    Email = "iniemaildummysaya@gmail.com",
                    EmailConfirmed = false,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new User
                {
                    Id = 5,
                    RefId = new Guid("55edc09e-db51-49da-98fb-7f2f25ddc2b8"),
                    Name = "yusri sahrul",
                    Email = "yusrisahrul.works@gmail.com",
                    EmailConfirmed = true,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new User
                {
                    Id = 6,
                    RefId = new Guid("c8097c3e-ab7f-48fb-95d4-01a912451575"),
                    Name = "yusri sahrul test",
                    Email = "yusribootcamp@gmail.com",
                    EmailConfirmed = true,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                }
            );
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = 1,
                    RefId = new Guid("40d61f76-7458-4f51-b7be-f665eaaf53f3"),
                    UserId = 1,
                    RoleId = 2,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new UserRole
                {
                    Id = 2,
                    RefId = new Guid("58536f91-0d39-4144-9092-2a587203054b"),
                    UserId = 2,
                    RoleId = 1,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new UserRole
                {
                    Id = 3,
                    RefId = new Guid("bee22858-a299-4adc-9349-d0d27146b2aa"),
                    UserId = 3,
                    RoleId = 1,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new UserRole
                {
                    Id = 4,
                    RefId = new Guid("12a54832-934c-4a98-96a7-3d0343f87568"),
                    UserId = 4,
                    RoleId = 1,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new UserRole
                {
                    Id = 5,
                    RefId = new Guid("435bcec6-301f-48c0-aeb5-72e275dc500a"),
                    UserId = 5,
                    RoleId = 1,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new UserRole
                {
                    Id = 6,
                    RefId = new Guid("d493a9b6-1f7e-45a7-8482-32636583e8f3"),
                    UserId = 6,
                    RoleId = 2,
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                }
            );

            //Seeding ROLE
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RefId = new Guid("e7b86411-acc4-4e6f-b132-8349974d973b"),
                    Name = "User",
                    Description = "Standard user with basic access",
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new Role
                {
                    Id = 2,
                    RefId = new Guid("c444bd50-1a9d-4a33-a0d9-b9b375e81a68"),
                    Name = "Admin",
                    Description = "Administrator with management access",
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                }
                
            );


            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod
                {
                    Id = 1,
                    RefId = new Guid("43380776-ac70-4350-a64b-82050eb436c7"),
                    Name = "Gopay",
                    LogoUrl = "img/Payment1.svg",
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new PaymentMethod
                {
                    Id = 2,
                    RefId = new Guid("17604b46-fd7f-41fd-8a5b-9281a3de15b1"),
                    Name = "OVO",
                    LogoUrl = "img/Payment2.svg",
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new PaymentMethod
                {
                    Id = 3,
                    RefId = new Guid("4aa1dd7f-8c22-446d-a3f5-a25548068daf"),
                    Name = "DANA",
                    LogoUrl = "img/Payment3.svg",
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new PaymentMethod
                {
                    Id = 4,
                    RefId = new Guid("11816d5a-aa8d-4363-95dc-2edcabc66fd5"),
                    Name = "Mandiri",
                    LogoUrl = "img/Payment4.svg",
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new PaymentMethod
                {
                    Id = 5,
                    RefId = new Guid("e4788b84-999f-43ee-b6fe-dead0c41c189"),
                    Name = "BCA",
                    LogoUrl = "img/Payment5.svg",
                    CreatedAt = seedDate,
                    CreatedBy = "System"
                },
                new PaymentMethod
                {
                    Id = 6,
                    RefId = new Guid("6a63902e-c624-41b7-b47a-a57c14514efb"),
                    Name = "BNI",
                    LogoUrl = "img/Payment6.svg",
                    CreatedAt = seedDate,
                    CreatedBy = "System"
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