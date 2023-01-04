using Application.RoomApplication.Responses;
using MediatR;

namespace Application.RoomApplication.Queries
{
    public class GetRoomQuery : IRequest<RoomResponse>
    {
        public int Id { get; set; }
    }
}
