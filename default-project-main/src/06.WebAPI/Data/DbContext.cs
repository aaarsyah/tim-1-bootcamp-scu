// Import Entity Framework Core untuk database operations
using Microsoft.EntityFrameworkCore;
// Import Models untuk entities
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Data
{
    // Untuk membuat: dotnet ef migrations add InitialCreate

    public class AppleMusicDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Course
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
                        .OnDelete(DeleteBehavior.Restrict); // Larang penghapusan Category bila ada Courses yang terhubung
                // Relationship dengan Schedule
                entity.HasMany(e => e.Schedules)
                        .WithOne(e => e.Course)
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua Schedule yang terhubung bila Courses dihapus
                // Constraint
                entity.ToTable(e => e.HasCheckConstraint(
                        "CK_Price", "[Price] >= 0"));
            });
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
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedules");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                entity.Property(e => e.Date)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                // Tak usah configure relationship sama Courses lagi
            });
            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                // Primary key
                entity.HasKey(e => e.Id);
                // Properties
                entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(20);
                entity.Property(e => e.Email)
                        .IsRequired()
                        .HasMaxLength(50);
                entity.Property(e => e.Password)
                        .IsRequired()
                        .HasMaxLength(256);
                entity.Property(e => e.IsActive)
                        .IsRequired()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);
                entity.Property(e => e.IsAdmin)
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
                        .OnDelete(DeleteBehavior.Cascade); // Hapus juga semua CartItem yang terhubung bila User dihapus
                // Relationship dengan Participant
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
            });
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
            // Invoice
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
            // Invoice
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
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.

            //Untuk sementara pakai SQL server lokal dari instalasi SQL Express
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=AppleMusicDb;Trusted_Connection=True;TrustServerCertificate=True");
    }
}
