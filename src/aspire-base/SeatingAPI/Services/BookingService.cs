
using Microsoft.EntityFrameworkCore;

public class BookingService
{
    private readonly ILogger<BookingService> _logger;
    private readonly AppDbContext _context;
    public BookingService(AppDbContext context, ILogger<BookingService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Simple request for all booking requests
    /// </summary>
    /// <returns></returns>
    public async Task<List<BookingRequest>> GetBookings()
    {
        return await _context.BookingRequests.ToListAsync();
    }

    /// <summary>
    /// Handle the retrieval of a single booking
    /// Desk and Staff are included in the response currently 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BookingRequest : Nullable</returns>
    public async Task<BookingRequest?> GetBooking(int id)
    {
        return await _context.BookingRequests
            .Include(x => x.Desk)
            .Include(x => x.Staff)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Request for all bookings for a specific location
    /// </summary>
    /// <param name="locationId"></param>
    /// <returns></returns>
    public async Task<List<BookingRequest>> GetBookingsForLocation(int locationId)
    {
        return await _context.BookingRequests
            .Where(x => x.Desk.LocationId == locationId)
            .ToListAsync();
    }

    /// <summary>
    /// Request all booking requests for a specific desk
    /// </summary>
    /// <param name="deskId"></param>
    /// <returns></returns>
    public async Task<List<BookingRequest>> GetBookingsForDesk(int deskId)
    {
        return await _context.BookingRequests
            .Where(x => x.DeskId == deskId)
            .ToListAsync();
    }

    /// <summary>
    /// Create a new booking request
    /// With some simple validation
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    public async Task<BookingRequest?> CreateBooking(CreateBookingRequestDTO booking)
    {
        _logger.LogInformation("Creating booking request for desk {DeskId} on {RequestDate}", booking.DeskId, booking.RequestDate);
        var desk = await _context.Desks.FirstOrDefaultAsync(x => x.Id == booking.DeskId);
        var staff = await _context.Staff.FirstOrDefaultAsync(x => x.Id == booking.StaffId);
        if (desk == null || staff == null)
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
        // If there are any pending or booked requests, we can't book the desk
        if (bookedCount > 0)
        {
            return null;
        }

        //Check for desk availability if its not a hot desk
        if(desk.IsHotDesk == false)
        {
            var isReleased = matchedBookings.Any(x => x.State == RequestState.Free);
            if (!isReleased)
            {
                return null;
            }
        }
        
        // Passed all validations, we can now create the booking

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

    /// <summary>
    /// Request all bookings for a specific location on a specific date
    /// Only returns bookings and no nesting
    /// </summary>
    /// <param name="locationId"></param>
    /// <param name="date"></param>
    /// <returns>List of BookingRequests</returns>
    public async Task<List<BookingRequest>> GetBookingsForLocationOnDate(int locationId, DateTime date)
    {
        return await _context.BookingRequests
            .Where(x => x.Desk.LocationId == locationId && x.RequestDate == date)
            .ToListAsync();
    }

    /// <summary>
    /// Handle a booking to cancellation
    /// if cancelled it will re-cancel the booking
    /// </summary>
    /// <param name="id">Id of the booking to cancel</param>
    /// <returns></returns>
    public async Task<bool> CancelBooking(int id)
    {
        var booking = await _context.BookingRequests.FirstOrDefaultAsync(x => x.Id == id);
        if (booking == null)
        {
            return false;
        }

        booking.State = RequestState.Cancelled;
        await _context.SaveChangesAsync();
        return true;
    }
}