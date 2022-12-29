using Application;
using Application.GuestApplication;
using Application.GuestApplication.DTO;
using Application.GuestApplication.Requests;
using Domain.GuestDomain.Entities;
using Domain.GuestDomain.Enuns;
using Domain.GuestDomain.Ports;
using Moq;
using NUnit.Framework;

namespace ApplicationTests
{
    public class GuestManagerTests
    {
        GuestManager guestManager;

        [SetUp]
        public void Setup()
        {
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

            var expectedId = 222;

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));

            guestManager = new GuestManager(fakeRepo.Object);

            var response = await guestManager.CreateGuest(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Data.Id, expectedId);
        }
        
        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
        {
            var guestDto = new GuestDTO
            {
                Name = "Marta",
                Surname = "Gonzalez",
                Email = "test@test.com",
                IdNumber = docNumber,
                IdTypeCode = 1
            };

            var expectedId = 222;

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));

            guestManager = new GuestManager(fakeRepo.Object);

            var response = await guestManager.CreateGuest(request);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Success);
            Assert.AreEqual(response.ErrorCode, ErrorCodes.INVALID_PERSON_ID);
            Assert.AreEqual(response.Message, "The Id passed is not valid");
        }
        
        [TestCase("", "surnametest", "test@test.com")]
        [TestCase(null, "surnametest", "test@test.com")]
        [TestCase("nametest", "", "test@test.com")]
        [TestCase("nametest", null, "test@test.com")]
        [TestCase("nametest", "surnametest", "")]
        [TestCase("nametest", "surnametest", null)]
        public async Task Should_Return_MissingRequiredInformation_WhenDocsAreInvalid(string name, string surName, string email)
        {
            var guestDto = new GuestDTO
            {
                Name = name,
                Surname = surName,
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            var expectedId = 222;

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));

            guestManager = new GuestManager(fakeRepo.Object);

            var response = await guestManager.CreateGuest(request);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Success);
            Assert.AreEqual(response.ErrorCode, ErrorCodes.MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(response.Message, "Missing Required Information passed");
        }

        [TestCase("abc.com")]
        public async Task Should_Return_InvalidEmailException_WhenDocsAreInvalid(string email)
        {
            var guestDto = new GuestDTO
            {
                Name = "Marta",
                Surname = "Gonzalez",
                Email = email,
                IdNumber = "abcd",
                IdTypeCode = 1
            };

            var expectedId = 222;

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));

            guestManager = new GuestManager(fakeRepo.Object);

            var response = await guestManager.CreateGuest(request);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Success);
            Assert.AreEqual(response.ErrorCode, ErrorCodes.INVALID_EMAIL);
            Assert.AreEqual(response.Message, "The given email is not valid");
        }

        [Test]
        public async Task Should_Return_GuestNotFound_When_GuestDoesntExist()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Get(3333)).Returns(Task.FromResult<Guest?>(null));

            guestManager = new GuestManager(fakeRepo.Object);

            var response = await guestManager.GetGuest(3333);

            Assert.IsNotNull(response);
            Assert.False(response.Success);
            Assert.AreEqual(response.ErrorCode, ErrorCodes.GUEST_NOT_FOUND);
            Assert.AreEqual(response.Message, "No Guest record was found with the givin Id");
        }

        [Test]
        public async Task Should_Return_Guest_Success()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            var fakeGuest = new Guest
            {
                Id = 123,
                Name = "Test",
                SurName = "TestTest",
                DocumentId = new Domain.GuestDomain.ValueObjects.PersonId
                {
                    DocumentType = DocumentType.DriveLicence,
                    IdNumber = "1234"
                }
            };

            fakeRepo.Setup(x => x.Get(3333)).Returns(Task.FromResult<Guest?>(fakeGuest));

            guestManager = new GuestManager(fakeRepo.Object);

            var response = await guestManager.GetGuest(3333);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Data.Id, fakeGuest.Id);
            Assert.AreEqual(response.Data.Name, fakeGuest.Name);
        }
    }
}
