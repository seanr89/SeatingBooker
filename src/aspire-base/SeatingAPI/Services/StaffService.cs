

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

    /// <summary>
    /// Single get staff member request
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Staff or null record</returns>
    public async Task<Staff?> GetStaffMember(int id)
    {
        return await _context.Staff
            .Include(s => s.Location)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="staff"></param>
    /// <returns></returns>
    public async Task<Staff?> CreateStaff(CreateStaffDTO staff)
    {
        var locationRequest = _context.Locations.FirstOrDefaultAsync(x => x.Id == staff.LocationId);
        await locationRequest;
        var location = locationRequest.Result;
        if (location == null)
        {
            return null;
        }

        var newStaff = new Staff
        {
            Name = staff.Name,
            Email = staff.Email,
            Location = location,
            Active = true
        };

        _context.Staff.Add(newStaff);
        await _context.SaveChangesAsync();
        return newStaff;
    }
}