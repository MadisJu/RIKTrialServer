using Microsoft.AspNetCore.Mvc;
using RIKTrialServer.Services.Interfaces;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Returns;

namespace RIKTrialServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentMethodController(IPaymentMethodService paymetServ) : ControllerBase
    {
        private readonly IPaymentMethodService _paymentServ = paymetServ;

        [HttpGet]
        [Route("paymentmethods")]
        public async Task<ActionResult<List<PaymentMethodReturnDTO>>> GetPaymentMethods(CancellationToken ctoken)
        {
            return Ok( await _paymentServ.GetPaymentMethods(ctoken));
        }

        [HttpPost]
        [Route("paymentmethod")]
        public async Task<ActionResult<bool>> PostPaymentMethod(PaymentMethodCreationDTO data, CancellationToken ctoken)
        {
            return Ok(await _paymentServ.CreatePaymentMethod(data, ctoken));
        }

        [HttpPut]
        [Route("togglepaymentmethod")]

        public async Task<ActionResult<bool>> TogglePaymentMethod([FromQuery] int paymentId, CancellationToken ctoken)
        {
            return Ok(await _paymentServ.TogglePaymentMethod(paymentId, ctoken));
        }
    }
}
