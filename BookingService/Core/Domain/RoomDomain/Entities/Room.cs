using Domain.GuestDomain.ValueObjects;

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
    }
}
