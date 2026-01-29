using RIKTrialServer.Domains.Models;
using RIKTrialServer.Repositories.Interfaces;
using RIKTrialServer.Services.Interfaces;
using RIKTrialServer.Transformers;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Returns;

namespace RIKTrialServer.Services.Implementations
{
    public class PaymentMethodService(IPaymentMethodRepository repo) : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _repo = repo;
        public async Task<bool> CreatePaymentMethod(PaymentMethodCreationDTO data, CancellationToken ctoken)
        {
            PaymentMethod pm = new()
            {
                Disabled = false,
                Method = data.Name
            };

            return await _repo.AddPaymentMethod(pm, ctoken);
        }

        public async Task<List<PaymentMethodReturnDTO>> GetAllPaymentMethods(CancellationToken ctoken)
        {
            List<PaymentMethod> methods = await _repo.GetAllPaymentMethods(ctoken);

            return methods.Select(pm => PaymentMethodMapper.MapToPaymentMethodReturn(pm)).ToList();
        }

        public async Task<List<PaymentMethodReturnDTO>> GetPaymentMethods(CancellationToken ctoken)
        {
            List<PaymentMethod> methods = await _repo.GetPaymentMethods(ctoken);

            return methods.Select(pm => PaymentMethodMapper.MapToPaymentMethodReturn(pm)).ToList();
        }

        public async Task<bool> TogglePaymentMethod(int paymentMethodId, CancellationToken ctoken)
        {
            PaymentMethod? pm = await _repo.GetPaymentMethod(paymentMethodId, ctoken);

            if (pm == null) return false;

            pm.Disabled = !pm.Disabled;

            return await _repo.UpdatePaymentMethod(pm, ctoken);
        }
    }
}
