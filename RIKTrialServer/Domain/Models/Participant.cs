namespace RIKTrialServer.Domain.Models
{
    public class Participant
    {
        public Guid Id { get; set; }
        public int? PaymentMethodId { get; set; }
        public Guid EventParticipantId { get; set; }
        public ParticipantType ParticipantType { get; set; }

        // -- ef navigation --

        private readonly List<EventParticipant> _events = new();
        public IReadOnlyCollection<EventParticipant> Events => _events;
        public PaymentMethod PaymentMethod { get; set; } = null!;
        public Person? Person { get; set; }
        public Company? Company { get; set; }

        // -- const --

        protected Participant() { } 

        protected Participant(Guid id, int paymentMethodId)
        {
            Id = id;
            PaymentMethodId = paymentMethodId;
        }

    }

    public enum ParticipantType
    {
        Person = 0,
        Company = 1
    }
}
