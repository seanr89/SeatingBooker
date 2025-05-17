using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILocationService
{
    Task<List<Location>> GetLocations();
    Task<Location> GetLocation(int id);
    Task<Location> GetDesksAndBookingsForLocationOnDate(int id, DateTime date);
}
