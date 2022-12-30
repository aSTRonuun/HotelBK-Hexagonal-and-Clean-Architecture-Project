using Application.BookingApplication.Dtos;
using Application.BookingApplication.Ports;
using Domain.BookingDomain.Ports;

namespace Application.BookingApplication
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Task<BookingDto> CreateBooking(BookingDto bookingDto)
        {
            var booking = bookingDto.
        } 

        public Task<BookingDto> GetBooking(int id)
        {
            throw new NotImplementedException();
        }
    }
}
