public class Desk
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool Active { get; set; } = true;
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public bool IsHotDesk { get; set; } = false;
    public List<BookingRequest> BookingRequests { get; set; } = [];
    public int? StaffId { get; set; }
    public Staff? Staff { get; set; }
}