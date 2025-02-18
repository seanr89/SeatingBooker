
using Microsoft.EntityFrameworkCore;

public class DeskService
{
    private readonly AppDbContext _context;
    public DeskService(AppDbContext context)
    {
        _context = context;
    }

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    public async Task<RequestState?> CheckDeskStatusForDate(int id, DateTime date)
    {
        var desk = await _context.Desks
            .Include(d => d.BookingRequests)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (desk == null)
        {
            throw new Exception("Desk not found");
        }

        //TODO: we also need to check if a booking request is made on that day for the desk

        var bookingRequest = desk.BookingRequests.FirstOrDefault(x => x.RequestDate.Date == date.Date);
        if (bookingRequest == null && desk.IsHotDesk)
        {
            return RequestState.Free;
        }
        
        if (!desk.IsHotDesk){
            return RequestState.Booked;
        }

        return bookingRequest?.State;
    }

    public async Task<Desk?> CreateDesk(CreateDeskDTO desk)
    {
        //TODO: add validation for desk
        var Location = await _context.Locations.FirstOrDefaultAsync(x => x.Id == desk.LocationId);
        if (Location == null)
        {
            //throw new Exception("Location not found");
            return null;
        }

        if(desk.StaffId != null)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(x => x.Id == desk.StaffId);
            if (staff == null)
            {
                //throw new Exception("Staff not found");
                return null;
            }
        }

        var newDesk = new Desk
        {
            Name = desk.Name,
            IsHotDesk = desk.IsHotDesk,
            LocationId = desk.LocationId
        };

        _context.Desks.Add(newDesk);
        await _context.SaveChangesAsync();

        return newDesk;
    }
}