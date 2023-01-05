using Application.BookingApplication.Dtos;
using Application.BookingApplication.Ports;
using Application.BookingApplication.Responses;
using Application.PaymentApplication.Dtos;
using Application.PaymentApplication.Ports;
using Application.PaymentApplication.Responses;
using Domain.BookingDomain.Exceptions;
using Domain.BookingDomain.Ports;
using Domain.GuestDomain.Ports;
using Domain.RoomDomain.Ports;

namespace Application.BookingApplication
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _rooRepository;
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;

        public BookingManager(
            IBookingRepository bookingRepository, 
            IGuestRepository guestRepository, 
            IRoomRepository rooRepository, 
            IPaymentProcessorFactory paymentProcessorFactory)
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _rooRepository = rooRepository;
            _paymentProcessorFactory = paymentProcessorFactory;
        }

        public async Task<PaymentResponse> PayForABooking(PaymentRequestDto paymentRequestDto)
        {
            var paymentProcessor = _paymentProcessorFactory.GetPaymentProcessor(paymentRequestDto.SelectedPaymentProvider);

            var response = await paymentProcessor.CapturePayment(paymentRequestDto.PaymentIntention);

            if (response.Success)
            {
                return new PaymentResponse
                {
                    Success = true,
                    Data = response.Data,
                    Message = "Payment successfully processed"
                };
            }
            return response;
        }

        public Task<BookingResponse> GetBooking(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BookingResponse> CreateBooking(BookingDto booking)
        {
            throw new NotImplementedException();
        }
    }
}
