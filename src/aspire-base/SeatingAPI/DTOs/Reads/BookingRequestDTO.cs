
public record BookingRequestDTO(int Id, int DeskId, int StaffId, DateTime RequestDate)
{
    public string State { get; set; } = "Unknown";
}