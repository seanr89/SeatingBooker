
public record StaffDTO(int Id, string Name, string Email, bool Active)
{
    public string LocationName { get; set; } = "No Location";
}