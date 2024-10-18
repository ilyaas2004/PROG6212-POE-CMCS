using Microsoft.EntityFrameworkCore;
using PROG6212_POE_CMCS.Models;

public class AppDbContext : DbContext
{
    public DbSet<Claim> Claims { get; set; }
    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<Document> Documents { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lecturer>()
            .Property(l => l.HourlyRate)
            .HasColumnType("decimal(18,2)"); // Adjust the precision and scale as needed

        base.OnModelCreating(modelBuilder);
    }
}
