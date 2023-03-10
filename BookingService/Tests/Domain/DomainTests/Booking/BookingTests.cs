using Domain.BookingDomain.Entities;
using Domain.GuestDomain.Enuns;
using NUnit.Framework;
using Action = Domain.GuestDomain.Enuns.Action;

namespace DomainTests.Bookings
{
    public class BookingTests
    {
        [Test]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();
            Assert.AreEqual(booking.Status, Status.Created);
        }
        
        [Test]
        public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);

            Assert.AreEqual(booking.Status, Status.Paid);
        }
        
        [Test]
        public void ShouldSetStatusToCanceledWhenCancelingForABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Cancel);

            Assert.AreEqual(booking.Status, Status.Canceled);
        }
        
        [Test]
        public void ShouldSetStatusToFinishedWhenFinishingForAPaidBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);

            Assert.AreEqual(booking.Status, Status.Finished);
        }
        
        [Test]
        public void ShouldSetStatusToRefoundWhenRefoundingForAPaidBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.Status, Status.Refounded);
        }
        
        [Test]
        public void ShouldSetStatusToCreatedWhenReopeningForACanceledBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Cancel);
            booking.ChangeState(Action.Reopen);

            Assert.AreEqual(booking.Status, Status.Created);
        }
        
        [Test]
        public void ShouldNotChangeStatusWhenRefoundingAFinishedBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.Status, Status.Finished);
        }
    }
}
