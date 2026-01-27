namespace RIKTrialServer.Domain.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime Date { get; set; }
        public string AdditionalInfo { get; set; } = null!;

        // -- ef navigation --

        private readonly List<EventParticipant> _participants = new();
        public IReadOnlyCollection<EventParticipant> Participants => _participants;

        // -- funcs --
        public void RegisterParticipant(Guid participantId)
        {
            _participants.Add(new EventParticipant(Id, participantId));
        }
    }

    
}
