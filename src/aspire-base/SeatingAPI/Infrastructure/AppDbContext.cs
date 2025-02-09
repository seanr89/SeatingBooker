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
        // modelBuilder.Entity<Customer>(c => {
        //     c.HasKey(x => x.Id);
        // });
    }
}