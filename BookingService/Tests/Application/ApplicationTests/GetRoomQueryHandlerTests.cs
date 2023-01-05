using Application;
using Application.RoomApplication.Queries;
using Domain.RoomDomain.Entities;
using Domain.RoomDomain.Ports;
using Moq;
using NUnit.Framework;

namespace ApplicationTests
{
    public class GetRoomQueryHandlerTests
    {
        [Test]
        public async Task Should_Return_Room()
        {
            var query = new GetRoomQuery { Id = 1 };

            var repoMock = new Mock<IRoomRepository>();
            var fakeRoom = new Room()
            {
                Id = 1,
            };

            repoMock.Setup(x => x.Get(query.Id)).Returns(Task.FromResult(fakeRoom));

            var handler = new GetRoomQueryHandler(repoMock.Object);

            var response = await handler.Handle(query, CancellationToken.None);

            Assert.IsTrue(response.Success);
            Assert.NotNull(response.Data);
        }
        
        [Test]
        public async Task Should_Return_ProperError_Message_WhenRoom_NotFound()
        {
            var query = new GetRoomQuery { Id = 1 };

            var repoMock = new Mock<IRoomRepository>();

            var handler = new GetRoomQueryHandler(repoMock.Object);

            var response = await handler.Handle(query, CancellationToken.None);

            Assert.False(response.Success);
            Assert.AreEqual(response.ErrorCode, ErrorCodes.ROOM_NOT_FOUND);
            Assert.AreEqual(response.Message, "Could not find a Room with the given Id");
        }
        
    }
}
