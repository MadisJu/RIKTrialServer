using RIKTrialServer.Domains.Models;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Filters;
using RIKTrialSharedModels.Domain.Returns;
using RIKTrialSharedModels.Domain.Updates;

namespace RIKTrialServer.Services.Interfaces
{
    public interface IParticipantService
    {
        public Task<ParticipantReturnDTO> GetParticipant(Guid id, CancellationToken ctoken);
        public Task<List<ParticipantReturnDTO>> GetParticipants(ParticipantFilters filters, CancellationToken ctoken);

        public Task<List<ParticipantLightReturnDTO>> GetEventParticipants(ParticipantFilters filters, Guid eventId, CancellationToken ctoken);
        public Task<Guid?> CreateParticipant(ParticipantCreationDTO data,  CancellationToken ctoken);

        public Task<bool> UpdateParticipant(ParticipantUpdateDTO data, Guid id, CancellationToken ctoken);

        public Task<bool> DeleteParticipant(Guid id, CancellationToken ctoken);

    }
}
