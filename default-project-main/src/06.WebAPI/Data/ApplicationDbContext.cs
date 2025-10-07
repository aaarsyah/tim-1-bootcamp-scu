using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Data
{
    public class CourseDbContext : DbContext
    {
        /// <summary>
        /// Constructor - Menerima DbContextOptions untuk dependency injection
        /// Options akan dikonfigurasi di Program.cs dengan connection string dan provider
        /// </summary>
        /// <param name="options">Database context options dari DI container</param>
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {
            // Constructor base akan handle semua configuration yang di-pass dari DI
        }

        /// <summary>
        /// DbSet untuk Course table
        /// Entity Framework akan map ini ke tabel "Course" di database
        /// </summary>
        public DbSet<Course> Course { get; set; }
    
        public DbSet<Category> Categories { get; set; }

        public DbSet<PaymentMethod> Payment { get; set; }
        public DbSet<User> User { get; set; }
        

        /// <summary>
        /// OnModelCreating - Method untuk configure entity models menggunakan Fluent API
        /// Dipanggil sekali saat DbContext pertama kali di-initialize
        /// Lebih powerful daripada Data Annotations untuk complex configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base implementation untuk ensure proper inheritance behavior
            base.OnModelCreating(modelBuilder);

            // ========== ENTITY CONFIGURATIONS ==========
            // Menggunakan separate methods untuk better organization dan readability
            ConfigureCategory(modelBuilder);
            ConfigureCourse(modelBuilder);

            // ========== GLOBAL CONFIGURATIONS ==========
            ApplyGlobalConfigurations(modelBuilder);

            // ========== DATA SEEDING ==========
            // Seed initial data untuk development dan testing
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Configure Category entity menggunakan Fluent API
        /// Detailed configuration untuk table structure, constraints, dan relationships
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                // ========== TABLE CONFIGURATION ==========
                // Explicit table name (optional, EF akan use "Categories" by convention)
                entity.ToTable("Categories");
                
                // ========== PRIMARY KEY ==========
                // Explicit primary key configuration (optional untuk "Id" property)
                entity.HasKey(e => e.Id);
                
                // ========== COLUMN CONFIGURATIONS ==========
                
                // Name column - Required dengan max length dan unique constraint
                entity.Property(e => e.Name)
                      .IsRequired()                           // NOT NULL constraint
                      .HasMaxLength(50)                      // VARCHAR(100) constraint
                      .HasComment("Category name - must be unique"); // SQL comment untuk documentation

                entity.Property(e => e.LongName)
                        .IsRequired()
                        .HasMaxLength(150);
                
                // Description column - Optional dengan max length
                entity.Property(e => e.Description)
                      .HasMaxLength(1000)                      // VARCHAR(500), nullable by default
                      .HasComment("Optional category description");
                
                // IsActive column dengan default value
                entity.Property(e => e.IsActive)
                      .IsRequired()                           // NOT NULL
                      .HasColumnType("bit")
                      .HasDefaultValue(true)                  // DEFAULT constraint di database
                      .HasComment("Indicates if category is active");
                
                // CreatedAt dengan default SQL function
                entity.Property(e => e.CreatedAt)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()")     // SQL Server function untuk current UTC time
                      .HasComment("Timestamp when category was created");
                
                // UpdatedAt akan di-handle di SaveChangesAsync override
                entity.Property(e => e.UpdatedAt)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()")
                      .HasComment("Timestamp when category was last updated");
                
                // ========== INDEXES ==========
                
                // Unique index pada Name column untuk prevent duplicates
                entity.HasIndex(e => e.Name)
                      .IsUnique()                             // UNIQUE constraint
                      .HasDatabaseName("IX_Categories_Name")  // Custom index name
                      .HasFilter("[IsActive] = 1");          // Filtered index - hanya untuk active categories
                
                // Index pada IsActive untuk performance (sering digunakan dalam WHERE clauses)
                entity.HasIndex(e => e.IsActive)
                      .HasDatabaseName("IX_Categories_IsActive");

                entity.Property(e => e.ImageUrl)
                      .HasMaxLength(500)                      // URL length limitation
                      .HasComment("URL path to Course image");
                
                // Composite index untuk queries yang filter berdasarkan multiple columns
                entity.HasIndex(e => new { e.IsActive, e.CreatedAt })
                      .HasDatabaseName("IX_Categories_Active_Created")
                      .HasFilter("[IsActive] = 1");
                
                // ========== RELATIONSHIPS ==========
                // One-to-Many relationship dengan Products akan dikonfigurasi di ConfigureProduct
            });
        }

        /// <summary>
        /// Configure Product entity dengan advanced Fluent API features
        /// Includes relationship configuration, constraints, dan performance optimizations
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ConfigureCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                // ========== TABLE CONFIGURATION ==========
                entity.ToTable("Courses");
                entity.HasKey(e => e.Id);

                // ========== COLUMN CONFIGURATIONS ==========

                // Name column dengan detailed constraints
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(150)                      // Lebih panjang dari Category untuk flexibility
                      .HasComment("Course name");

                // Description column
                entity.Property(e => e.Description)
                      .IsRequired()                           // Required untuk products (business rule)
                      .HasMaxLength(3000)                     // Long text untuk detailed descriptions
                      .HasComment("Detailed Course description");

                // Price column dengan precision/scale specification
                entity.Property(e => e.Price)
                      .IsRequired()
                      .HasColumnType("numeric(10)")         
                      .HasComment("Course price in numeric format");


                // ImageUrl column - Optional
                entity.Property(e => e.ImageUrl)
                      .HasMaxLength(500)                      // URL length limitation
                      .HasComment("URL path to Course image");

                // IsActive dengan default value
                entity.Property(e => e.IsActive)
                      .IsRequired()
                      .HasColumnType("bit")
                      .HasDefaultValue(true)
                      .HasComment("Indicates if Course is active and available");

                // Timestamp columns
                entity.Property(e => e.CreatedAt)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()")
                      .HasComment("Timestamp when Course was created");

                entity.Property(e => e.UpdatedAt)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()")
                      .HasComment("Timestamp when Course was last updated");

                // ========== FOREIGN KEY CONFIGURATION ==========

                // CategoryId foreign key dengan explicit configuration
                entity.Property(e => e.CategoryId)
                      .IsRequired()                           // NOT NULL - every product must have category
                      .HasComment("Foreign key reference to Categories table");

                // ========== INDEXES FOR PERFORMANCE ==========

                // Index pada Name untuk search functionality
                entity.HasIndex(e => e.Name)
                      .HasDatabaseName("IX_Course_Name");

                // Index pada CategoryId untuk join performance
                entity.HasIndex(e => e.CategoryId)
                      .HasDatabaseName("IX_Course_CategoryId");

                // Index pada Price untuk range queries dan sorting
                entity.HasIndex(e => e.Price)
                      .HasDatabaseName("IX_Course_Price");

                // Composite index untuk active products queries
                entity.HasIndex(e => new { e.IsActive, e.CategoryId })
                      .HasDatabaseName("IX_Course_Active_Category")
                      .HasFilter("[IsActive] = 1");

                // Composite index untuk search dan pagination
                entity.HasIndex(e => new { e.IsActive, e.Name, e.Price })
                      .HasDatabaseName("IX_Course_Active_Name_Price")
                      .HasFilter("[IsActive] = 1");

                // ========== RELATIONSHIP CONFIGURATION ==========

                // Many-to-One relationship dengan Category
                entity.HasOne(e => e.Category)               // Product has one Category
                      .WithMany(e => e.Course)              // Category has many Products  
                      .HasForeignKey(e => e.CategoryId)       // Foreign key property
                      .OnDelete(DeleteBehavior.Restrict)      // Larang penghapusan Category bila ada Courses yang terhubung
                      .HasConstraintName("FK_Course_Categories"); // Custom FK constraint name

                // Relationship dengan Schedule
                entity.HasMany(e => e.Schedules)
                      .WithOne(e => e.Course)
                      .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua Schedule yang terhubung bila Courses dihapus

            });
        }

        private void ConfigurePayment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                // ========== TABLE CONFIGURATION ==========
                entity.ToTable("Payment");
                entity.HasKey(e => e.Id);

                // ========== COLUMN PAYMENT METHOD ==========

                entity.ToTable("PaymentMethods");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasComment("Payment Method name");

                entity.Property(e => e.LogoUrl)
                      .HasMaxLength(500)                      // URL length limitation
                      .HasComment("URL path to Payment logo");

                entity.Property(e => e.CreatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");

                 // IsActive dengan default value
                entity.Property(e => e.IsActive)
                      .IsRequired()
                      .HasColumnType("bit")
                      .HasDefaultValue(true)
                      .HasComment("Indicates if Payment is active and available");

                // Tak usah configure relationship sama Invoice lagi
                
                // Composite index untuk queries yang filter berdasarkan multiple columns
                entity.HasIndex(e => new { e.IsActive, e.Name})
                      .HasDatabaseName("IX_Payment_Active_Name")
                      .HasFilter("[IsActive] = 1");

            });
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                // ========== TABLE CONFIGURATION ==========
                entity.ToTable("User");
                entity.HasKey(e => e.Id);

                // ========== COLUMN USER ==========
                entity.Property(e => e.Name)
                       .IsRequired()
                       .HasMaxLength(20)
                       .HasComment("User name");

                entity.Property(e => e.Email)
                        .IsRequired()
                        .HasMaxLength(50);

                entity.Property(e => e.Password)
                        .IsRequired()
                        .HasMaxLength(256);

                entity.Property(e => e.IsActive)
                        .IsRequired()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasComment("Indicates if User is active and available");

                entity.Property(e => e.IsAdmin)
                        .IsRequired()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasComment("Indicates if User is Admin");

                entity.Property(e => e.CreatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");

                // Relationship dengan ItemCart
                entity.HasMany<CartItem>() //bukan ICollection karena tidak boleh null sedangkan default awal nilai CartItem null
                        .WithOne(e => e.User)
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua CartItem yang terhubung bila User dihapus

                // Relationship dengan Participant (My Class)
                entity.HasMany<Participant>()
                        .WithOne(e => e.User)
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua Participant yang terhubung bila User dihapus

                // Relationship dengan Invoice
                entity.HasMany<Invoice>()
                        .WithOne(e => e.User)
                        .OnDelete(DeleteBehavior.SetNull); // Putuskan semua Invoice yang terhubung bila User dihapus

                // Constraint
                entity.ToTable(e => e.HasCheckConstraint(
                        "CK_Email", "[Email] LIKE '%@%.%'"));

                entity.ToTable(e => e.HasCheckConstraint(
                        "CK_Password",
                        "LEN([Password]) >= 8 " +
                        "AND [Password] LIKE '%[A-Z]%' " +
                        "AND [Password] LIKE '%[a-z]%' " +
                        "AND [Password] LIKE '%[0-9]%'"));

                //Indexis (Pencarian)
                entity.HasIndex(e => e.Email)
                      .IsUnique()
                      .HasDatabaseName("IX_User_Email");

                entity.HasIndex(e => new { e.Name })
                      .HasDatabaseName("IX_User_Name");

                entity.HasIndex(e => e.IsActive)
                      .HasDatabaseName("IX_User_IsActive")
                      .HasFilter("[IsActive] = 1");

                entity.HasIndex(e => e.IsAdmin)
                      .HasDatabaseName("IX_User_IsAdmin")
                      .HasFilter("[IsAdmin] = 1");      
    
            });
        }

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
                            .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua CartItem yang terhubung bila Schedule dihapus
                });

        }

        private void ConfigureParticipant(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participant>(entity =>
            {
                entity.ToTable("Participants");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                // Tak usah configure relationship sama User lagi
                // Relationship dengan Schedule
                entity.HasOne(e => e.Schedule)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua Participant yang terhubung bila Schedule dihapus
            });
        }

        private void ConfigureInvoice(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoices");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                entity.Property(e => e.Date)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                // Tak usah configure relationship sama User lagi
                // Relationship dengan PaymentMethod
                entity.HasOne(e => e.PaymentMethod)
                        .WithMany()
                        .OnDelete(DeleteBehavior.SetNull); // Hapus juga semua InvoiceDetails yang terhubung bila Invoice dihapus
                // Relationship dengan InvoiceDetails
                entity.HasMany(e => e.InvoiceDetails)
                        .WithOne()
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua InvoiceDetails yang terhubung bila Invoice dihapus
            });
        }        

        /// <summary>
        /// Apply global configurations yang berlaku untuk semua entities
        /// Menggunakan conventions untuk consistency across database
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void ApplyGlobalConfigurations(ModelBuilder modelBuilder)
        {
            // ========== GLOBAL NAMING CONVENTIONS ==========

            // Configure semua string properties untuk use NVARCHAR instead of NVARCHAR(MAX)
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    // Set default max length untuk string properties yang belum dikonfigurasi
                    if (property.ClrType == typeof(string) && property.GetMaxLength() == null)
                    {
                        property.SetMaxLength(255); // Default max length
                    }

                }
            }

            // ========== GLOBAL QUERY FILTERS ==========
            // Apply soft delete filter globally (if needed in future)
            // modelBuilder.Entity<Product>().HasQueryFilter(p => p.IsActive);
            // modelBuilder.Entity<Category>().HasQueryFilter(c => c.IsActive);
        }

        /// <summary>
        /// Seed initial data untuk development dan testing
        /// Data ini akan di-insert saat migration pertama kali dijalankan
        /// 
        /// Data Seeding Best Practices:
        /// - Use stable IDs untuk avoid conflicts
        /// - Provide realistic test data  
        /// - Keep seed data minimal tapi representative
        /// - Use meaningful data untuk development dan demo
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance</param>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // ========== SEED CATEGORIES ==========
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Drum",
                    LongName = "Drummer class",
                    Description = "Kelas yang mengajarkan teknik dasar hingga lanjutan bermain drum, termasuk ritme, tempo, dan koordinasi tangan.",
                    IsActive = true,
                    ImageUrl = "/images/drum.svg",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 2,
                    Name = "Piano",
                    LongName = "Pianist class",
                    Description = "Pelajari dasar bermain piano, membaca notasi musik, harmoni, dan teknik permainan untuk semua level.",
                    IsActive = true,
                    ImageUrl = "/images/piano.svg",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 3,
                    Name = "Gitar",
                    LongName = "Guitarist class",
                    Description = "Kursus gitar akustik dan elektrik untuk mempelajari chord, melodi, improvisasi, dan teknik fingerstyle.",
                    IsActive = true,
                    ImageUrl = "/images/guitar.svg",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 4,
                    Name = "Bass",
                    LongName = "Bassist class",
                    Description = "Kelas bass untuk memahami peran ritmis dan harmonis dalam musik serta teknik slap dan groove.",
                    IsActive = true,
                    ImageUrl = "/images/bass.svg",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 5,
                    Name = "Biola",
                    LongName = "Violinist class",
                    Description = "Pelatihan bermain biola dari dasar hingga mahir, mencakup teknik bowing, fingering, dan intonasi.",
                    IsActive = true,
                    ImageUrl = "/images/violin.svg",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 6,
                    Name = "Menyanyi",
                    LongName = "Singer class",
                    Description = "Kelas vokal untuk melatih teknik pernapasan, intonasi, artikulasi, dan ekspresi dalam bernyanyi.",
                    IsActive = true,
                    ImageUrl = "/images/singing.svg",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 7,
                    Name = "Flute",
                    LongName = "Flutist class",
                    Description = "Kursus flute untuk memahami embouchure, pernapasan, teknik fingering, dan interpretasi musik klasik maupun modern.",
                    IsActive = true,
                    ImageUrl = "/images/flute.svg",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 8,
                    Name = "Saxophone",
                    LongName = "Saxophone class",
                    Description = "Kelas saxophone untuk mempelajari teknik embouchure, improvisasi jazz, serta kontrol nada dan dinamika.",
                    IsActive = true,
                    ImageUrl = "/images/saxophone.svg",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }


            );

            // ========== SEED PRODUCTS ==========
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Name = "Kursus Drummer Special Coach (Eno Netral)",
                    Description = "High-performance gaming laptop with RTX graphics, 16GB RAM, and 1TB SSD. Perfect for gaming and professional work.",
                    Price = 8500000,
                    ImageUrl = "/images/Class1.svg",
                    IsActive = true,
                    CategoryId = 1,
                    CreatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc)
                },
                new Course
                {
                    Id = 2,
                    Name = "[Beginner] Guitar class for kids",
                    Description = "Latest smartphone with 5G connectivity, triple camera system, and all-day battery life. Available in multiple colors.",
                    Price = 1600000,
                    ImageUrl = "/images/Class2.svg",
                    IsActive = true,
                    CategoryId = 3,
                    CreatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc)
                },
                new Course
                {
                    Id = 3,
                    Name = "Biola Mid-Level Course",
                    Description = "Premium noise-cancelling wireless headphones with 30-hour battery life and superior sound quality.",
                    Price = 3000000,
                    ImageUrl = "/images/Class3.svg",
                    IsActive = true,
                    CategoryId = 5,
                    CreatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc)
                },
                new Course
                {
                    Id = 4,
                    Name = "Drummer for kids (Level Basic/1)",
                    Description = "Comfortable 100% organic cotton t-shirt with modern fit. Available in multiple sizes and colors.",
                    Price = 2200000,
                    ImageUrl = "/images/Class4.svg",
                    IsActive = true,
                    CategoryId = 1,
                    CreatedAt = new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc)
                },
                new Course
                {
                    Id = 5,
                    Name = "Kursus Piano : From Zero to Pro (Full Package)",
                    Description = "Classic fit denim jeans made from durable, comfortable fabric. Perfect for casual and semi-formal occasions.",
                    Price = 11650000,
                    ImageUrl = "/images/Class5.svg",
                    IsActive = true,
                    CategoryId = 2,
                    CreatedAt = new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc)
                },
                new Course
                {
                    Id = 6,
                    Name = "Expert Level Saxophone",
                    Description = "Comprehensive guide to C# programming with practical examples, best practices, and real-world projects.",
                    Price = 7350000,
                    ImageUrl = "/images/Class6.svg",
                    IsActive = true,
                    CategoryId = 8,
                    CreatedAt = new DateTime(2024, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 4, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // ========== SEED PAYMENT METHOD ==========
            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod
                {
                    Id = 1,
                    Name = "Gopay",
                    LogoUrl = "img/Payment1.svg"
                },
                new PaymentMethod
                {
                    Id = 2,
                    Name = "OVO",
                    LogoUrl = "img/Payment2.svg"
                },
                new PaymentMethod
                {
                    Id = 3,
                    Name = "Dana",
                    LogoUrl = "img/Payment3.svg"
                },
                new PaymentMethod
                {
                    Id = 4,
                    Name = "Mandiri",
                    LogoUrl = "img/Payment4.svg"
                },
                new PaymentMethod
                {
                    Id = 5,
                    Name = "BCA",
                    LogoUrl = "img/Payment5.svg"
                },
                new PaymentMethod
                {
                    Id = 6,
                    Name = "BNI",
                    LogoUrl = "img/Payment6.svg"
                }
            );

            // ========== SEED USER ==========
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Zulfan Jaya",
                    Email = "zulfanjaya@gmail.com",
                    Password = "zulfanjaya",
                    IsActive = true,
                    IsAdmin = false
                },
                new User
                {
                    Id = 2,
                    Name = "Kiki Saputri",
                    Email = "kikisaputri@gmail.com",
                    Password = "kikisaputri",
                    IsActive = true,
                    IsAdmin = true
                },
                new User
                {
                    Id = 3,
                    Name = "Dana Wijaya",
                    Email = "danawijaya@gmail.com",
                    Password = "danawijaya",
                    IsActive = false,
                    IsAdmin = false
                },
                new User
                {
                    Id = 4,
                    Name = "Hana Wahdiah",
                    Email = "hanawahdiah@gmail.com",
                    Password = "hanawahdiah",
                    IsActive = true,
                    IsAdmin = false
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
            var modifiedCourses = ChangeTracker.Entries<Course>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in modifiedCourses)
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