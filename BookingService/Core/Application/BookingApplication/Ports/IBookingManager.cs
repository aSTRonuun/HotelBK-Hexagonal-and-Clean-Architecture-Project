using Application.BookingApplication.Dtos;

namespace Application.BookingApplication.Ports
{
    public interface IBookingManager
    {
        Task<BookingDto> CreateBooking(BookingDto booking);
        Task<BookingDto> GetBooking(int id);
    }
}
