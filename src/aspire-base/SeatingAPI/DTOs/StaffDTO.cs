
public class StaffDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public bool Active { get; set; }
    public string LocationName { get; set; }
}