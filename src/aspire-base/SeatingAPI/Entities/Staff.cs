
public class Staff
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; } = false;
    public int LocationId { get; set; }
    public Location Location { get; set; }
}