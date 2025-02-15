
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

    public async Task<Desk?> GetDesk(int id)
    {
        return await _context.Desks
            .Include(d => d.Staff)
            .Include(d => d.BookingRequests)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}