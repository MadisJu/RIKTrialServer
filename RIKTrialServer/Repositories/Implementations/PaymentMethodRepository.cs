using Microsoft.EntityFrameworkCore;
using RIKTrialServer.Domains.Models;
using RIKTrialServer.Infra.Persistance;
using RIKTrialServer.Repositories.Interfaces;

namespace RIKTrialServer.Repositories.Implementations
{
    public class PaymentMethodRepository(ServerDbContext dbc) : IPaymentMethodRepository  
    {
        private readonly ServerDbContext _dbc = dbc;

        public async Task<bool> AddPaymentMethod(PaymentMethod pm, CancellationToken ctoken)
        {
            _dbc.PaymentMethods.Add(pm);
            return 0 < await _dbc.SaveChangesAsync(ctoken);
        }

        public async Task<PaymentMethod?> GetPaymentMethod(int id, CancellationToken ctoken)
        {
            return await _dbc.PaymentMethods.FirstOrDefaultAsync(e => e.Id == id && !e.Disabled, ctoken);
        }


        public async Task<List<PaymentMethod>> GetPaymentMethods(CancellationToken ctoken)
        {
            return await _dbc.PaymentMethods.Where(e => !e.Disabled).ToListAsync(ctoken);
        }

        public async Task<List<PaymentMethod>> GetAllPaymentMethods(CancellationToken ctoken)
        {
            return await _dbc.PaymentMethods.ToListAsync(ctoken);
        }


        public async Task<bool> UpdatePaymentMethod(PaymentMethod pm, CancellationToken ctoken)
        {
            _dbc.PaymentMethods.Update(pm);

            return 0 < await _dbc.SaveChangesAsync(ctoken);
        }
    }
}
