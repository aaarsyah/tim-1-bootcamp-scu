// Import Entity Framework Core untuk database operations
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Models.Entities;

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
                        .HasColumnType("bigint");
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
                // Indexes
                //entity.HasIndex(e => e.Name).IsUnique(); //TODO: Uncomment this when it's ready
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
                // Base Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId)
                    .IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                // Unique Index
                entity.HasIndex(e => e.RefCode)
                    .IsUnique();
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
                entity.ToTable("InvoiceDetails");
                // Base Entity
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RefId)
                    .IsUnique();
                entity.Property(e => e.RefId)
                    .IsRequired();
                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("bigint");
                // Properties
                // Tak usah configure relationship sama Invoice lagi
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
            var seedDate = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc);
            
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    RefId = new Guid("a2f2a74c-9819-4051-852e-93e859c54661"),
                    Name = "Drum",
                    LongName = "Drummer class",
                    Description = "Pelajari teknik bermain drum dari dasar hingga mahir, termasuk ritme, koordinasi tangan-kaki, dan improvisasi.",
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
                    Description = "Kuasai piano dari dasar sampai teknik lanjutan, termasuk membaca not, improvisasi, dan interpretasi musik.",
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
                    Description = "Pelajari gitar akustik dan elektrik, teknik petikan, chord, solo, dan improvisasi kreatif.",
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
                    Description = "Belajar bass untuk menciptakan groove yang solid, memahami teknik slap, fingerstyle, dan improvisasi musik.",
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
                    Description = "Pelajari biola dari teknik dasar, membaca not, hingga ekspresi musik klasik dan modern.",
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
                    Description = "Kembangkan kemampuan vokal, teknik pernapasan, kontrol nada, serta ekspresi dan interpretasi lagu.",
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
                    Description = "Belajar flute dari teknik dasar embouchure, fingering, hingga memainkan melodi klasik dan kontemporer.",
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
                    Description = "Kuasai saxophone dengan belajar teknik embouchure, breath control, improvisasi jazz, dan interpretasi musik modern.",
                    ImageUrl = "img/Class8.svg",
                    CreatedAt = new DateTime(2022, 10, 18, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System"
                }
            );
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    RefId = new Guid("d236d9db-5312-48d7-b171-a340c134f6f6"),
                    Name = "Kursus Drummer Special Coach (Eno Netral)",
                    Description = "Kursus drum eksklusif dengan mentor profesional Eno Netral, fokus pada teknik, groove, dan improvisasi.",
                    ImageUrl = "img/Landing1.svg",
                    Price = 8500000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 1
                },
                new Course
                {
                    Id = 2,
                    RefId = new Guid("633473ab-67d5-4460-9f9a-a8eef9be341d"),
                    Name = "Drummer for kids (Level Basic/1)",
                    Description = "Kursus drum untuk anak-anak, belajar ritme dasar, koordinasi tangan-kaki, dan bermain lagu sederhana.",
                    ImageUrl = "img/Landing4.svg",
                    Price = 2200000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 1
                },
                new Course
                {
                    Id = 3,
                    RefId = new Guid("250ef74f-6bec-4a29-b101-0d6ed27437ad"),
                    Name = "Kursus Piano : From Zero to Pro (Full Package)",
                    Description = "Belajar piano dari dasar hingga mahir, termasuk teknik, sight-reading, dan repertoar klasik & modern.",
                    ImageUrl = "img/Landing5.svg",
                    Price = 11650000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 2
                },
                new Course
                {
                    Id = 4,
                    RefId = new Guid("2cc98cec-0887-4171-bfeb-b23d68e6e9e1"),
                    Name = "Piano Jazz Improvisation",
                    Description = "Kuasai improvisasi piano jazz, belajar chord voicing, scales, dan bermain bersama band.",
                    ImageUrl = "img/Landing22.svg",
                    Price = 7000000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 2
                },
                new Course
                {
                    Id = 5,
                    RefId = new Guid("602558bf-e45c-4ff5-80ac-c3f4fba1300e"),
                    Name = "[Beginner] Guitar class for kids",
                    Description = "Kursus gitar pemula untuk anak-anak, fokus belajar chord dasar, petikan, dan lagu sederhana.Kursus gitar pemula untuk anak-anak, fokus belajar chord dasar, petikan, dan lagu sederhana.",
                    ImageUrl = "img/Landing2.svg",
                    Price = 1600000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 3
                },
                new Course
                {
                    Id = 6,
                    RefId = new Guid("217cdf56-0c1c-419e-885c-302488474f13"),
                    Name = "Guitar Rock Techniques",
                    Description = "Belajar teknik gitar rock, solo, power chord, dan riff untuk pemula hingga menengah.",
                    ImageUrl = "img/Landing33.svg",
                    Price = 3500000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 3
                },
                new Course
                {
                    Id = 7,
                    RefId = new Guid("ed415851-8d59-44bc-ad4c-c79cc871564e"),
                    Name = "Bass Fundamentals for Beginners",
                    Description = "Kursus bass untuk pemula, belajar teknik dasar, groove, dan memainkan lagu sederhana.",
                    ImageUrl = "img/Landing44.svg",
                    Price = 2000000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 4
                },
                new Course
                {
                    Id = 8,
                    RefId = new Guid("ff17199d-6790-4052-b04b-96d3149b877c"),
                    Name = "Advanced Bass Techniques",
                    Description = "Pelajari teknik lanjutan bass, termasuk slap, tapping, dan improvisasi untuk berbagai genre musik.",
                    ImageUrl = "img/Landing45.svg",
                    Price = 4500000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 4
                },
                new Course
                {
                    Id = 9,
                    RefId = new Guid("1afb6912-52ff-46df-a8ac-feb53bfc7326"),
                    Name = "Biola Mid-Level Course",
                    Description = "Kursus biola level menengah, belajar teknik bowing, vibrato, dan ekspresi musikal.",
                    ImageUrl = "img/Landing3.svg",
                    Price = 3000000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 5
                },
                new Course
                {
                    Id = 10,
                    RefId = new Guid("c1de03ce-528f-4d52-ab9e-bb156e3fc0c1"),
                    Name = "Violin Advanced Performance",
                    Description = "Belajar teknik lanjutan biola, interpretasi musik klasik, dan persiapan tampil di konser.",
                    ImageUrl = "img/Landing55.svg",
                    Price = 6000000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 5
                },
                new Course
                {
                    Id = 11,
                    RefId = new Guid("1b507945-91bf-4904-ac4a-09a18d0e27d1"),
                    Name = "Vocal Training for Beginners",
                    Description = "Kursus menyanyi untuk pemula, fokus pada teknik pernapasan, kontrol nada, dan penguatan suara.",
                    ImageUrl = "img/Landing66.svg",
                    Price = 2500000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 6
                },
                new Course
                {
                    Id = 12,
                    RefId = new Guid("ce44161a-a1e3-4fde-889a-dab9f1355e20"),
                    Name = "Advanced Vocal Mastery",
                    Description = "Kuasai teknik vokal lanjutan, ekspresi musik, vibrato, dan performa panggung profesional.",
                    ImageUrl = "img/Landing66.svg",
                    Price = 5500000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 6
                },
                new Course
                {
                    Id = 13,
                    RefId = new Guid("174bc8e5-e615-4877-b4c2-5202cc70e9d8"),
                    Name = "Flute Basics for Beginners",
                    Description = "Kursus flute pemula, belajar teknik embouchure, fingering, dan memainkan melodi sederhana.",
                    ImageUrl = "img/Landing67.svg",
                    Price = 2200000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 7
                },
                new Course
                {
                    Id = 14,
                    RefId = new Guid("20cd6469-d4c0-486f-84ee-0a3d9805b921"),
                    Name = "Flute Performance & Expression",
                    Description = "Pelajari teknik lanjutan flute, interpretasi musik, dinamika, dan ekspresi panggung.",
                    ImageUrl = "img/Landing77.svg",
                    Price = 4800000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 7
                },
                new Course
                {
                    Id = 15,
                    RefId = new Guid("039dfcd8-7258-4417-8359-5f47fbd44e18"),
                    Name = "Expert Level Saxophone",
                    Description = "Kursus saxophone level expert, belajar improvisasi jazz, teknik embouchure, dan performa profesional.",
                    ImageUrl = "img/Landing6.svg",
                    Price = 7350000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    CategoryId = 8
                },
                new Course
                {
                    Id = 16,
                    RefId = new Guid("c3a3d793-bad8-4f3f-856e-b71de82deb2a"),
                    Name = "Saxophone Jazz Essentials",
                    Description = "Belajar teknik dasar hingga improvisasi jazz di saxophone untuk pemula hingga menengah.",
                    ImageUrl = "img/Landing88.svg",
                    Price = 4500000,
                    CreatedAt = new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc),
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
                Date = new DateOnly(2022, 10, 25),
                CourseId = 2
            },
            new Schedule
            {
                Id = 5,
                RefId = new Guid("abf7194c-d954-407d-af3b-8ebfb946d8f3"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 2
            },
            new Schedule
            {
                Id = 6,
                RefId = new Guid("b2bc0584-af24-4896-a5cc-7642cabff8d9"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 2
            },
            new Schedule
            {
                Id = 7,
                RefId = new Guid("336e590c-a8ec-49ac-af12-6f08c837e93d"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 3
            },
            new Schedule
            {
                Id = 8,
                RefId = new Guid("392a3fce-9a3b-41f3-8d2d-5a1547a0b337"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 3
            },
            new Schedule
            {
                Id = 9,
                RefId = new Guid("d59b88dc-9880-412f-8702-fa36ab470805"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 3
            },
            new Schedule
            {
                Id = 10,
                RefId = new Guid("80dd8f4a-6d80-4f37-b201-0855f20a5620"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 4
            },
            new Schedule
            {
                Id = 11,
                RefId = new Guid("06c3d0ff-45e6-46f5-94b7-f76b5aed1a23"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 4
            },
            new Schedule
            {
                Id = 12,
                RefId = new Guid("ebd1b0d8-72a7-4a17-ae78-65b3d8f54db1"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 4
            },
            new Schedule
            {
                Id = 13,
                RefId = new Guid("797150ca-baa3-400b-b88f-e3d11b086a76"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 5
            },
            new Schedule
            {
                Id = 14,
                RefId = new Guid("d3f704ec-3ba8-4cfa-95c2-d99ce4a71c15"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 5
            },
            new Schedule
            {
                Id = 15,
                RefId = new Guid("e1f2d3c4-5678-4f9a-b123-abcdef123456"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 5
            },
            new Schedule
            {
                Id = 16,
                RefId = new Guid("f1a2b3c4-5678-4a9b-cdef-123456abcdef"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 6
            },
            new Schedule
            {
                Id = 17,
                RefId = new Guid("f2b3c4d5-6789-4b0c-def1-234567abcdef"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 6
            },
            new Schedule
            {
                Id = 18,
                RefId = new Guid("f3c4d5e6-7890-4c1d-ef12-345678abcdef"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 6
            },
            new Schedule
            {
                Id = 19,
                RefId = new Guid("f4d5e6f7-8901-4d2e-f123-456789abcdef"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 7
            },
            new Schedule
            {
                Id = 20,
                RefId = new Guid("a7b8c9d0-9012-4e3f-8123-56789abcdef0"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 7
            },
            new Schedule
            {
                Id = 21,
                RefId = new Guid("b8c9d0e1-0123-4f4a-9234-6789abcdef01"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 7
            },
            new Schedule
            {
                Id = 22,
                RefId = new Guid("c9d0e1f2-1234-4a5b-a345-789abcdef012"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 8
            },
            new Schedule
            {
                Id = 23,
                RefId = new Guid("d0e1f2a3-2345-4b6c-b456-89abcdef0123"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 8
            },
            new Schedule
            {
                Id = 24,
                RefId = new Guid("e1f2a3b4-3456-4c7d-c567-9abcdef01234"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 8
            },
            new Schedule
            {
                Id = 25,
                RefId = new Guid("e8ad711c-43bd-43a2-98c6-3d46b3667812"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 9
            },
            new Schedule
            {
                Id = 26,
                RefId = new Guid("cbd109ea-3589-488a-bf07-668b46f6bc8a"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 9
            },
            new Schedule
            {
                Id = 27,
                RefId = new Guid("670e7315-12b0-4dda-8adb-34de699cf3be"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 9
            },
            new Schedule
            {
                Id = 28,
                RefId = new Guid("20ee3579-1c49-4e8f-8554-3a1ff11411e2"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 10
            },
            new Schedule
            {
                Id = 29,
                RefId = new Guid("7cceb84e-1fb3-4834-b594-903edb63e1a1"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 10
            },
            new Schedule
            {
                Id = 30,
                RefId = new Guid("5d4ab8e2-0383-4d71-9d70-9e1b6e86ec94"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 10
            },
            new Schedule
            {
                Id = 31,
                RefId = new Guid("26f85f8d-9f30-4b2d-88f1-1170c0cfd7bc"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 11
            },
            new Schedule
            {
                Id = 32,
                RefId = new Guid("c68ce666-4fb9-41f1-8823-cb5ef12a26ee"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 11
            },
            new Schedule
            {
                Id = 33,
                RefId = new Guid("fc1e014b-d804-45e6-b9e6-ca7cb863deee"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 11
            },
            new Schedule
            {
                Id = 34,
                RefId = new Guid("bacae46f-be78-4e4e-ad18-7050a9fcf125"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 12
            },
            new Schedule
            {
                Id = 35,
                RefId = new Guid("c9444095-c496-4a71-9e60-3932449f5f91"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 12
            },
            new Schedule
            {
                Id = 36,
                RefId = new Guid("7b89ba88-8d47-438b-938d-e4a6b207ee93"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 12
            },
            new Schedule
            {
                Id = 37,
                RefId = new Guid("0a5b10b7-5b2b-4323-99f7-d4016697b191"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 13
            },
            new Schedule
            {
                Id = 38,
                RefId = new Guid("7f10395a-f36e-455b-9dbc-7c39210d3ee6"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 13
            },
            new Schedule
            {
                Id = 39,
                RefId = new Guid("237fc072-5e2e-4f10-b980-0d18b889cab1"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 13
            },
            new Schedule
            {
                Id = 40,
                RefId = new Guid("b04045b4-13af-4d8c-921c-971e0c5cf904"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 14
            },
            new Schedule
            {
                Id = 41,
                RefId = new Guid("c244ef8a-3222-4002-be2b-d53a7fc6e787"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 14
            },
            new Schedule
            {
                Id = 42,
                RefId = new Guid("2bb5eaf0-f3a6-4842-a191-161cc51b4bf0"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 14
            },
            new Schedule
            {
                Id = 43,
                RefId = new Guid("c6666efa-2e35-4bac-810f-10e694a6d7db"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 15
            },
            new Schedule
            {
                Id = 44,
                RefId = new Guid("1083cf7b-d1ef-4953-901b-64c72ae82160"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 15
            },
            new Schedule
            {
                Id = 45,
                RefId = new Guid("4e0ad8cb-938d-4f9d-9786-34550d529dcc"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 15
            },
            new Schedule
            {
                Id = 46,
                RefId = new Guid("db796688-95bb-4aa4-8a08-d846dd81e1d3"),
                Date = new DateOnly(2022, 10, 25),
                CourseId = 16
            },
            new Schedule
            {
                Id = 47,
                RefId = new Guid("172ac710-0583-42f9-b27d-653fc99251cf"),
                Date = new DateOnly(2022, 10, 26),
                CourseId = 16
            },
            new Schedule
            {
                Id = 48,
                RefId = new Guid("5923917b-fefd-4525-aab3-12813aa6c41c"),
                Date = new DateOnly(2022, 10, 27),
                CourseId = 16
            }
            );

            var admin = new User
            {
                Id = 1,
                IsActive = true,
                RefId = new Guid("f37e30ef-bacd-4023-be66-da243fc25964"),
                Name = "Super Admin",
                Email = "admin@applemusic.com",
                EmailConfirmed = true,
                CreatedAt = seedDate,
                CreatedBy = "System"
            };

            // Hash password
            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123@");


            modelBuilder.Entity<User>().HasData(
                admin,
                new User
                {
                    Id = 2,
                    IsActive = true,
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
                    IsActive = true,
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
                    IsActive = false,
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
                    IsActive = true,
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
                    IsActive = true,
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