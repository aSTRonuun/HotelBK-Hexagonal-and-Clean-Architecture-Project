﻿using Application.PaymentApplication.Dtos;
using Application.MercadoPago.Exceptions;
using Application.PaymentApplication.Responses;
using Application.PaymentApplication.Ports;

namespace Application.MercadoPago
{
    public class MercadoPagoAdapter : IPaymentProcessor
    {

        public Task<PaymentResponse> CapturePayment(string paymentIntention)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(paymentIntention))
                {
                    throw new InvalidPaymentIntentionException();
                }

                paymentIntention += "/success";

                var dto = new PaymentStateDto
                {
                    CreatedDate = DateTime.UtcNow,
                    Message = $"Successfully paid {paymentIntention}",
                    PaymentId = "123",
                    Status = Status.Success
                };

                var response = new PaymentResponse
                {
                    Data = dto,
                    Success = true,
                    Message = "Payment successfully processed"
                };

                return Task.FromResult(response);
            }
            catch (InvalidPaymentIntentionException)
            {
                var resp = new PaymentResponse()
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PAYMENT_INVALID_PAYMENT_INTENTION,
                    Message = "Payment intention is invalid",
                };
                return Task.FromResult(resp);
            }
        }
    }
}
