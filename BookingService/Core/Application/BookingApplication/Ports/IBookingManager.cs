using Application.BookingApplication.Dtos;
using Application.BookingApplication.Responses;

namespace Application.BookingApplication.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreateBooking(BookingDto booking);
        Task<BookingResponse> GetBooking(int id);
    }
}
