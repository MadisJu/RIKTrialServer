using System;
using System.Collections.Generic;
using System.Text;

namespace RIKTrialSharedModels.Domain.Returns
{
    public class PaymentMethodReturnDTO
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public bool Disabled { get; set; }
    }
}
