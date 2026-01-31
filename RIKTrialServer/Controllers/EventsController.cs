using Microsoft.AspNetCore.Mvc;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Returns;
using RIKTrialSharedModels.Domains.Filters;
using RIKTrialServer.Services.Interfaces;

namespace RIKTrialServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController(IEventService eventService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService;

        [HttpGet]
        [Route("events")]
        public async Task<ActionResult<List<EventReturnDTO>>> GetEvents([FromQuery] EventFilters filters, CancellationToken ctoken)
        {
            return Ok(await _eventService.GetEvents(filters, ctoken));
        }

        [HttpGet]
        [Route("event")]
        public async Task<ActionResult<EventDetailedReturnDTO>> GetEvent([FromQuery] Guid id, CancellationToken ctoken)
        {
            try
            {
                return Ok(await _eventService.GetEvent(id, ctoken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("event")]
        public async Task<ActionResult<bool>> PostEvent([FromBody] EventCreationDTO data, CancellationToken ctoken)
        {
            if (data == null)
            {
                return BadRequest("Sisestage andmed");
            }

            return Ok(await _eventService.AddEvent(data, ctoken));
        }

        [HttpDelete]
        [Route("event")]
        public async Task<ActionResult<bool>> DeleteEvent([FromQuery] Guid id, CancellationToken ctoken)
        {
            try
            {
                return Ok(await _eventService.DeleteEvent(id, ctoken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<bool>> RegisterParticipant([FromQuery] RegistrationDTO data, CancellationToken ctoken)
        {
            try
            {
                return Ok(await _eventService.RegisterParticipant(data.EventId, data.ParticipantId, ctoken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("unregister")]
        public async Task<ActionResult<bool>> UnRegisterParticipant([FromQuery] RegistrationDTO data, CancellationToken ctoken)
        {
            try
            {
                return Ok(await _eventService.UnRegisterParticipant(data.EventId, data.ParticipantId, ctoken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
