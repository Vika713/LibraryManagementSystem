using Autofac.Extras.Moq;
using Business.Cards;
using Data.Repositories.Cards;
using Data.Repositories.Members;
using Domain.Models;
using Moq;
using System;
using Xunit;

namespace LibraryManagementTests.Business.Cards
{
    public class CardQueriesTests
    {
        [Fact]
        public void GetCreateDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var id = Guid.NewGuid();

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(new Member());

                CardQueries cardQueries = mock.Create<CardQueries>();

                //Act
                var actual = cardQueries.GetCreateDTO(id);

                //Assert
                Assert.Equal(id, actual.MemberId);
            }
        }

        [Fact]
        public void GetBlockDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var id = Guid.NewGuid();

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(new Member());

                mock.Mock<ICardRepository>()
                    .Setup(x => x.GetActiveCard(It.IsAny<Guid>()))
                    .Returns(new Card());

                CardQueries cardQueries = mock.Create<CardQueries>();

                //Act
                var actual = cardQueries.GetBlockDTO(id);

                //Assert
                Assert.Equal(id, actual.MemberId);
            }
        }
    }
}
