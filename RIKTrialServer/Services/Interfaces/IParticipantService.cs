using RIKTrialServer.Domains.Models;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Returns;

namespace RIKTrialServer.Services.Interfaces
{
    public interface IParticipantService
    {
        public Task<ParticipantReturnDTO> GetParticipant(Guid id, CancellationToken ctoken);
        public Task<List<ParticipantReturnDTO>> GetParticipants(CancellationToken ctoken);

        public Task<List<ParticipantLightReturnDTO>> GetEventParticipants(Guid eventId, CancellationToken ctoken);
        public Task<bool> CreateParticipant(ParticipantCreationDTO data,  CancellationToken ctoken);

        public Task<bool> UpdateParticipant(ParticipantCreationDTO data, Guid id, CancellationToken ctoken);

        public Task<bool> DeleteParticipant(Guid id, CancellationToken ctoken);

    }
}
