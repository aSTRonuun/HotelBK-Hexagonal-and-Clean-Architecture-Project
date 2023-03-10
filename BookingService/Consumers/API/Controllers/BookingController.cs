using Application;
using Application.BookingApplication.Commands;
using Application.BookingApplication.Dtos;
using Application.BookingApplication.Ports;
using Application.BookingApplication.Queries;
using Application.BookingApplication.Requests;
using Application.PaymentApplication.Dtos;
using Application.PaymentApplication.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;
        private readonly IMediator _mediator;

        public BookingController(
            ILogger<BookingController> logger, 
            IBookingManager bookingManager, 
            IMediator mediator)
        {
            _logger = logger;
            _bookingManager = bookingManager;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("{bookingId}/Pay")]
        public async Task<ActionResult<PaymentResponse>> Pay(
            PaymentRequestDto paymentRequestDto, int bookingId)
        {
            paymentRequestDto.BookingId = bookingId;
            var res = await _bookingManager.PayForABooking(paymentRequestDto);

            if (res.Success) return Ok(res.Data);

            return BadRequest(res);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(BookingDto booking)
        {
            var command = new CreateBookingCommand
            {
                BookingDto = booking
            };

            var response = await _mediator.Send(command);

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

        [HttpGet]
        public async Task<ActionResult<BookingDto>> Get(int Id)
        {
            var query = new GetBookingQuery
            {
                Id = Id
            };

            var res = await _mediator.Send(query);

            if (res.Success) return Created("", res.Data);

            _logger.LogError("Could not process the request", res);
            return BadRequest(500);
        }
    }
}
