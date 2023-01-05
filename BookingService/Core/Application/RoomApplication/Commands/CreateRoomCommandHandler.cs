using Application.RoomApplication.DTO;
using Application.RoomApplication.Responses;
using Domain.RoomDomain.Exceptions;
using Domain.RoomDomain.Ports;
using MediatR;

namespace Application.RoomApplication.Commands
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomResponse>
    {
        private readonly IRoomRepository _roomRepository;
        public CreateRoomCommandHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        
        public async Task<RoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var room = RoomDTO.MapToEntity(request.RoomDTO);

                await room.Save(_roomRepository);

                request.RoomDTO.Id = room.Id;

                return new RoomResponse
                {
                    Success = true,
                    Data = request.RoomDTO
                };
            }
            catch (InvalidRoomDataException)
            {

                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION,
                    Message = "Missing required information passed"
                };
            }
            catch (InvalidPriceValueException)
            {

                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ROOM_INVALID_PRICE_VALUE,
                    Message = "Room price is invalid"
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
    }
}
