
public class CreateBookingRequestDTO
{
    public int DeskId { get; set; }
    public int StaffId { get; set; }
    /// <summary>
    /// Should expect a valid date and time passed??
    /// </summary>
    /// <value></value>
    public DateTime RequestDate { get; set; }
}