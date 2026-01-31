using RIKTrialServer.Domains.Models;
using RIKTrialServer.Repositories.Interfaces;
using RIKTrialServer.Services.Interfaces;
using RIKTrialServer.Transformers;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Returns;
using RIKTrialSharedModels.Domains.Filters;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RIKTrialServer.Services.Implementations
{
    public class EventService(IEventRepository repo) : IEventService
    {
        private readonly IEventRepository _eventRepo = repo;
        public async Task<bool> AddEvent(EventCreationDTO data, CancellationToken ctoken)
        {
            if (data.Date < DateTime.Now) return false;
            if (data.AdditionalInfo.Length > 1000) return false;

            Event ev = new Event
            (
                Guid.NewGuid(),
                data.Name,
                data.Location,
                data.Date,
                data.AdditionalInfo
            );

            return await _eventRepo.AddEvent(ev, ctoken);
        }

        public async Task<bool> DeleteEvent(Guid eventId, CancellationToken ctoken)
        {
            Event? ev = await _eventRepo.GetEventByID(eventId, ctoken)
                ?? throw new Exception("Proovite kustutada mitte eksisteerivat üritust");

            if (ev.Date < DateTime.Now) throw new Exception("Ei saa kustutada minevikust");

            return await _eventRepo.RemoveEvent(ev, ctoken);
        }

        public async Task<EventDetailedReturnDTO> GetEvent(Guid eventId, CancellationToken ctoken)
        {
            Event? ev = await _eventRepo.GetEventByID(eventId, ctoken) ?? throw new Exception("No event");

            return EventMapper.MapToEventResponse(ev);
        }

        public async Task<List<EventReturnDTO>> GetEvents(EventFilters filters, CancellationToken ctoken)
        {
            List<Event> events = await _eventRepo.GetEvents(filters, ctoken);

            return events.Select(ev => EventMapper.MapToEventsResponse(ev, ParticipantCount(ev))).ToList();
        }

        public  async Task<bool> RegisterParticipant(Guid id, Guid participantId, CancellationToken ctoken)
        {
            Event? e = await _eventRepo.GetEventByID(id, ctoken) ?? throw new Exception("Üritust ei ole olemas");
            if (e.Date < DateTime.Now) return false;
            e.RegisterParticipant(participantId);

            return await _eventRepo.UpdateEvent(e, ctoken);
        }

        public async Task<bool> UnRegisterParticipant(Guid eventId, Guid participantId, CancellationToken ctoken)
        {
            Event? e = await _eventRepo.GetEventByID(eventId, ctoken) ?? throw new Exception("Üritust ei ole olemas");
            if (e.Date < DateTime.Now) return false;
            e.UnRegisterParticipant(participantId);

            return await _eventRepo.UpdateEvent(e, ctoken);
        }

        // -- helper funcs etc --

        private static int ParticipantCount(Event ev)
        {
            return ev.Participants.Sum(pa => pa.Participant.ParticipantCount);
        }


    }
}
