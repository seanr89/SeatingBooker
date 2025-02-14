
public class BookingService
{
    private readonly AppDbContext _context;
    public BookingService(AppDbContext context)
    {
        _context = context;
    }
}