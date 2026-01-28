namespace RIKTrialWebInterface.Services
{
    public class BackendHealthService
    {
        private readonly HttpClient _httpClient;

        public BackendHealthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsBackendReadyAsync
        (
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.GetAsync("/Health/health");

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
