

public record LocationDTO(int Id, string Name)
{
    public int DeskCount { get; set; } = 0;
    public List<DeskDTO> Desks { get; set; } = [];
}