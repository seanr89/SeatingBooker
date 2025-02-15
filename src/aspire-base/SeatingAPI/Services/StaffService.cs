

using Microsoft.EntityFrameworkCore;

public class StaffService
{
    private readonly AppDbContext _context;
    public StaffService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Simple request to get all staff information
    /// </summary>
    /// <returns></returns>
    public async Task<List<Staff>> GetStaff()
    {
        return await _context.Staff.ToListAsync();
    }

    public async Task<Staff> GetStaff(int id)
    {
        return await _context.Staff
            .FirstAsync(s => s.Id == id);
    }
}