
using Microsoft.EntityFrameworkCore;

public class LocationService
{
    private readonly AppDbContext _context;
    public LocationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Location>> GetLocations()
    {
        return await _context.Locations.ToListAsync();
    }

    // public List<Location> GetLocations()
    // {
    //     return new List<Location>
    //     {
    //         new Location
    //         {
    //             Id = 1,
    //             Name = "Location 1",
    //             Active = true,
    //             SeatingCount = 100
    //         },
    //         new Location
    //         {
    //             Id = 2,
    //             Name = "Location 2",
    //             Active = true,
    //             SeatingCount = 65
    //         },
    //         new Location
    //         {
    //             Id = 3,
    //             Name = "Location 3",
    //             Active = false,
    //             SeatingCount = 40
    //         }
    //     };
    // }

    public Location GetLocation(int id)
    {
        return new Location
        {
            Id = id,
            Name = "Location " + id
        };
    }
}