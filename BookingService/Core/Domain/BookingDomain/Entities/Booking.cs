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
            Status = Status.Created;
        }
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Room Room { get; set; }
        public Guest Guest { get; set; }
        private Status Status { get; set; }
        public Status CurrentStatus { get { return Status; } }

        public void ChangeState(Action action)
        {
            Status = (Status, action) switch
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
            if (this.PlacedAt == null) { throw new PlacedAtIsARequiredInformationException(); }
            if (this.Start == null) { }
            if (this.End == null) { }
            if (this.Room == null) { }
            if (this.Guest == null) { }
        }

        public async Task Save(IBookingRepository bookingRepository)
        {
            this.ValidateState();

            if(this.Id == 0)
            {
                this.Id = await bookingRepository.Create(this);
            }
            else
            {

            }
        }
    }
}
