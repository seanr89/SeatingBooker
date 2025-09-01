
public class Location : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "Default Location";
    public string Address1 { get; set; } = "";
    public string Address2 { get; set; } = "";
    public string City { get; set; } = "";
    public string State { get; set; } = "";
    public bool Active { get; set; } = false;
    public int SeatingCount { get; set; } = 0;
    public List<Desk> Desks { get; set; } = [];
}