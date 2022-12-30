using Application;
using Application.BookingApplication.Dtos;
using Application.BookingApplication.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;

        public BookingController(ILogger<BookingController> logger, IBookingManager bookingManager)
        {
            _logger = logger;
            _bookingManager = bookingManager;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(BookingDto booking)
        {
            var request = new CreateBookingRequest
            {
                Data = booking
            };

            var response = await _bookingManager.CreateBooking(request);

            if (response.Success) return Created("", response.Data);

            if (response.ErrorCode == ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.BOOKING_COULD_NOT_STORE) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.BOOKING_GUEST_NOT_FOUND) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.BOOKING_ROOM_NOT_FOUND) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.BOOKING_ROOM_NOT_AVAILABLE) return BadRequest(response);

            _logger.LogError("Response with unknow ErrorCode Returned", response);
            return BadRequest(500);
        }
    }
}
