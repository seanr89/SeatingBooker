

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

    public Staff GetStaff(int id)
    {
        return new Staff
        {
            Id = id,
            Name = "Staff " + id,
            Email = "s" + id + "@email.com",
            Active = true,
            LocationId = 1
        };
    }
}