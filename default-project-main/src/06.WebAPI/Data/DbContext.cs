// Import Entity Framework Core untuk database operations
using Microsoft.EntityFrameworkCore;
// Import Models untuk entities
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Data
{
    public class AppleMusicDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Course>()
            //    .Property(b => b.Url)
            //    .IsRequired();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");
                //Primary key
                entity.HasKey(e => e.Id);
                //Properties (just like CREATE TABLE in SQL)
                entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(150);
                entity.Property(e => e.Description)
                        .HasMaxLength(3000);
                entity.Property(e => e.Price)
                        .IsRequired()
                        .HasColumnType("integer");
                //relaitonship dengan Category
                entity.HasOne(e => e.Category)
                        .WithMany(e => e.Courses)
                        .OnDelete(DeleteBehavior.Restrict); //Larang penghapusan bila ada Courses dengan Category yang akan dihapus
                //relaitonship dengan Schedule
                entity.HasMany(e => e.Schedules)
                        .WithMany(e => e.Courses); // TODO: Konfigurasi tabel "join" (tabel antara Course dan Schedule

            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                //Primary key
                entity.HasKey(e => e.Id);
                //
                entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(50);
                entity.Property(e => e.Description)
                        .HasMaxLength(1000);
                //Tak usah configure relationship sama Courses lagi
            });
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");
                //Primary key
                entity.HasKey(e => e.Id);
                //
                entity.Property(e => e.Date)
                        .IsRequired()
                        .HasDefaultValueSql("GETUTCDATE()");
                //Tak usah configure relationship sama Courses lagi
            });
        }
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.

        //Untuk sementara pakai SQL server lokal dari instalasi SQL Express
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True");
    }
}
