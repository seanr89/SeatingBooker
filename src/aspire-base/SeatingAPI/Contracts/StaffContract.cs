
namespace SeatingAPI.Contracts.Reads
{
    public record StaffContract(int Id, string Name, string Email, bool Active)
    {
        public string LocationName { get; set; } = "No Location";
    }
}

namespace SeatingAPI.Contracts.Creates
{
    public class CreateStaffContract
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int LocationId { get; set; }
    }
}

namespace SeatingAPI.Contracts.Updates
{
    public class UpdateStaffContract
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int LocationId { get; set; }
        public bool Active { get; set; }
    }
}
