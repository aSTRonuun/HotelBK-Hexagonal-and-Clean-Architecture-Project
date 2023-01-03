using Application.PaymentApplication.Dtos;
using Application.PaymentApplication.Ports;

namespace Payment.Application
{
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        public IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders supportedPaymentProviders)
        {
            throw new NotImplementedException();
        }
    }
}
