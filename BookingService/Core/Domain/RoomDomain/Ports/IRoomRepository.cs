using Domain.RoomDomain.Entities;

namespace Domain.RoomDomain.Ports
{
    interface IRoomRepository
    {
        Task<int> Create(Room room);
        Task<Room?> Get(int id);
    }
}
