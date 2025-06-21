
namespace SeatingAPI.DTOs;

public record StaffDTO(int Id, string Name, string Email, bool Active)
{
    public string LocationName { get; set; } = "No Location";
}

public class CreateStaffDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int LocationId { get; set; }   
}