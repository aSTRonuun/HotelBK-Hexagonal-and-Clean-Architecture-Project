using Application;
using Application.BookingApplication.Commands;
using Application.Room.Ports;
using Application.RoomApplication.Commands;
using Application.RoomApplication.DTO;
using Application.RoomApplication.Queries;
using Application.RoomApplication.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _roomManager;
        private readonly IMediator _mediator;

        public RoomController(
            ILogger<RoomController> logger, 
            IRoomManager roomManager,
            IMediator mediator)
        {
            _logger = logger;
            _roomManager = roomManager;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDTO>> Post(RoomDTO room)
        {
            var command = new CreateRoomCommand
            {
                RoomDTO = room
            };

            var response = await _mediator.Send(command);

            if (response.Success) return Created("", response.Data);

            if (response.ErrorCode == ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.ROOM_COULD_NOT_STORE) return BadRequest(response);

            _logger.LogError("Response with unknow ErrorCode Returned", response);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<RoomDTO>> Get(int roomId)
        {
            var query = new GetRoomQuery
            {
                Id = roomId
            };

            var response = await _mediator.Send(query);

            if (response.Success) return Ok(response.Data);

            return NotFound(response);
        }
    }
}
