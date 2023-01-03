using Application.BookingApplication.Dtos;
using Application.BookingApplication.Responses;
using Application.PaymentApplication.Dtos;
using Application.PaymentApplication.Responses;

namespace Application.BookingApplication.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreateBooking(BookingDto booking);
        Task<BookingResponse> GetBooking(int id);
        Task<PaymentResponse> PayForABooking(PaymentRequestDto paymentRequestDto);
    }
}
