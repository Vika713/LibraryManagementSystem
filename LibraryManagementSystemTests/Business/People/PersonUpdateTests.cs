using Autofac.Extras.Moq;
using Business.People;
using Business.People.DTOs;
using Common.Constants;
using Data.Repositories.Librarians;
using Data.Repositories.Members;
using Data.Repositories.People;
using Domain.Models;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace LibraryManagementTests.Business.People
{
    public class PersonUpdateTests
    {
        [Fact]
        public void Create_Adds()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var personMockDataAccess = mock.Mock<IPersonRepository>();
                var personUpdate = mock.Create<PersonUpdate>();

                //Act
                personUpdate.Create(GetSampleCreateDTO());

                //Assert
                personMockDataAccess.Verify(x => x.Add(It.IsAny<Person>()), Times.Once);
                personMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Edit_Updates()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var personMockDataAccess = mock.Mock<IPersonRepository>();

                var person = GetSamplePerson();

                personMockDataAccess
                    .Setup(x => x.Get(new Guid()))
                    .Returns(person);

                var personUpdate = mock.Create<PersonUpdate>();

                //Act
                personUpdate.Edit(GetSampleEditDTO());

                //Assert
                personMockDataAccess.Verify(x => x.Update(person), Times.Once);
                personMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void AddAsMember_Adds()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var memberMockDataAccess = mock.Mock<IMemberRepository>();
                var personUpdate = mock.Create<PersonUpdate>();

                //Act
                personUpdate.AddAsMember(new Guid());

                //Assert
                memberMockDataAccess.Verify(x => x.Add(It.IsAny<Member>()), Times.Once);
                memberMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void AddAsLibrarian_Adds()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var librarianMockDataAccess = mock.Mock<ILibrarianRepository>();
                var personUpdate = mock.Create<PersonUpdate>();

                //Act
                personUpdate.AddAsLibrarian(new Guid());

                //Assert
                librarianMockDataAccess.Verify(x => x.Add(It.IsAny<Librarian>()), Times.Once);
                librarianMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        private Person GetSamplePerson()
        {
            const string chars = "0123456789";
            string phone = new string(Enumerable.Repeat(chars, Consts.PhoneLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            Person output = new Person("code", "name", "email", phone, null, null, null, null, null);

            return output;
        }

        private PersonCreateDTO GetSampleCreateDTO()
        {
            const string chars = "0123456789";
            string phone = new string(Enumerable.Repeat(chars, Consts.PhoneLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            PersonCreateDTO output = new PersonCreateDTO()
            {
                Phone = phone
            };

            return output;
        }

        private PersonEditDTO GetSampleEditDTO()
        {
            const string chars = "0123456789";
            string phone = new string(Enumerable.Repeat(chars, Consts.PhoneLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            PersonEditDTO output = new PersonEditDTO()
            {
                Phone = phone
            };

            return output;
        }
    }
}
