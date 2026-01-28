using RIKTrialServer.Domains.Models;
using RIKTrialServer.Domains.DTOs.Creation;
using RIKTrialServer.Domains.DTOs.Returns;
using RIKTrialServer.Domains.Filters;

namespace RIKTrialServer.Services.Interfaces
{
    public interface IEventService
    {
        public Task<List<EventReturnDTO>> GetEvents(EventFilters filters);
        public Task<Event?> GetEvent(Guid eventId);
        public Task<bool> AddEvent(EventCreationDTO data, CancellationToken ctoken);
        public Task DeleteEvent(Guid eventId, CancellationToken ctoken);
        public Task RegisterParticipant(Guid eventId, Guid participantId, CancellationToken ctoken);
    }
}
