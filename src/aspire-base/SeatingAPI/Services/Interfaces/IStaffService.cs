public interface IStaffService
{
    Task<List<Staff>> GetStaff();
    Task<Staff?> GetStaffMember(int id);
    Task<Staff?> GetStaffMemberByEmail(string email);
    Task<Staff?> CreateStaff(CreateStaffDTO staff);
}
