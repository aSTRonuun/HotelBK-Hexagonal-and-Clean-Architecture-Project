using Application.GuestApplication.Requests;
using Application.GuestApplication.Responses;

namespace Application.GuestApplication.Ports
{
    public interface IGuestManeger
    {
        Task<GuestResponse> CreateGuest(CreateGuestRequest request);

        Task<GuestResponse> GetGuest(int guestId);
    }
}
