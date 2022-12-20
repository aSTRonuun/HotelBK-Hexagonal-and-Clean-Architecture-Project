using Domain.Entites;
using Domain.Ports;

namespace Data.GuestData
{
    public class GuestRepository : IGuestRepository
    {
        private HotelDbContext _hotelDbContext;

        public GuestRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> Create(Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest.Id;
        }
        public Task<Guest> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
