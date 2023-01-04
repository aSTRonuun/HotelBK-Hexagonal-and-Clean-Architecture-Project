using Application;
using Application.MercadoPago;
using Application.PaymentApplication.Dtos;
using NUnit.Framework;
using Payment.Application;

namespace Payment.UnitTests
{
    public class MercadoPagoTests
    {
        [Test]
        public void ShouldReturn_MercadoPagoAdapter_Provider()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

            Assert.AreEqual(provider.GetType(), typeof(MercadoPagoAdapter));
        }

        [Test]
        public async Task Should_FailWhenPaymentIntentionStringIsInvalid()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

            var result = await provider.CapturePayment("");

            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.ErrorCode, ErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION);
            Assert.AreEqual(result.Message, "Payment intention is invalid");
        }

        [Test]
        public async Task Should_ReturnSuccessfullyWhenPaymentIntentionIsValid()
        {
            var factory = new PaymentProcessorFactory();

            var provider = factory.GetPaymentProcessor(SupportedPaymentProviders.MercadoPago);

            var result = await provider.CapturePayment("https://www.mercadopago.com.br/sdjsdbs");

            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Message, "Payment successefully processed");
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.CreatedDate);
            Assert.NotNull(result.Data.PaymentId);
        }


    }
}
