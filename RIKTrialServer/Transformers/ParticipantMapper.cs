using RIKTrialServer.Domains.Models;
using RIKTrialSharedModels.Domain.Returns;
using RIKTrialSharedModels.Domain.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RIKTrialServer.Transformers
{
    public static class ParticipantMapper
    {
        public static ParticipantReturnDTO MapToParticipantReturnDTO(Participant p)
        {
            ParticipantReturnDTO ret = new();

            switch (p)
            {
                case Person person:
                    ret.Id = person.Id;
                    ret.PaymentMethod = PaymentMethodMapper.MapToPaymentMethodReturn(person.PaymentMethod);
                    ret.IdNumber = person.IdNumber;
                    ret.FirstName = person.FirstName;
                    ret.LastName = person.LastName;
                    ret.AdditionalInfo = person.AdditionalInfo;
                    break;

                case Company comp:
                    ret.Id = comp.Id;
                    ret.Name = comp.Name;
                    ret.PaymentMethod = PaymentMethodMapper.MapToPaymentMethodReturn(comp.PaymentMethod);
                    ret.ComapnyCode = comp.CompanyCode;
                    ret.ParticipantAmount = comp.ParticipantAmount;
                    ret.AdditionalInfo = comp.AdditionalInfo;
                    break;
            }

            return ret;
        }

        public static ParticipantLightReturnDTO MapToParticipantLightReturnDTO(Participant p)
        {
            ParticipantLightReturnDTO ret = new();

            switch (p)
            {
                case Person person:
                    ret.Id = person.Id;
                    ret.IdNumber = person.IdNumber;
                    ret.FirstName = person.FirstName;
                    ret.LastName = person.LastName;
                    break;
                case Company comp:
                    ret.Id = comp.Id;
                    ret.Name = comp.Name;
                    ret.ComapnyCode = comp.CompanyCode;
                    ret.ParticipantAmount = comp.ParticipantAmount;
                    break;
            }

            return ret;
        }
    }
}
