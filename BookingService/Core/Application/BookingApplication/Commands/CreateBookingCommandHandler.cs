using Application.BookingApplication.Dtos;
using Application.BookingApplication.Responses;
using Domain.BookingDomain.Exceptions;
using Domain.BookingDomain.Ports;
using Domain.GuestDomain.Ports;
using Domain.RoomDomain.Ports;
using MediatR;

namespace Application.BookingApplication.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _rooRepository;

        public CreateBookingCommandHandler(
            IBookingRepository bookingRepository,
            IGuestRepository guestRepository,
            IRoomRepository rooRepository)
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _rooRepository = rooRepository;
        }

        public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingDto = request.BookingDto;
                var booking = BookingDto.MapToEntity(bookingDto);
                booking.Guest = await _guestRepository.Get(bookingDto.GuestId);
                booking.Room = await _rooRepository.GetAggregate(bookingDto.RoomId);

                await booking.Save(_bookingRepository);

                bookingDto.Id = booking.Id;

                return new BookingResponse
                {
                    Success = true,
                    Data = bookingDto,
                };
            }
            catch (PlacedAtIsARequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_PLACEDAT_MISSING_REQUIRED_INFOMRATION,
                    Message = "PlacedAt is a required information"
                };
            }
            catch (StartIsARequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_START_MISSING_REQUIRED_INFOMRATION,
                    Message = "Start is a required information"
                };
            }
            catch (EndIsARequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_END_MISSING_REQUIRED_INFOMRATION,
                    Message = "End is a required information"
                };
            }
            catch (GuestIsARequiredInfomationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_GUEST_MISSING_REQUIRED_INFORMATION,
                    Message = "Guest is a required information"
                };
            }
            catch (RoomIsARequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_ROOM_MISSING_REQUIRED_INFORMATION,
                    Message = "Room is a required information"
                };
            }
            catch (RoomCannotBeBookedException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_ROOM_CANNOT_BE_BOOKED,
                    Message = "The Selected Room is not available"
                };
            }
            catch (Exception)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_COULD_NOT_STORE,
                    Message = "There was an error when saving to DB"
                };
            }
        }
    }
}
