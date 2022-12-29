using Domain.GuestDomain.Enuns;

namespace Domain.GuestDomain.ValueObjects
{
    public class Price
    {
        public decimal Value { get; set; }
        public AcceptedCurrencies Currency { get; set; }
    }
}
