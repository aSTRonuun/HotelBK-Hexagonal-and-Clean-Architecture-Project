using Application.BookingApplication.Dtos;
using Application.BookingApplication.Responses;
using Domain.BookingDomain.Ports;
using MediatR;

namespace Application.BookingApplication.Queries
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingResponse>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingResponse> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.Get(request.Id);

            var bookinDto = BookingDto.MapToDto(booking);

            return new BookingResponse
            {
                Success = true,
                Data = bookinDto,
            };
        }
    }
}
