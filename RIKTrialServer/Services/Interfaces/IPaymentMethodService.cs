using RIKTrialServer.Domains.Models;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Returns;

namespace RIKTrialServer.Services.Interfaces
{
    public interface IPaymentMethodService
    {
        public Task<List<PaymentMethodReturnDTO>> GetPaymentMethods(CancellationToken ctoken);

        public Task<List<PaymentMethodReturnDTO>> GetAllPaymentMethods(CancellationToken ctoken);

        public Task<bool> CreatePaymentMethod(PaymentMethodCreationDTO data, CancellationToken ctoken);

        public Task<bool> TogglePaymentMethod(int paymentMethodId, CancellationToken ctoken);

    }
}
