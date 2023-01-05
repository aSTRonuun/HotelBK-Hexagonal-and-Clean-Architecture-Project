using Application.RoomApplication.DTO;
using Application.RoomApplication.Responses;
using MediatR;

namespace Application.RoomApplication.Commands
{
    public class CreateRoomCommand : IRequest<RoomResponse>
    {
        public RoomDTO RoomDTO { get; set; }
    }
}
