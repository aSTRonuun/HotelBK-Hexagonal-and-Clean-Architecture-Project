using Application.Room.Ports;
using Application.RoomApplication.DTO;
using Application.RoomApplication.Requests;
using Application.RoomApplication.Responses;
using Data.RoomData;
using Domain.RoomDomain.Exceptions;
using Domain.RoomDomain.Ports;

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
                var room = RoomDTO.MapToEntity(request.Data);

                await room.Save(_roomRepository);

                return new RoomResponse
                {
                    Success = true,
                    Data = request.Data
                };
            }
            catch (InvalidRoomDataException)
            {

                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION,
                    Message = "The data passed is not valid"                
                };
            }
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ROOM_COULD_NOT_STORE,
                    Message = "The room could not be stored"
                };
            }
        }

        public Task<RoomResponse> GetRoom(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
