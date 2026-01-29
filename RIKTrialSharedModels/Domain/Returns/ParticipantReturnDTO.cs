using RIKTrialSharedModels.Domain.Types;

namespace RIKTrialSharedModels.Domain.Returns
{
    public class ParticipantReturnDTO
    {
        public ParticipantType Type { get; set; }
        public Guid Id { get; set; }
        public PaymentMethodReturnDTO PaymentMethod { get; set; } = null!;
        public string? AdditionalInfo { get; set; }

        // For person type participant
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdNumber { get; set; }

        // For Company type

        public string? Name { get; set; }
        public string? ComapnyCode { get; set; }
        public int? ParticipantAmount { get; set; }

    }
}
