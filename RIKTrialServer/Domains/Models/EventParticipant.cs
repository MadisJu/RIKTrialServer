namespace RIKTrialServer.Domain.Models
{
    public class EventParticipant
    {
        public Guid EventId { get; set; }
        public Guid ParticipantId { get; set; }
        private EventParticipant() { }
        public EventParticipant(Guid eventId, Guid participantId)
        {
            EventId = eventId;
            ParticipantId = participantId;
        }

        // -- ef navigation --

        public Event Event { get; set; } = null!;
        public Participant Participant { get; set; } = null!;
    }
}
