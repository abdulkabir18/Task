namespace Infrastructures.DBaseContext
{
    using Domain.Entities;
    using Domain.ValueObjects;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<AppLog> ApplicationLogs => Set<AppLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var emailConverter = new ValueConverter<Email, string>(
                v => v.Value,
                v => new Email(v)
            );
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("App_Users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).HasConversion(emailConverter).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.Email);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);
                entity.Property(u => u.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("App_Tasks");
                entity.HasKey(u => u.Id);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("App_Notifications");
                entity.HasKey(u => u.Id);
            });

            modelBuilder.Entity<AppLog>(entity => 
            {
                entity.ToTable("App_Logs");
                entity.HasKey(u => u.Id);
            });
        }
    }

}
