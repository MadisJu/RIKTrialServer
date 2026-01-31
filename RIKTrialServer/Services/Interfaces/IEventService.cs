using RIKTrialServer.Domains.Models;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Returns;
using RIKTrialSharedModels.Domains.Filters;

namespace RIKTrialServer.Services.Interfaces
{
    public interface IEventService
    {
        public Task<List<EventReturnDTO>> GetEvents(EventFilters filters, CancellationToken ctoken);
        public Task<EventDetailedReturnDTO> GetEvent(Guid eventId, CancellationToken ctoken);
        public Task<bool> AddEvent(EventCreationDTO data, CancellationToken ctoken);
        public Task<bool> DeleteEvent(Guid eventId, CancellationToken ctoken);
        public Task<bool> RegisterParticipant(Guid eventId, Guid participantId, CancellationToken ctoken);
        public Task<bool> UnRegisterParticipant(Guid eventId, Guid participantId, CancellationToken ctoken);

    }
}
