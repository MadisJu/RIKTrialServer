namespace RIKTrialSharedModels.Domain.Returns
{
    public class EventDetailedReturnDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime Date { get; set; }
        public List<ParticipantLightReturnDTO> Participants { get; set; } = new();

        public string? AdditionalInfo { get; set; }
    }
}
