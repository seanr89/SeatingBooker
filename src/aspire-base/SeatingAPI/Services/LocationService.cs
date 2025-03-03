
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

    /// <summary>
    /// TODO: detail this!
    /// </summary>
    /// <param name="id"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    public async Task<Location> GetDesksAndBookingsForLocationOnDate(int id, DateTime date)
    {
        var location = await _context.Locations
            .Include(l => l.Desks)
            .ThenInclude(d => d.BookingRequests.Where(x => x.RequestDate.Date == date.Date))
            .FirstAsync(l => l.Id == id);
        
        // Loop through the desks and check if they have any bookings etc...
        foreach (var desk in location.Desks)
        {
            // desk check for dedicated desks that have no bookings!
            if(desk.BookingRequests.Any() == false && desk.IsHotDesk == false)
            {
                // we need to do a quick check here to avoid null reference exceptions!!
                desk.BookingRequests.Add(new BookingRequest(){
                    RequestDate = date.Date,
                    DeskId = desk.Id,
                    Desk = desk,
                    StaffId = (int)desk.StaffId,
                    Staff = desk.Staff,
                    State = RequestState.Booked
                });
                continue;
            }
            // if no booking has been made at all then the desk by default is free!
            if(desk.BookingRequests.Any() == false)
            {
                desk.BookingRequests.Add(new BookingRequest(){
                    RequestDate = date.Date,
                    DeskId = desk.Id,
                    Desk = desk,
                    State = RequestState.Free
                });
                continue;
            }
            // else we then just grab all the bookings for that day!
            desk.BookingRequests = desk.BookingRequests.Where(x => x.RequestDate.Date == date.Date).ToList();
        }
        return location;
    }
}