namespace RIKTrialServer.Domain.Models
{
    public class Participant
    {
        public Guid Id { get; set; }
        public int? PaymentMethodId { get; set; }

        // -- ef navigation --

        private readonly List<EventParticipant> _events = new();
        public IReadOnlyCollection<EventParticipant> Events => _events;
        public PaymentMethod PaymentMethod { get; set; } = null!;

        // -- const --

        protected Participant() { } 

        protected Participant(Guid id, int paymentMethodId)
        {
            Id = id;
            PaymentMethodId = paymentMethodId;
        }

    }
}
