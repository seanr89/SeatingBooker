
public class LocationService
{
    public LocationService()
    {
        
    }

    public List<Location> GetLocations()
    {
        return new List<Location>
        {
            new Location
            {
                Id = 1,
                Name = "Location 1"
            },
            new Location
            {
                Id = 2,
                Name = "Location 2"
            },
            new Location
            {
                Id = 3,
                Name = "Location 3"
            }
        };
    }

    public Location GetLocation(int id)
    {
        return new Location
        {
            Id = id,
            Name = "Location " + id
        };
    }
}