using Application;
using Application.MercadoPago;
using Application.PaymentApplication.Dtos;
using NUnit.Framework;
using Payment.Application;

namespace Payment.UnitTests
{
    public class PaymentProcessorFactoryTests
    {
        [Test]
        public void SouldReturn_NotImplementedPaymentProvider_WhenAskingForStripeProvider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.Stripe);

            Assert.AreEqual(provider.GetType(), typeof(NotImplementedPaymentProvider));
        }

        [Test]
        public void SouldReturn_MercadoPagoAdapter_WhenAskingForMercadoPagoProvider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

            Assert.AreEqual(provider.GetType(), typeof(MercadoPagoAdapter));
        }

        [Test]
        public async Task ShouldReturnFalse_WhenCapturingPaymentFor_NotImplementedPaymentProvider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.PayPal);

            var result = await provider.CapturePayment("https://mvprovider.com/nsdnjsn");

            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.ErrorCode, ErrorCodes.PAYMENT_PROVIDER_NOT_IMPLMENTED);
            Assert.AreEqual(result.Message, "The selected payment provider is not available at the moment");
        }
    }
}
