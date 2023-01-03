using Application.PaymentApplication.Dtos;

namespace Application.PaymentApplication.Responses
{
    public class PaymentResponse : Response
    {
        public PaymentStateDto Data { get; set; }
    }
}
