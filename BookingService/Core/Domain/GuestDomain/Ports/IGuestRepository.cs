using Domain.GuestDomain.Entities;

namespace Domain.GuestDomain.Ports
{
    public interface IGuestRepository
    {
        Task<int> Create(Guest guest);
        Task<Guest> Get(int Id);
    }
}
