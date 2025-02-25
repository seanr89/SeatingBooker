
using Microsoft.EntityFrameworkCore;

public class DeskService
{
    private readonly ILogger<DeskService> _logger;
    private readonly AppDbContext _context;
    public DeskService(AppDbContext context, ILogger<DeskService> logger)
    {
        _context = context;
        _logger = logger;
    }

    #region Get Methods

    public async Task<List<Desk>> GetDesks()
    {
        return await _context.Desks.ToListAsync();
    }

    public async Task<Desk?> GetDeskById(int id)
    {
        return await _context.Desks
            .Include(d => d.Staff)
            .Include(d => d.BookingRequests)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    public async Task<RequestState?> CheckDeskStatusForDate(int id, DateTime date)
    {
        //Search for the desk first and handle nulls
        var desk = await _context.Desks
            .Include(d => d.BookingRequests)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (desk == null)
        {
            return null;
        }

        //Check if a booking request has been made on that day for the desk
        var bookingRequest = desk.BookingRequests.FirstOrDefault(x => x.RequestDate.Date == date.Date);
        // if its null and hotdesk is not flagged
        if (bookingRequest == null && desk.IsHotDesk)
        {
            return RequestState.Free;
        }
        
        // if its null and hotdesk is flagged then it is by default booked
        if (!desk.IsHotDesk){
            return RequestState.Booked;
        }

        // Else we return the state of the request!
        return bookingRequest?.State;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="desk"></param>
    /// <returns></returns>
    public async Task<Desk?> CreateDesk(CreateDeskDTO desk)
    {
        _logger.LogInformation("DeskService:CreateDesk");
        var Location = await _context.Locations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == desk.LocationId);
        if (Location == null)
        {
            return null;
        }

        if(desk.StaffId != null)
        {
            var staff = await _context.Staff
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == desk.StaffId);
            if (staff == null)
            {
                return null;
            }
        }

        _logger.LogInformation("Creating new desk");
        var newDesk = new Desk
        {
            Name = desk.Name,
            IsHotDesk = desk.IsHotDesk,
            LocationId = desk.LocationId
        };

        await _context.Desks.AddAsync(newDesk);
        await _context.SaveChangesAsync();

        return newDesk;
    }
}