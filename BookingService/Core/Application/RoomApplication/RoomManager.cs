using Application.Room.Ports;

namespace Application.Room
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomRepository _roomRepository;
        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<RoomResponse> CreateRoom(CreateRoomRequest request)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<RoomResponse> GetRoom(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
