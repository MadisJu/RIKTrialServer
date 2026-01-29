using Microsoft.AspNetCore.Mvc;
using RIKTrialServer.Services.Interfaces;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Returns;

namespace RIKTrialServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController(IParticipantService participantService) : ControllerBase
    {
        private readonly IParticipantService _participantService = participantService;

        [HttpGet]
        [Route("participant")]
        public async Task<ActionResult<ParticipantReturnDTO>> GetParticipant([FromQuery] Guid userId, CancellationToken ctoken)
        {
            try
            {
                return Ok(await _participantService.GetParticipant(userId,ctoken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("participant")]
        public async Task<ActionResult<bool>> CreateParticipant([FromBody] ParticipantCreationDTO data, CancellationToken ctoken)
        {
            try
            {
                return Ok(await _participantService.CreateParticipant(data, ctoken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("participant")]
        public async Task<ActionResult<bool>> UpdateParticipant([FromBody] ParticipantCreationDTO data, [FromQuery] Guid id, CancellationToken ctoken)
        {
            try
            {
                return Ok(await _participantService.UpdateParticipant(data, id, ctoken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("participant")]
        public async Task<ActionResult<bool>> DeleteParticipant([FromQuery] Guid id, CancellationToken ctoken)
        {
            try
            {
                return Ok(_participantService.DeleteParticipant(id, ctoken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpGet]
        [Route("participants")]
        public async Task<ActionResult<List<ParticipantLightReturnDTO>>> GetEventParticipants([FromQuery] Guid eventId, CancellationToken ctoken)
        {
            return Ok(await _participantService.GetEventParticipants(eventId,ctoken));
        }

        [HttpGet]
        [Route("allparticipants")]
        public async Task<ActionResult<List<ParticipantReturnDTO>>> GetAllParticipants(CancellationToken ctoken)
        {
            return Ok(await _participantService.GetParticipants(ctoken));
        }

    }
}
