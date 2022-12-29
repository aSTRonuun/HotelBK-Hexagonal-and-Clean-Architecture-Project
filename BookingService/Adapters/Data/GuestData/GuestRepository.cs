using Domain.GuestDomain.Entities;
using Domain.GuestDomain.Ports;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Guest?> Get(int id)
        {
            return await _hotelDbContext.Guests.Where(g => g.Id == id).FirstOrDefaultAsync();
        }
    }
}
