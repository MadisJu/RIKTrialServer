using RIKTrialServer.Domains.Models;
using RIKTrialServer.Domains.Filters;

namespace RIKTrialServer.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<Event?> GetEventByID(Guid id);
        Task<List<Event>> GetEvents(EventFilters filters);
        Task<bool> AddEvent(Event e, CancellationToken ctoken);
        Task<bool> UpdateEvent(Event e, CancellationToken ctoken);
        Task<bool> DeleteEvent(Guid id, CancellationToken ctoken);
    }
}
