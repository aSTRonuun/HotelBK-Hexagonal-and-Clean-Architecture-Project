using Domain.BookingDomain.Entities;
using Domain.GuestDomain.Entities;
using Domain.GuestDomain.Enuns;
using Domain.RoomDomain.Entities;

namespace Application.BookingApplication.Dtos
{
    public class BookingDto
    {
        public BookingDto()
        {
            this.PlacedAt = DateTime.UtcNow;
        }
        
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        private Status Status { get; set; }

        public static Booking MapToEntity(BookingDto bookingDto)
        {
            return new Booking
            {
                Id = bookingDto.Id,
                Start = bookingDto.Start,
                End = bookingDto.End,
                Room = new Domain.RoomDomain.Entities.Room { Id = bookingDto.RoomId },
                Guest = new Guest { Id = bookingDto.GuestId }
            };
        }

        public static BookingDto MapToDto(Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                Start = booking.Start,
                End = booking.End,
                RoomId = booking.Room.Id,
                GuestId = booking.Guest.Id,
                Status = booking.Status,
                PlacedAt = booking.PlacedAt
            };
        }
    }
}
