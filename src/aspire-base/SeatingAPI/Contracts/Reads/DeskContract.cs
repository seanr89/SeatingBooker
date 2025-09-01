
namespace SeatingAPI.Contracts.Reads
{
    public record DeskContract(int Id, string Name, string Location, bool IsHotDesk, string StaffName, bool Active);
}
