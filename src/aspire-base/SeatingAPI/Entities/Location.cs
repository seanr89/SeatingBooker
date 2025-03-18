
public class Location : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "Default Location";
    public bool Active { get; set; } = false;
    public int SeatingCount { get; set; } = 0;
    public List<Desk> Desks { get; set; } = [];
}