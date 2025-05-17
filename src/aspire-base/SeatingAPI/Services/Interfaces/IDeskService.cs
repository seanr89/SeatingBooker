using System.Threading.Tasks;
using System.Collections.Generic;

public interface IDeskService
{
    Task<List<Desk>> GetDesks();
    Task<Desk?> GetDeskById(int id);
    Task<List<Desk>> GetDesksByLocation(int locationId);
    Task<RequestState?> CheckDeskStatusForDate(int id, DateTime date);
    Task<Desk?> CreateDesk(CreateDeskDTO desk);
}
