using Domain.BookingDomain.Entities;

namespace Domain.BookingDomain.Ports
{
    public interface IBookingRepository
    {
        Task<int> Create(Booking booking);
        Task<Booking> Get(int Id);
    }
}
