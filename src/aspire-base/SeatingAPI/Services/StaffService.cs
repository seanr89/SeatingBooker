

public class StaffService
{
    public StaffService()
    {
        
    }

    /// <summary>
    /// Simple request to get all staff information
    /// </summary>
    /// <returns></returns>
    public List<Staff> GetStaff()
    {
        return new List<Staff>
        {
            new Staff
            {
                Id = 1,
                Name = "Staff 1",
                Email = "s@email.com",
                Active = true,
                LocationId = 1
            },
        };
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