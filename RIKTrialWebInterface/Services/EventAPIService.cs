using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Returns;
using RIKTrialSharedModels.Domains.Filters;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RIKTrialWebInterface.Services
{
    public class EventAPIService
    {
        private readonly HttpClient _httpClient;

        public EventAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EventReturnDTO>> GetEvents(
            EventFilters filters,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"api/Events/events?Page={filters.Page}&PageSize={filters.PageSize}";

            if (filters.StartDate.HasValue)
                url += $"&StartDate={filters.StartDate:yyyy-MM-dd}";

            if (filters.EndDate.HasValue)
                url += $"&EndDate={filters.EndDate:yyyy-MM-dd}";

            return await _httpClient.GetFromJsonAsync<List<EventReturnDTO>>(
                       url,
                       cancellationToken)
                   ?? new List<EventReturnDTO>();
        }

        public async Task<EventDetailedReturnDTO?> GetEvent(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            string url =
                $"api/Events/event?id={id}";

            return await _httpClient.GetFromJsonAsync<EventDetailedReturnDTO>(
                       url,
                       cancellationToken)
                   ?? null;
        }

        public async Task<bool> CreateEvent(EventCreationDTO data, CancellationToken ctoken = default)
        {
            string url = "api/Events/event";

            HttpResponseMessage response =
                await _httpClient.PostAsJsonAsync(
                    url,
                    data,
                    ctoken);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEvent(Guid id, CancellationToken ctoken = default)
        {
            string url = $"api/Events/event/{id}";

            HttpResponseMessage response =
                await _httpClient.DeleteAsync(url, ctoken);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddParticipant
        (
            RegistrationDTO reg,
            CancellationToken ctoken = default
        )
        {
            HttpResponseMessage response =
                await _httpClient.PostAsync(
                    $"api/Events/register?ParticipantId={reg.ParticipantId}&EventId={reg.EventId}", 
                    content: null,
                    ctoken);

            return response.IsSuccessStatusCode;
        }

    }
}
