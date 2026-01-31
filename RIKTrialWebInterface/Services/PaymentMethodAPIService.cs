using RIKTrialSharedModels.Domain.Returns;
using static System.Net.WebRequestMethods;

namespace RIKTrialWebInterface.Services
{
    public class PaymentMethodAPIService(HttpClient http)
    {
        private readonly HttpClient _http = http;

        public async Task<List<PaymentMethodReturnDTO>> GetPaymentMethods(
            CancellationToken ctoken = default)
        {
            List<PaymentMethodReturnDTO>? result =
                await _http.GetFromJsonAsync<List<PaymentMethodReturnDTO>>(
                    "api/PaymentMethod/paymentmethods",
                    ctoken);

            return result ?? new();
        }
    }
}
