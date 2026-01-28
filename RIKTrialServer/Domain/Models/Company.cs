namespace RIKTrialServer.Domain.Models
{
    public class Company : Participant
    {
        public string Name { get; set; } = null!;
        public string CompanyCode { get; set; } = null!;
        public int ParticipantAmount { get; set; }
        public string? AdditionalInfo { get; set; } = null!;

        // -- const --

        private Company() { }

        public Company
        (
            Guid id,
            int paymentMethodId,
            string name,
            string companyCode,
            int participantAmount,
            string? additionalInfo
        )
            : base(id, paymentMethodId)
        {
            Name = name;
            CompanyCode = companyCode;
            ParticipantAmount = participantAmount;
            AdditionalInfo = additionalInfo;
        }

    }
}
