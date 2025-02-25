

/// <summary>
/// Unique desk booking request model
/// </summary>
public class BookingRequest : AuditableEntity
{
    public int Id { get; set; }
    public DateTime RequestDate { get; set; }
    public int DeskId { get; set; }
    public Desk Desk { get; set; }
    public int StaffId { get; set; }
    public Staff Staff { get; set; }
    public RequestState State { get; set; }
}