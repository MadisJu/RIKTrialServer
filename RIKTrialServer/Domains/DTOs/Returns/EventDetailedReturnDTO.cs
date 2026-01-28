namespace RIKTrialServer.Domains.DTOs.Returns
{
    public class EventDetailedReturnDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime Date { get; set; }
        public int ParticipantCount { get; set; }
    }
}
