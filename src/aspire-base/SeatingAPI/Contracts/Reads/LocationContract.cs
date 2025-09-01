
using SeatingAPI.Contracts.Reads;

namespace SeatingAPI.Contracts.Reads
{
    public record LocationContract(int Id, string Name)
    {
        public int DeskCount { get; set; } = 0;
        public List<DeskContract> Desks { get; set; } = [];
    }
}
