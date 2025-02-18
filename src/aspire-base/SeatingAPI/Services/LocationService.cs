
using Microsoft.EntityFrameworkCore;

public class LocationService
{
    private readonly ILogger<LocationService> _logger;
    private readonly AppDbContext _context;
    public LocationService(AppDbContext context,
        ILogger<LocationService> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<Location>> GetLocations()
    {
        return await _context.Locations.ToListAsync();
    }

    /// <summary>
    /// Support for getting a single location
    /// Includes Desks and Staff 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Location> GetLocation(int id)
    {
        var location = await _context.Locations
            .Include(l => l.Desks)
            .ThenInclude(d => d.Staff)
            .FirstAsync(l => l.Id == id);
        return location;
    }
}