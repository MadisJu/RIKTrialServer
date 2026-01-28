namespace RIKTrialServer.Domains.Models
{
    public class Person : Participant
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string IdNumber { get; set; } = null!;
        public string? AdditionalInfo { get; set; } = null!;

        public override int ParticipantCount => 1;

        // -- const --
        private Person() { }

        public Person
        (
            Guid id,
            int paymentMethodId,
            string firstName,
            string lastName,
            string idNumber,
            string? additionalInfo
        )
        : base(id, paymentMethodId)
        {
            FirstName = firstName;
            LastName = lastName;
            IdNumber = idNumber;
            AdditionalInfo = additionalInfo;
        }

    }
}
