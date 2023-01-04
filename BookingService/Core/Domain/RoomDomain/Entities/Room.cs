using Domain.BookingDomain.Entities;
using Domain.GuestDomain.Enuns;
using Domain.GuestDomain.ValueObjects;
using Domain.RoomDomain.Exceptions;
using Domain.RoomDomain.Ports;

namespace Domain.RoomDomain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintnance { get; set; }
        public Price Price { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public bool IsAvailable
        {
            get
            {
                if (InMaintnance || HasGuest) return false;
                return true;
            }
        }
        public bool HasGuest
        {
            get
            {
                var notAvailableStatuses = new List<Status>() { Status.Created, Status.Paid };

                return this.Bookings.Where(
                    b => b.Room.Id == this.Id &&
                    notAvailableStatuses.Contains(b.Status)).Count() > 0;
            } 
        }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(this.Name) || 
                Level <= 0)
            {
                throw new InvalidRoomDataException();
            }
            if (this.Price.Value < 10 )
            {
                throw new InvalidPriceValueException();
            }
        }

        public bool CanBeBooked()
        {
            try
            {
                this.ValidateState();
            }
            catch (Exception)
            {

                return false;
            }
            if (!this.IsAvailable) return false;
            return true;
        }

        public async Task Save(IRoomRepository roomRepository)
        {
            ValidateState();
            if (this.Id == 0)
            {
                this.Id = await roomRepository.Create(this);
            }
            else
            {
            }
        }
    }
}
