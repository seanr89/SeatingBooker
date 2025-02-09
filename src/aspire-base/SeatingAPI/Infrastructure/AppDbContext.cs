using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Location> Locations { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Desk> Desks { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Desk>()
            .HasOne(d => d.Location)
            .WithMany(l => l.Desks)
            .HasForeignKey(d => d.LocationId);

        modelBuilder.Entity<BookingRequest>()
            .HasOne(br => br.Desk)
            .WithMany(d => d.BookingRequests)
            .HasForeignKey(br => br.DeskId);
    }
}