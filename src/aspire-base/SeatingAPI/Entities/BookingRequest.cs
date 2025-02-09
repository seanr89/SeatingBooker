

public class BookingRequest
{
    public int Id { get; set; }
    public DateTime RequestDate { get; set; }
    public int DeskId { get; set; }
    public Desk Desk { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
}