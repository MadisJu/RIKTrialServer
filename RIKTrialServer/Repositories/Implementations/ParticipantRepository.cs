using Microsoft.EntityFrameworkCore;
using RIKTrialServer.Domains.Models;
using RIKTrialServer.Infra.Persistance;
using RIKTrialServer.Repositories.Interfaces;
using RIKTrialSharedModels.Domain.Filters;

namespace RIKTrialServer.Repositories.Implementations
{
    public class ParticipantRepository(ServerDbContext dbc) : IParticipantRepository
    {
        private readonly ServerDbContext _dbc = dbc;

        public async Task<bool> AddParticipant(Participant p, CancellationToken ctoken)
        {
            await _dbc.AddAsync(p, ctoken);
            return 0 < await _dbc.SaveChangesAsync(ctoken);
        }

        public async Task<List<Participant>> GetEventParticipants(ParticipantFilters filters, Guid eventId, CancellationToken ctoken)
        {
            int skip = (filters.Page - 1) * filters.PageSize;

            return await _dbc.EventParticipants
                .Where(ep => ep.EventId == eventId)
                .Select(ep => ep.Participant)
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(filters.PageSize)
                .ToListAsync(ctoken);
        }

        public async Task<Participant?> GetParticipant(Guid id, CancellationToken ctoken)
        {
            return await _dbc.Participants
                .Include(p => p.PaymentMethod)
                .FirstOrDefaultAsync(p => p.Id == id, ctoken);
        }

        public async Task<List<Participant>> GetParticipants(ParticipantFilters filters, CancellationToken ctoken)
        {

            int skip = (filters.Page - 1) * filters.PageSize;

            return await BaseQuery()
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(filters.PageSize)
                .ToListAsync(ctoken);
        }

        public async Task<bool> UpdateParticipant(Participant p, CancellationToken ctoken)
        {
            _dbc.Participants.Update(p);
            return 0 < await _dbc.SaveChangesAsync(ctoken);
        }
        public async Task<bool> RemoveAsync(Participant p, CancellationToken ctoken)
        {
            _dbc.Participants.Remove(p);
            return 0 < await _dbc.SaveChangesAsync(ctoken);
        }

        // -- Helpers --

        private IQueryable<Participant> BaseQuery()
        {
            IQueryable<Participant> query = _dbc.Participants
                .Include(p => p.PaymentMethod);

            return query;
        }
    }
}
