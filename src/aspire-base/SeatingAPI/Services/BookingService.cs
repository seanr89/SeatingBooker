
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
    public async Task<BookingRequest> CreateBooking(BookingRequestDTO booking)
    {
        throw new NotImplementedException();
    //     _context.BookingRequests.Add(booking);
    //     await _context.SaveChangesAsync();
    //     return booking;
    // }
}