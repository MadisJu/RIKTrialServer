using RIKTrialServer.Domains.Models;
using RIKTrialSharedModels.Domain.Returns;

namespace RIKTrialServer.Transformers
{
    public static class EventMapper
    {
        public static EventReturnDTO MapToEventsResponse (Event ev, int participantCount)
        {
            return new EventReturnDTO
            {
                Id = ev.Id,
                ParticipantCount = participantCount,
                Date = ev.Date,
                Location = ev.Location,
                Name = ev.Name,
            };
        }
    }
}
