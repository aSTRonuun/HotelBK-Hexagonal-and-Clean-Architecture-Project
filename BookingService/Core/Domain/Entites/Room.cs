using Domain.ValueObjects;

namespace Domain.Entites
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
                if (this.InMaintnance || this.HasGuest) return false;
                return true;
            }
        }
        public bool HasGuest
        {
            // Verify if exists open bookings to this room
            get { return true;  }
        }
    }
}
