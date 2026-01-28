namespace RIKTrialSharedModels.Domain.Creation
{
    public class EventCreationDTO
    {
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public string AdditionalInfo { get; set; } = null!;
    }
}
