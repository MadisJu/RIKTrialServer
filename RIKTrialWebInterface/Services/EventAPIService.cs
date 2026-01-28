using RIKTrialSharedModels.Domain.Returns;
using RIKTrialSharedModels.Domains.Filters;

namespace RIKTrialWebInterface.Services
{
    public class EventAPIService
    {
        private readonly HttpClient _httpClient;

        public EventAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EventReturnDTO>> GetEventsAsync(
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
    }
}
