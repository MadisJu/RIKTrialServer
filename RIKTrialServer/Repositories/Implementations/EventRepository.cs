using Microsoft.EntityFrameworkCore;
using RIKTrialServer.Domain.Models;
using RIKTrialServer.Domains.Filters;
using RIKTrialServer.Infra.Persistance;
using RIKTrialServer.Repositories.Interfaces;

namespace RIKTrialServer.Repositories.Implementations
{
    public class EventRepository       
    (
        ServerDbContext dbc
    )
    : IEventRepository
    {

        private readonly ServerDbContext _dbc = dbc;

        public async Task AddEvent(Event e, CancellationToken ctoken)
        {
            await _dbc.Events.AddAsync(e, ctoken);
            await _dbc.SaveChangesAsync(ctoken);
        }

        public async Task<Event?> GetEventByID(Guid id)
        {
            Event? e = await _dbc.Events.Where(e => e.Id == id).FirstOrDefaultAsync();

            return e;
        }

        public async Task<List<Event>> GetEvents(EventFilters filters)
        {
            List<Event> e = await ApplyFilters(BaseQuery(), filters).ToListAsync();

            return e;
        }

        public async Task UpdateEvent(Event e, CancellationToken ctoken)
        {
            await _dbc.SaveChangesAsync(ctoken);
        }

        // -- helpers queries whatever whatever --
        private IQueryable<Event> BaseQuery()
        {
            IQueryable<Event> query = _dbc.Events;
            return query;
        }
        private IQueryable<Event> ApplyFilters(IQueryable<Event> query, EventFilters filters)
        {
            if (filters.StartDate is DateTime start)
                query = query.Where(e => e.Date > start);

            if (filters.EndDate is DateTime end)
                query = query.Where(e => e.Date < end);

            return query;
        }
    }
}
