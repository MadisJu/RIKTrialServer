using RIKTrialServer.Domains.Models;
using RIKTrialServer.Domains.DTOs.Creation;
using RIKTrialServer.Domains.DTOs.Returns;
using RIKTrialServer.Domains.Filters;
using RIKTrialServer.Repositories.Interfaces;
using RIKTrialServer.Services.Interfaces;
using RIKTrialServer.Transformers;
using System.Reflection.Metadata.Ecma335;

namespace RIKTrialServer.Services.Implementations
{
    public class EventService(IEventRepository repo) : IEventService
    {
        private readonly IEventRepository _eventRepo = repo;
        public async Task<bool> AddEvent(EventCreationDTO data, CancellationToken ctoken)
        {
            if (data.Date < DateTime.Now) throw new Exception("Saab ainult tuleviku lisada");
            if (data.AdditionalInfo.Length > 1000) throw new Exception("Liiga palju tähemärke lisainfol");

            Event ev = new()
            {
                Name = data.Name,
                Date = data.Date,
                Location = data.Location,
                AdditionalInfo = data.AdditionalInfo,
            };

            return await _eventRepo.AddEvent(ev, ctoken);
        }

        public async Task DeleteEvent(Guid eventId, CancellationToken ctoken)
        {
            Event? ev = await _eventRepo.GetEventByID(eventId);

            if (ev == null) throw new Exception("Proovite kustutada mitte eksisteerivat üritust");
            if (ev.Date < DateTime.Now) throw new Exception("Ei saa kustutada minevikust");

            bool suc = await _eventRepo.DeleteEvent(eventId, ctoken);

            if(!suc) throw new Exception("Kustutamine ei õnnestunud");
        }

        public async Task<Event?> GetEvent(Guid eventId)
        {
            return await _eventRepo.GetEventByID(eventId);
        }

        public async Task<List<EventReturnDTO>> GetEvents(EventFilters filters)
        {
            List<Event> events = await _eventRepo.GetEvents(filters);

            return events.Select(ev => EventMapper.EventsResponseMapper(ev, ParticipantCount(ev))).ToList();
        }

        public async Task RegisterParticipant(Guid e, Guid participant, CancellationToken ctoken)
        {
            throw new NotImplementedException();
        }

        // -- helper funcs etc --

        private int ParticipantCount(Event ev)
        {
            return ev.Participants.Sum(pa => pa.Participant.ParticipantCount);
        }
    }
}
