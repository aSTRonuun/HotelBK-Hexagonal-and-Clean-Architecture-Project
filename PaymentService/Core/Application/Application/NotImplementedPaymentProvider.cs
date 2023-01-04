using Application;
using Application.PaymentApplication.Ports;
using Application.PaymentApplication.Responses;

namespace Payment.Application
{
    public class NotImplementedPaymentProvider : IPaymentProcessor
    {
        public Task<PaymentResponse> CapturePayment(string paymentIntention)
        {
            var paymenteResponse = new PaymentResponse()
            {
                Success = false,
                ErrorCode = ErrorCodes.PAYMENT_PROVIDER_NOT_IMPLMENTED,
                Message = "The selected payment provider is not available at the moment",
            };

            return Task.FromResult(paymenteResponse);
        }
    }
}
