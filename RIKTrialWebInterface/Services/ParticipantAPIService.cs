using System.Net.Http.Json;
using RIKTrialSharedModels.Domain.Creation;

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
    }
}

