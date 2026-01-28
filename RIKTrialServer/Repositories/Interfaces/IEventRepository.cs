using RIKTrialServer.Domain.Models;
using RIKTrialServer.Domains.Filters;

namespace RIKTrialServer.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<Event?> GetEventByID(Guid id);
        Task<List<Event>> GetEvents(EventFilters filters);
        Task AddEvent(Event e, CancellationToken ctoken);
        Task UpdateEvent(Event e, CancellationToken ctoken);
    }
}
