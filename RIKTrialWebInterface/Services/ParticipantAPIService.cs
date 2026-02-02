using System.Net.Http.Json;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Filters;
using RIKTrialSharedModels.Domain.Returns;
using RIKTrialSharedModels.Domain.Updates;

namespace RIKTrialWebInterface.Services
{
    public sealed class ParticipantAPIService(HttpClient http)
    {
        private readonly HttpClient _http = http;

        public async Task<Guid> CreateParticipant
            (
                ParticipantCreationDTO dto,
                CancellationToken ctoken = default
            )
        {
            HttpResponseMessage response =
                await _http.PostAsJsonAsync
                (
                    $"api/Participant/participant",
                    dto,
                    ctoken
                );

            response.EnsureSuccessStatusCode();

            Guid id = await response.Content.ReadFromJsonAsync<Guid>(ctoken);

            return id;
        }

        public async Task<ParticipantReturnDTO?> GetParticipant
            (
                Guid id,
                CancellationToken ctoken = default
            )
        {
            return await _http.GetFromJsonAsync<ParticipantReturnDTO>
                (
                    $"api/Participant/participant?userId={id}",
                    ctoken
                ) ?? null;
        }

        public async Task<List<ParticipantReturnDTO>> GetAllParticipants
            (
                ParticipantFilters filters,
                CancellationToken ctoken = default
            )
        {
            return await _http.GetFromJsonAsync<List<ParticipantReturnDTO>>
                (
                    $"api/Participant/allparticipants?Page={filters.Page}&PageSize={filters.PageSize}",
                    ctoken
                ) ?? throw new Exception("Osavõtjate fetchimine failis.");
        }

        public async Task<List<ParticipantLightReturnDTO>> GetEventParticipants
            (
                Guid eventId,
                ParticipantFilters filters,
                CancellationToken ctoken = default
            )
        {
            return await _http.GetFromJsonAsync<List<ParticipantLightReturnDTO>>
                (
                    $"api/Participant/participants?eventId={eventId}&Page={filters.Page}&PageSize={filters.PageSize}",
                    ctoken
                ) ?? throw new Exception("Osavõtjate fetchimine failis.");
        }


        public async Task<bool> UpdateParticipant
            (
                Guid id,
                ParticipantUpdateDTO dto,
                CancellationToken ctoken = default
            )
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync
                (
                    $"api/Participant/participant?id={id}",
                    dto,
                    ctoken
                );

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteParticipant
            (
            Guid id,
            CancellationToken ctoken = default
            )
        {
            HttpResponseMessage response = await _http.DeleteAsync
                (
                $"api/Participant/participant?id={id}",
                ctoken
                );

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}

