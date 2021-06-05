using Autofac.Extras.Moq;
using Business.Identity;
using Common.Constants;
using Common.Enumeration;
using Data.Repositories.People;
using Domain.Models;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace LibraryManagementTests.Business.Identity
{
    public class IdentityServiceTests
    {
        [Fact]
        public void GetRoles_PersonIsMember_ReturnsMemberRole()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var person = GetSamplePersonMember();

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.GetByEmail(It.IsAny<string>()))
                    .Returns(person);

                var identityService = mock.Create<IdentityService>();

                //Act
                var actual = identityService.GetRoles(string.Empty);

                //Assert
                Assert.Contains(Roles.Member, actual);
            }
        }

        [Fact]
        public void GetRoles_PersonIsLibrarian_ReturnsLibrarianRole()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var person = GetSamplePersonLibrarian();

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.GetByEmail(It.IsAny<string>()))
                    .Returns(person);

                var identityService = mock.Create<IdentityService>();

                //Act
                var actual = identityService.GetRoles(string.Empty);

                //Assert
                Assert.Contains(Roles.Librarian, actual);
            }
        }

        [Fact]
        public void ChangePersonEmail_NewEmail_UpdatesWithChangedEmail()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var person = GetSamplePerson();
                var email = "new email";
                var id = Guid.NewGuid();

                var personMockDataAccess = mock.Mock<IPersonRepository>();

                personMockDataAccess
                    .Setup(x => x.Get(id))
                    .Returns(person);

                var identityService = mock.Create<IdentityService>();

                //Act
                identityService.ChangePersonEmail(id, email);

                //Assert
                personMockDataAccess.Verify(x => x.Update(It.Is<Person>(p => p.Email == email)), Times.Once);
                personMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        private Person GetSamplePerson()
        {
            const string chars = "0123456789";
            string phone = new string(Enumerable.Repeat(chars, Consts.PhoneLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            Person output = new Person("personal", "name", "email", phone, null, null, null, null, null);

            return output;
        }

        private Person GetSamplePersonMember()
        {
            Person output = GetSamplePerson();
            output.CreateMember("memberId", DateTime.Now);

            return output;
        }

        private Person GetSamplePersonLibrarian()
        {
            Person output = GetSamplePerson();
            output.CreateLibrarian("librarianId");

            return output;
        }
    }
}
