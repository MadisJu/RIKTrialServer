using RIKTrialServer.Domains.Models;

namespace RIKTrialServer.Repositories.Interfaces
{
    public interface IPaymentMethodRepository
    {
        public Task<bool> AddPaymentMethod(PaymentMethod pm, CancellationToken ctoken);
        public Task<bool> UpdatePaymentMethod(PaymentMethod pm, CancellationToken ctoken);
        public Task<List<PaymentMethod>> GetPaymentMethods(CancellationToken ctoken);
        public Task<List<PaymentMethod>> GetAllPaymentMethods(CancellationToken ctoken);
        public Task<PaymentMethod?> GetPaymentMethod(int id, CancellationToken ctoken);
    }
}
