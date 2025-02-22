

public class LocationDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int DeskCount { get; set; } = 0;
    public List<DeskDTO> Desks { get; set; } = [];
}