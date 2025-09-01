
namespace SeatingAPI.Contracts.Reads
{
    public record BookingRequestContract(int Id, int DeskId, int StaffId, DateTime RequestDate, string State);
}

