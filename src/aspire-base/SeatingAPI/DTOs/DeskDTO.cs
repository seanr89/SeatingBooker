
public class DeskDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public bool IsHotDesk { get; set; }
    public required string StaffName { get; set; }
    public bool Active { get; set; }
}