using Application.PaymentApplication.Dtos;

namespace Application.PaymentApplication.Ports
{
    public interface IPaymentProcessorFactory
    {
        IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders supportedPaymentProviders);
    }
}
