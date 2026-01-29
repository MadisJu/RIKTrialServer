using RIKTrialServer.Domains.Models;
using RIKTrialServer.Repositories.Interfaces;
using RIKTrialServer.Services.Interfaces;
using RIKTrialServer.Transformers;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Returns;
using RIKTrialSharedModels.Domains.Filters;

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

        public async Task<bool> DeleteEvent(Guid eventId, CancellationToken ctoken)
        {
            Event? ev = await _eventRepo.GetEventByID(eventId)
                ?? throw new Exception("Proovite kustutada mitte eksisteerivat üritust");

            if (ev.Date < DateTime.Now) throw new Exception("Ei saa kustutada minevikust");

            return await _eventRepo.RemoveEvent(ev, ctoken);
        }

        public async Task<Event?> GetEvent(Guid eventId)
        {
            return await _eventRepo.GetEventByID(eventId);
        }

        public async Task<List<EventReturnDTO>> GetEvents(EventFilters filters)
        {
            List<Event> events = await _eventRepo.GetEvents(filters);

            return events.Select(ev => EventMapper.MapToEventsResponse(ev, ParticipantCount(ev))).ToList();
        }

        public  async Task<bool> RegisterParticipant(Guid id, Guid participantId, CancellationToken ctoken)
        {
            Event? e = await _eventRepo.GetEventByID(id) ?? throw new Exception("Üritust ei ole olemas");
            if (e.Date < DateTime.Now) throw new Exception("Ei saa registreerida mineviku");
            e.RegisterParticipant(participantId);

            return await _eventRepo.UpdateEvent(e, ctoken);
        }

        public async Task<bool> UnRegisterParticipant(Guid eventId, Guid participantId, CancellationToken ctoken)
        {
            Event? e = await _eventRepo.GetEventByID(eventId) ?? throw new Exception("Üritust ei ole olemas");
            if (e.Date < DateTime.Now) throw new Exception("Ei saa muuta mineviku");
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
