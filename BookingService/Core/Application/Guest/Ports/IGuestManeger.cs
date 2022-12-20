using Application.Guest.Requests;
using Application.Guest.Responses;

namespace Application.Guest.Ports
{
    public interface IGuestManeger
    {
        Task<GuestResponse> CreateGuest(CreateGuestRequest request);
    }
}
