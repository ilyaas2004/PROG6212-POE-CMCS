using Microsoft.EntityFrameworkCore;
using PROG6212_POE_CMCS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Claim> Claims { get; set; }
    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<Document> Documents { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        // Configure relationships
        modelBuilder.Entity<Claim>()
            .HasOne(c => c.Lecturer)
            .WithMany() // No navigation back to claims from Lecturer
            .HasForeignKey(c => c.LecturerID);

        // Configure FinalPayment column precision and scale
        modelBuilder.Entity<Claim>()
            .Property(c => c.FinalPayment)
            .HasColumnType("decimal(18,2)"); // Or use .HasPrecision(18, 2)

        base.OnModelCreating(modelBuilder);
    }
}
