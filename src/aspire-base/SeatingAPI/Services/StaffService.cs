

using Microsoft.EntityFrameworkCore;

public class StaffService
{
    private readonly AppDbContext _context;
    public StaffService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Request all staff information
    /// </summary>
    /// <returns></returns>
    public async Task<List<Staff>> GetStaff()
    {
        return await _context.Staff.ToListAsync();
    }

    /// <summary>
    /// Get a staff member request
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
    /// Single get staff member by email
    /// </summary>
    /// <param name="email">staff email address</param>
    /// <returns></returns>
    public async Task<Staff?> GetStaffMemberByEmail(string email)
    {
        return await _context.Staff
            .Include(s => s.Location)
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    /// <summary>
    /// Create a new staff member record via the DTO with a cast/convert to a model!
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