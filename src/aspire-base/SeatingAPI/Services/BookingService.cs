
using Microsoft.EntityFrameworkCore;

public class BookingService
{
    private readonly AppDbContext _context;
    public BookingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookingRequest>> GetBookings()
    {
        return await _context.BookingRequests.ToListAsync();
    }

    public async Task<BookingRequest?> GetBooking(int id)
    {
        return await _context.BookingRequests.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// TODO: need to add validation for booking!!
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    public async Task<BookingRequest?> CreateBooking(BookingRequestDTO booking)
    {
        var desk = await _context.Desks.FirstOrDefaultAsync(x => x.Id == booking.DeskId);
        if (desk == null)
        {
            return null;
        }
        var staff = await _context.Staff.FirstOrDefaultAsync(x => x.Id == booking.StaffId);
        if (staff == null)
        {
            return null;
        }

        // We want to search for other bookings on the same desk on the same date
        var matchedBookings = await _context.BookingRequests
            .Where(x => x.DeskId == booking.DeskId && x.RequestDate == booking.RequestDate)
            .ToListAsync();
        
        var bookedCount = matchedBookings.Count(x => 
            x.State == RequestState.Pending 
            || x.State == RequestState.Booked);
        if (bookedCount > 0)
        {
            return null;
        }

        //TODO: check for desk availability if hotDesk is not there
        if(desk.IsHotDesk == false)
        {
            var isReleased = matchedBookings.Any(x => x.State == RequestState.Free);
            if (!isReleased)
            {
                return null;
            }
        }
        
        var bookingRequest = new BookingRequest
        {
            DeskId = booking.DeskId,
            StaffId = booking.StaffId,
            RequestDate = booking.RequestDate,
            State = RequestState.Booked
        };

        await _context.BookingRequests.AddAsync(bookingRequest);
        await _context.SaveChangesAsync();
        return bookingRequest;   
    }
}