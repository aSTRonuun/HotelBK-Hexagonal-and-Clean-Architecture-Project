using Application;
using Application.Guest.DTO;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Application.GuestApplication.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManeger _guestManeger;

        public GuestController(ILogger<GuestController> logger, IGuestManeger guestManeger)
        {
            _logger = logger;
            _guestManeger = guestManeger;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest
            };
            
            var response = await _guestManeger.CreateGuest(request);

            if (response.Success) return Created("", response.Data);

            if (response.ErrorCode == ErrorCodes.NOT_FOUND) return NotFound(response);
            if (response.ErrorCode == ErrorCodes.INVALID_EMAIL) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.INVALID_PERSON_ID) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.MISSING_REQUIRED_INFORMATION) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.COULDNOT_STORE_DATA) return BadRequest(response);

            _logger.LogError("Response with unknow ErrorCode Returned", response);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<GuestDTO>> Get(int guestId)
        {
            var response = await _guestManeger.GetGuest(guestId);

            if (response.Success) return Created("", response.Data);

            return NotFound(response);
        }
    }
}
