using Application;
using Application.Guest.DTO;
using Application.Guest.Requests;
using Domain.Entites;
using Domain.Ports;
using NUnit.Framework;

namespace ApplicationTests
{
    class FakeRepo : IGuestRepository
    {
        public Task<int> Create(Guest guest)
        {
            return Task.FromResult(111);
        }

        public Task<Guest> Get(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class GuestManagerTests
    {
        GuestManager guestManager;

        [SetUp]
        public void Setup()
        {
            var fakeRepo = new FakeRepo();
            guestManager = new GuestManager(fakeRepo);
        }

        [Test]
        public async Task HappyPath()
        {
            var guestDto = new GuestDTO
            {
                Name = "Marta",
                Surname = "Gonzalez",
                Email = "test@test.com",
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var response = await guestManager.CreateGuest(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
        }
    }
}
