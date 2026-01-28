using Microsoft.EntityFrameworkCore;
using RIKTrialServer.Domains.Models;
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

        public async Task<bool> AddEvent(Event e, CancellationToken ctoken)
        {
            await _dbc.Events.AddAsync(e, ctoken);
            return 0 < await _dbc.SaveChangesAsync(ctoken);
        }

        public async Task<Event?> GetEventByID(Guid id)
        {
            Event? e = await _dbc.Events.Where(e => e.Id == id).FirstOrDefaultAsync();

            return e;
        }

        public async Task<List<Event>> GetEvents(EventFilters filters)
        {
            int skip = (filters.Page - 1) * filters.PageSize;

            List<Event> e = await ApplyFilters(BaseQuery(), filters)
                .OrderBy(e => e.Date)
                .Skip(skip)
                .Take(filters.PageSize)
                .ToListAsync();

            return e;
        }

        public async Task<bool> UpdateEvent(Event e, CancellationToken ctoken)
        {
            return 0 < await _dbc.SaveChangesAsync(ctoken);
        }

        public async Task<bool> DeleteEvent(Guid id, CancellationToken ctoken)
        {
            Event? ev = await _dbc.Events.FirstOrDefaultAsync(e => e.Id == id, ctoken);

            if (ev != null)
            {
                _dbc.Events.Remove(ev);
                return (0 < await _dbc.SaveChangesAsync(ctoken)); // if the um affected rows??
            }

            return false;
        }

        // -- helpers queries whatever whatever --
        private IQueryable<Event> BaseQuery()
        {
            IQueryable<Event> query = _dbc.Events
                .Include(e => e.Participants)
                    .ThenInclude(ep => ep.Participant);
            return query;
        }
        private static IQueryable<Event> ApplyFilters(IQueryable<Event> query, EventFilters filters)
        {
            if (filters.StartDate is DateTime start)
                query = query.Where(e => e.Date > start);

            if (filters.EndDate is DateTime end)
                query = query.Where(e => e.Date < end);

            return query;
        }


    }
}
