
public class BookingRequestDTO
{
    public int Id { get; set; }
    public int DeskId { get; set; }
    public Desk Desk { get; set; }
    public int StaffId { get; set; }
    public Staff Staff { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}