
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
}