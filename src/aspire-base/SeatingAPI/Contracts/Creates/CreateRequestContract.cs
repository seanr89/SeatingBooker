
namespace SeatingAPI.Contracts.Creates
{
    public class CreateRequestContract
    {
        public int Id { get; set; }
        public int DeskId { get; set; }
        public int StaffId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
