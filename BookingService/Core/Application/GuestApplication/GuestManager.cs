using Application.GuestApplication.DTO;
using Application.GuestApplication.Ports;
using Application.GuestApplication.Requests;
using Application.GuestApplication.Responses;
using Domain.GuestDomain.Exceptions;
using Domain.GuestDomain.Ports;

namespace Application.GuestApplication
{
    public class GuestManager : IGuestManeger
    {
        private IGuestRepository _guestRepository;

        public GuestManager(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDTO.MapToEntity(request.Data);

                await guest.Save(_guestRepository);

                request.Data.Id = guest.Id;

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (InvalidPersonDocumentIdException e)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.INVALID_PERSON_ID,
                    Message = "The Id passed is not valid"
                };
            }
            catch (MissingRequiredInformation e)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing Required Information passed"
                };
            }
            catch (InvalidEmailException e)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.INVALID_EMAIL,
                    Message = "The given email is not valid"
                };
            }
            catch (Exception)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Message = "There was an error savind to DB"
                };
            }
        }

        public async Task<GuestResponse> GetGuest(int guestId)
        {
            var guest = await _guestRepository.Get(guestId);

            if (guest == null)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_NOT_FOUND,
                    Message = "No Guest record was found with the givin Id"
                };
            }

            return new GuestResponse
            {
                Data = GuestDTO.MapToDto(guest),
                Success = true,
            };
        }
    }
}
