using RIKTrialServer.Domains.Models;
using RIKTrialSharedModels.Domain.Returns;

namespace RIKTrialServer.Transformers
{
    public static class PaymentMethodMapper
    {
        public static PaymentMethodReturnDTO MapToPaymentMethodReturn(PaymentMethod pm)
        {
            return new PaymentMethodReturnDTO()
            {
                Id = pm.Id,
                PaymentMethod = pm.Method,
                Disabled = pm.Disabled,
            };
        }
    }
}
