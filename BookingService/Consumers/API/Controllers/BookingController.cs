using Application;
using Application.BookingApplication.Dtos;
using Application.BookingApplication.Ports;
using Application.BookingApplication.Requests;
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

            var response = await _bookingManager.CreateBooking(request.Data);

            if (response.Success) return Created("", response.Data);

            if (response.ErrorCode == ErrorCodes.BOOKING_PLACEDAT_MISSING_REQUIRED_INFOMRATION) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.BOOKING_START_MISSING_REQUIRED_INFOMRATION) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.BOOKING_END_MISSING_REQUIRED_INFOMRATION) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.BOOKING_GUEST_MISSING_REQUIRED_INFORMATION) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.BOOKING_END_MISSING_REQUIRED_INFOMRATION) return BadRequest(response);
            if (response.ErrorCode == ErrorCodes.BOOKING_ROOM_CANNOT_BE_BOOKED) return BadRequest(response);

            _logger.LogError("Response with unknow ErrorCode Returned", response);
            return BadRequest(500);
        }
    }
}
