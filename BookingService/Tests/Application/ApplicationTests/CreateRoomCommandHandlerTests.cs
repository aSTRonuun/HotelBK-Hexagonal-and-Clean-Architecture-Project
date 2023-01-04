using Application;
using Application.BookingApplication.Commands;
using Application.RoomApplication.Commands;
using Domain.GuestDomain.Enuns;
using Domain.RoomDomain.Entities;
using Domain.RoomDomain.Ports;
using Moq;
using NUnit.Framework;

namespace ApplicationTests
{
    public class CreateRoomCommandHandlerTests
    {
        [Test]
        public async Task Should_Not_CreateRoom_If_Name_IsNot_Provided()
        {
            var command = new CreateRoomCommand()
            {
                RoomDTO = new Application.RoomApplication.DTO.RoomDTO
                {
                    InMaintenance = false,
                    Level = 1,
                    Currency = AcceptedCurrencies.Dollar,
                    Name = "",
                    Price = 100
                }
            };

            var repoMock = new Mock<IRoomRepository>();
            repoMock.Setup(x => x.Create(It.IsAny<Room>()))
                .Returns(Task.FromResult(1));
            
            var handler = new CreateRoomCommandHandler(repoMock.Object);

            var res = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(res);
            Assert.IsFalse(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing required information passed");
        }
        
        [Test]
        public async Task Should_Not_CreateRoom_If_Price_Is_Invalid()
        {
            var command = new CreateRoomCommand()
            {
                RoomDTO = new Application.RoomApplication.DTO.RoomDTO
                {
                    InMaintenance = false,
                    Level = 1,
                    Currency = AcceptedCurrencies.Dollar,
                    Name = "Room Test",
                    Price = 5
                }
            };

            var repoMock = new Mock<IRoomRepository>();
            repoMock.Setup(x => x.Create(It.IsAny<Room>()))
                .Returns(Task.FromResult(1));
            
            var handler = new CreateRoomCommandHandler(repoMock.Object);

            var res = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(res);
            Assert.IsFalse(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.ROOM_INVALID_PRICE_VALUE);
            Assert.AreEqual(res.Message, "Room price is invalid");
        }
        
        [Test]
        public async Task Should_CreateRoom()
        {
            var command = new CreateRoomCommand()
            {
                RoomDTO = new Application.RoomApplication.DTO.RoomDTO
                {
                    InMaintenance = false,
                    Level = 1,
                    Currency = AcceptedCurrencies.Dollar,
                    Name = "Room Test",
                    Price = 20
                }
            };

            var repoMock = new Mock<IRoomRepository>();
            repoMock.Setup(x => x.Create(It.IsAny<Room>()))
                .Returns(Task.FromResult(1));
            
            var handler = new CreateRoomCommandHandler(repoMock.Object);

            var res = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(res);
            Assert.IsTrue(res.Success);
            Assert.NotNull(res.Data);
            Assert.AreEqual(res.Data.Id, 1);
        }
    }
}
