using Domain.RoomDomain.Entities;
using Domain.RoomDomain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.RoomData
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public RoomRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<int> Create(Room room)
        {
            _hotelDbContext.Rooms.Add(room);
            await _hotelDbContext.SaveChangesAsync();
            return room.Id;
        }

        public Task<Room> Get(int Id)
        {
            return _hotelDbContext.Rooms
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();
        }

        public Task<Room> GetAggregate(int Id)
        {
            return _hotelDbContext.Rooms
                .Include(x => x.Bookings)
                .Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
    }
}
