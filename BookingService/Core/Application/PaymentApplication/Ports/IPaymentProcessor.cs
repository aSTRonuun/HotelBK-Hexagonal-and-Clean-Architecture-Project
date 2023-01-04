using Application.PaymentApplication.Responses;

namespace Application.PaymentApplication.Ports
{
    public interface IPaymentProcessor
    {
        Task<PaymentResponse> CapturePayment(string paymentIntention);
    }
}
