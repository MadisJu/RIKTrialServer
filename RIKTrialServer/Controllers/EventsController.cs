using Microsoft.AspNetCore.Mvc;
using RIKTrialServer.Domains.DTOs.Creation;
using RIKTrialServer.Domains.DTOs.Returns;
using RIKTrialServer.Domains.Filters;
using RIKTrialServer.Services.Interfaces;

namespace RIKTrialServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController(IEventService eventService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService;

        [HttpPost]
        [Route("events")]
        public async Task<ActionResult<List<EventReturnDTO>>> GetEvents([FromBody] EventFilters filters)
        {
            return Ok(await _eventService.GetEvents(filters));
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
    }
}
