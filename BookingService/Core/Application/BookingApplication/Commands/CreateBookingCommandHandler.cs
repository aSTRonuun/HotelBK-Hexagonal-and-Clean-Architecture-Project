using Application.BookingApplication.Ports;
using Application.BookingApplication.Responses;
using MediatR;

namespace Application.BookingApplication.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
    {

        private IBookingManager _bookingManager;

        public CreateBookingCommandHandler(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        public Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            return _bookingManager.CreateBooking(request.BookingDto);
        }
    }
}
