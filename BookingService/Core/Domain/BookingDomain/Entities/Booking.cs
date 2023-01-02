using Domain.BookingDomain.Exceptions;
using Domain.BookingDomain.Ports;
using Domain.GuestDomain.Entities;
using Domain.GuestDomain.Enuns;
using Domain.RoomDomain.Entities;
using Action = Domain.GuestDomain.Enuns.Action;

namespace Domain.BookingDomain.Entities
{
    public class Booking
    {
        public Booking()
        {
            this.Status = Status.Created;
            this.PlacedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Room Room { get; set; }
        public Guest Guest { get; set; }
        public Status Status { get; set; }
        public void ChangeState(Action action)
        {
            this.Status = (this.Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paid, Action.Finish) => Status.Finished,
                (Status.Paid, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Created,
                _ => Status
            };
        }

        private void ValidateState()
        {
            if (this.PlacedAt == default(DateTime)) { throw new PlacedAtIsARequiredInformationException(); }
            if (this.Start == default(DateTime)) { throw new StartIsARequiredInformationException(); }
            if (this.End == default(DateTime)) { throw new EndIsARequiredInformationException(); }
            if (this.Room == null) { throw new RoomIsARequiredInformationException(); }
            if (this.Guest == null) { throw new GuestIsARequiredInfomationException(); }
        }

        public async Task Save(IBookingRepository bookingRepository)
        {
            this.ValidateState();
            
            this.Guest.IsValidate();
            if (!this.Room.CanBeBooked()) { throw new RoomCannotBeBookedException(); }

            if (this.Id == 0)
            {
                this.Id = await bookingRepository.Create(this);
            }
            else
            {

            }
        }
    }
}
