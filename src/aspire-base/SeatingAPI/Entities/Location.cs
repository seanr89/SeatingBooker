
public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; } = false;
    public int SeatingCount { get; set; } = 0;
    public List<Desk> Desks { get; set; } = new List<Desk>();
}