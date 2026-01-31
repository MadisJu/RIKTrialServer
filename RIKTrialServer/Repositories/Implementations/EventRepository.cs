using Microsoft.EntityFrameworkCore;
using RIKTrialServer.Domains.Models;
using RIKTrialSharedModels.Domains.Filters;
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

        public async Task<Event?> GetEventByID(Guid id, CancellationToken ctoken)
        {

            return await _dbc.Events
                .Include(e => e.Participants)
                    .ThenInclude(ep => ep.Participant)
                        .ThenInclude(p => p.PaymentMethod)
                .FirstOrDefaultAsync(e => e.Id == id, ctoken);
        }

        public async Task<List<Event>> GetEvents(EventFilters filters, CancellationToken ctoken)
        {
            int skip = (filters.Page - 1) * filters.PageSize;

            List<Event> e = await ApplyFilters(BaseQuery(), filters)
                .OrderBy(e => e.Date)
                .Skip(skip)
                .Take(filters.PageSize)
                .ToListAsync(ctoken);

            return e;
        }

        public async Task<bool> UpdateEvent(Event e, CancellationToken ctoken)
        {
            return 0 < await _dbc.SaveChangesAsync(ctoken);
        }

        public async Task<bool> RemoveEvent(Event e, CancellationToken ctoken)
        {
            _dbc.Events.Remove(e);
            return 0 < await _dbc.SaveChangesAsync(ctoken);
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
