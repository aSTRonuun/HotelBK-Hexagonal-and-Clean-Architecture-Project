using Application.BookingApplication;
using Application.PaymentApplication.Dtos;
using Application.PaymentApplication.Ports;
using Application.PaymentApplication.Responses;
using Domain.BookingDomain.Ports;
using Domain.GuestDomain.Ports;
using Domain.RoomDomain.Ports;
using Moq;
using NUnit.Framework;

namespace ApplicationTests
{
    public class BookingManagerTests
    {
        [Test]
        public async Task Should_PayForABooking()
        {
            var dto = new PaymentRequestDto
            {
                SelectedPaymentProvider = SupportedPaymentProviders.MercadoPago,
                PaymentIntention = "www.//www.mercadopago.com/najdnsd",
                SelectedPaymentMethod = SupportedPaymentMethods.CreditCard
            };

            var bookingRepository = new Mock<IBookingRepository>();
            var roomRepository = new Mock<IRoomRepository>();
            var guestRepository = new Mock<IGuestRepository>();
            var paymentProcessorFactory = new Mock<IPaymentProcessorFactory>();
            var paymentProcessor = new Mock<IPaymentProcessor>();

            var responseDto = new PaymentStateDto
            {
                CreatedDate = DateTime.UtcNow,
                Message = $"Successfully paid {dto.PaymentIntention}",
                PaymentId = "123",
                Status = Status.Success
            };

            var response = new PaymentResponse
            {
                Data = responseDto,
                Success = true,
                Message = "Payment successfully processed"
            };

            paymentProcessor
                .Setup(x => x.CapturePayment(dto.PaymentIntention))
                .Returns(Task.FromResult(response));

            paymentProcessorFactory
                .Setup(x => x.GetPaymentProcessor(dto.SelectedPaymentProvider))
                .Returns(paymentProcessor.Object);

            var bookingManager = new BookingManager(
                bookingRepository.Object,
                guestRepository.Object,
                roomRepository.Object,
                paymentProcessorFactory.Object);

            var res = await bookingManager.PayForABooking(dto);


            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.AreEqual(res.Message, "Payment successfully processed");
        }
    }
}
