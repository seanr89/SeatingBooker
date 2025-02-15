public class Desk
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; } = true;
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public bool IsHotDesk { get; set; } = false;
    public int? StaffId { get; set; }
    public Staff? Staff { get; set; }
    public List<BookingRequest> BookingRequests { get; set; }
}