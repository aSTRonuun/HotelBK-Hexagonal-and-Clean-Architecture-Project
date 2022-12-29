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
            // Verify if exists open bookings to this room
            get { return true; }
        }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(this.Name) || 
                Level <= 0)
            {
                throw new InvalidRoomDataException();
            }
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
