using Autofac.Extras.Moq;
using Business.People;
using Business.People.DTOs;
using Common.Constants;
using Data.Repositories.People;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryManagementTests.Business.People
{
    public class PersonQueriesTests
    {
        [Fact]
        public void GetIndexItems_ReturnsCorrectSizeList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                List<Person> people = GetSamplePeople();

                int count = people.Count();

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.GetFilteredAndPaginated(
                        0, count, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(people);

                var personQueries = mock.Create<PersonQueries>();

                //Act
                var actual = personQueries.GetIndexItems(new PeopleFilterDTO(), 1, count);

                //Assert
                Assert.Equal(count, actual.Count());
            }
        }

        [Fact]
        public void GetIndexItems_ReturnsCorrectList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                List<Person> people = GetSamplePeople();

                int count = people.Count();

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.GetFilteredAndPaginated(
                        0, count, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(people);

                var personQueries = mock.Create<PersonQueries>();

                //Act
                var actual = personQueries.GetIndexItems(new PeopleFilterDTO(), 1, count);

                //Assert
                for (int i = 0; i < count; i++)
                {
                    Assert.Equal(people[i].Name, actual[i].Name);
                };
            }
        }

        [Fact]
        public void GetDetailsDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                Person person = GetSamplePerson();

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(person);

                var personQueries = mock.Create<PersonQueries>();

                //Act
                var actual = personQueries.GetDetailsDTO(new Guid());

                //Assert
                Assert.Equal(person.PersonalCode, actual.PersonalCode);
            }
        }

        [Fact]
        public void GetEditDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                Person person = GetSamplePerson();

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(person);

                var personQueries = mock.Create<PersonQueries>();

                //Act
                var actual = personQueries.GetEditDTO(new Guid());

                //Assert
                Assert.Equal(person.PersonalCode, actual.PersonalCode);
            }
        }

        [Fact]
        public void PersonIsOnlyMember_PersonIsLibrarianAndMember_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IPersonRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(GetSamplePerson());

                var personQueries = mock.Create<PersonQueries>();

                //Act
                var actual = personQueries.PersonIsOnlyMember(new Guid());

                //Assert
                Assert.False(actual);
            }
        }

        private Person GetSamplePerson()
        {
            const string chars = "0123456789";
            string phone = new string(Enumerable.Repeat(chars, Consts.PhoneLength1)
                  .Select(s => s[new Random().Next(s.Length)]).ToArray());

            Person output = new Person("personal", "name", "email", phone, null, null, null, null, null);
            output.CreateMember("memberId", DateTime.Now);
            output.CreateLibrarian("librarianId");

            return output;
        }

        private List<Person> GetSamplePeople()
        {
            const string chars = "0123456789";
            string phone = new string(Enumerable.Repeat(chars, Consts.PhoneLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            List<Person> output = new List<Person>()
            {
                new Person("personalCode1", "name1", "email1", phone, null, null, null, null, null),
                new Person("personalCode2", "name2", "email2", phone, null, null, null, null, null),
                new Person("personalCode3", "name3", "email3", phone, null, null, null, null, null)
            };

            return output;
        }
    }
}
