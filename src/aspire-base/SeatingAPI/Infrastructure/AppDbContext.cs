using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Location> Locations { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Desk> Desks { get; set; }
    public DbSet<BookingRequest> BookingRequests { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Staff>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Staff>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Location>()
            .HasKey(l => l.Id);
        modelBuilder.Entity<Location>()
            .Property(l => l.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Desk>()
            .HasKey(d => d.Id);
        modelBuilder.Entity<Desk>()
            .Property(d => d.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Desk>()
            .HasOne(d => d.Location)
            .WithMany(l => l.Desks)
            .HasForeignKey(d => d.LocationId);

        modelBuilder.Entity<BookingRequest>()
            .HasOne(br => br.Desk)
            .WithMany(d => d.BookingRequests)
            .HasForeignKey(br => br.DeskId);
    }

    /// <summary>
    /// Supporting default and global controls for Audit events (dates and edits)
    /// </summary>
    /// <returns></returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "System";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "System";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}