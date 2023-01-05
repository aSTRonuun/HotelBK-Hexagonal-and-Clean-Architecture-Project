using Application;
using Application.BookingApplication.Commands;
using Application.BookingApplication.Dtos;
using Domain.BookingDomain.Entities;
using Domain.BookingDomain.Ports;
using Domain.GuestDomain.Entities;
using Domain.GuestDomain.Ports;
using Domain.RoomDomain.Entities;
using Domain.RoomDomain.Ports;
using Moq;
using NUnit.Framework;

namespace ApplicationTests
{
    public class CreateBookingCommandHandlerTests
    {
        private CreateBookingCommandHandler GetCommandMock(
            Mock<IRoomRepository> roomRepoistory = null,
            Mock<IGuestRepository> guestRepository = null,
            Mock<IBookingRepository> bookingRepository = null)
        {
            var _bookingRepository = bookingRepository ?? new Mock<IBookingRepository>();
            var _guestRepository = guestRepository ?? new Mock<IGuestRepository>();
            var _roomRepository = roomRepoistory ?? new Mock<IRoomRepository>();

            var coomandHandler = new CreateBookingCommandHandler(
                _bookingRepository.Object,
                _guestRepository.Object,
                _roomRepository.Object);

            return coomandHandler;
        }
        
        [Test]
        public async Task Should_Not_CreateBooking_If_RoomIsMissing()
        {
            var command = new CreateBookingCommand
            {
                BookingDto = new BookingDto
                {
                    GuestId = 1,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(2),
                }
            };

            var fakeGuest = new Guest
            {
                Id = command.BookingDto.GuestId,
                DocumentId = new Domain.GuestDomain.ValueObjects.PersonId
                {
                    DocumentType = Domain.GuestDomain.Enuns.DocumentType.Passport,
                    IdNumber = "abc1234"
                },
                Email = "a@a.com",
                Name = "Fake Guest",
                SurName = "Fake Surname Guest"
            };

            var guestRepository = new Mock<IGuestRepository>();
            guestRepository.Setup(x => x.Get(command.BookingDto.GuestId))
                .Returns(Task.FromResult(fakeGuest));

            var fakeBooking = new Booking
            {
                Id = 1
            };

            var bookingRepoMock = new Mock<IBookingRepository>();
            bookingRepoMock.Setup(x => x.Create(It.IsAny<Booking>()))
                .Returns(Task.FromResult(fakeBooking.Id));

            var handler = GetCommandMock(null, guestRepository, bookingRepoMock);

            var resp = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.IsTrue(resp.ErrorCode == ErrorCodes.BOOKING_ROOM_MISSING_REQUIRED_INFORMATION);
            Assert.IsTrue(resp.Message == "Room is a required information");
        }

        [Test]
        public async Task Should_Not_CreateBooking_If_StartDateIsMissing()
        {
            var command = new CreateBookingCommand
            {
                BookingDto = new BookingDto
                {
                    GuestId = 1,
                    RoomId = 1,
                    End = DateTime.Now.AddDays(2),
                }
            };

            var fakeGuest = new Guest
            {
                Id = command.BookingDto.GuestId,
                DocumentId = new Domain.GuestDomain.ValueObjects.PersonId
                {
                    DocumentType = Domain.GuestDomain.Enuns.DocumentType.Passport,
                    IdNumber = "abc1234"
                },
                Email = "a@a.com",
                Name = "Fake Guest",
                SurName = "Fake Surname Guest"
            };

            var guestRepository = new Mock<IGuestRepository>();
            guestRepository.Setup(x => x.Get(command.BookingDto.GuestId))
                .Returns(Task.FromResult(fakeGuest));

            var fakeRoom = new Room
            {
                Id = command.BookingDto.RoomId,
                InMaintnance = false,
                Price = new Domain.GuestDomain.ValueObjects.Price
                {
                    Currency = Domain.GuestDomain.Enuns.AcceptedCurrencies.Dollar,
                    Value = 100
                },
                Name = "Fake Room 01",
                Level = 1,
            };

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(x => x.GetAggregate(command.BookingDto.RoomId))
                .Returns(Task.FromResult(fakeRoom));

            var handler = GetCommandMock(roomRepository, guestRepository);

            var resp = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.IsTrue(resp.ErrorCode == ErrorCodes.BOOKING_START_MISSING_REQUIRED_INFOMRATION);
            Assert.IsTrue(resp.Message == "Start is a required information");
        }

        [Test]
        public async Task Should_CreateBoking()
        {
            var command = new CreateBookingCommand
            {
                BookingDto = new BookingDto
                {
                    GuestId = 1,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(2),
                }
            };

            var fakeGuest = new Guest
            {
                Id = command.BookingDto.GuestId,
                DocumentId = new Domain.GuestDomain.ValueObjects.PersonId
                {
                    DocumentType = Domain.GuestDomain.Enuns.DocumentType.Passport,
                    IdNumber = "abc1234"
                },
                Email = "a@a.com",
                Name = "Fake Guest",
                SurName = "Fake Surname Guest"
            };

            var guestRepository = new Mock<IGuestRepository>();
            guestRepository.Setup(x => x.Get(command.BookingDto.GuestId))
                .Returns(Task.FromResult(fakeGuest));

            var fakeRoom = new Room
            {
                Id = command.BookingDto.RoomId,
                InMaintnance = false,
                Price = new Domain.GuestDomain.ValueObjects.Price
                {
                    Currency = Domain.GuestDomain.Enuns.AcceptedCurrencies.Dollar,
                    Value = 100
                },
                Name = "Fake Room 01",
                Level = 1,
                Bookings = new List<Booking>()
            };

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(x => x.GetAggregate(command.BookingDto.RoomId))
                .Returns(Task.FromResult(fakeRoom));

            var fakeBooking = new Booking
            {
                Id = 1
            };

            var bookingRepoMock = new Mock<IBookingRepository>();
            bookingRepoMock.Setup(x => x.Create(It.IsAny<Booking>()))
                .Returns(Task.FromResult(fakeBooking.Id));

            var handler = GetCommandMock(roomRepository, guestRepository, bookingRepoMock);

            var resp = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.IsTrue(resp.Success);
            Assert.NotNull(resp.Data);
            Assert.AreEqual(resp.Data.Id, fakeBooking.Id);
        }
    }
}
