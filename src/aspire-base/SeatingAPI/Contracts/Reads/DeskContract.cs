
namespace SeatingAPI.Contracts.Reads
{
    /// <summary>
    /// Simple desk contract for a location
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Location"></param>
    /// <param name="IsHotDesk"></param>
    /// <param name="StaffName"></param>
    /// <param name="Active"></param>
    /// <returns></returns>
    public record DeskContract(int Id, string Name, string Location, bool IsHotDesk, string StaffName, bool Active);
}
