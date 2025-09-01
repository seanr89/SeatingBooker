
namespace SeatingAPI.Contracts.Creates
{
    public class CreateDeskContract
    {
        public string? Name { get; set; }
        public int LocationId { get; set; }
        public bool IsHotDesk { get; set; }
        public int? StaffId { get; set; }
    }
}
