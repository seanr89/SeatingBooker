
namespace SeatingAPI.Contracts.Reads
{
    public record LocationContract(int Id, string Name, string Address1, string Address2, string City, string State, bool Active)
    {
        public int DeskCount { get; set; } = 0;
        public List<DeskContract> Desks { get; set; } = [];
    }
}
