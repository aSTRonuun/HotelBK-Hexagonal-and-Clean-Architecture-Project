using Application;
using Application.Room.Ports;
using Application.RoomApplication.DTO;
using Application.RoomApplication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _roomManager;

        public RoomController(ILogger<RoomController> logger, IRoomManager roomManager)
        {
            _logger = logger;
            _roomManager = roomManager;
        }
        
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> Post(RoomDTO room)
        {
            var request = new CreateRoomRequest
            {
                Data = room
            };

            var response = await _roomManager.CreateRoom(request);

            if (response.Success) return Created("", response.Data);

            if (response.ErrorCode == ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.ROOM_COULD_NOT_STORE) return BadRequest(response);

            _logger.LogError("Response with unknow ErrorCode Returned", response);
            return BadRequest(500);
        }
    }
}
