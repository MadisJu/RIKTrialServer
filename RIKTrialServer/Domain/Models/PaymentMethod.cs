namespace RIKTrialServer.Domain.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Method { get; set; } = null!;

        // -- ef navigations --

        public List<Participant> Participants { get; set; } = new();
    }
}
