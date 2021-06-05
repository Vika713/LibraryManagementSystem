using Autofac.Extras.Moq;
using Business.Cards;
using Business.Cards.DTOs;
using Data.Repositories.Cards;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace LibraryManagementTests.Business.Cards
{
    public class CardUpdateTests
    {
        [Fact]
        public void Create_AddsWithCorrectParameters()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var date = DateTime.Today;
                var cardMockDataAccess = mock.Mock<ICardRepository>();
                var cardUpdate = mock.Create<CardUpdate>();

                //Act
                cardUpdate.Create(new CardCreateDTO());

                //Assert
                cardMockDataAccess
                    .Verify(x => x.Add(It.Is<Card>(c => c.IsActive == true && c.IssuedAt == date)), Times.Once);
                cardMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void MakeAllCardsInactive_ActiveCard_UpdatesWithCorrectParameter()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var cardMockDataAccess = mock.Mock<ICardRepository>();

                cardMockDataAccess
                   .Setup(x => x.GetActiveByMember(It.IsAny<Guid>()))
                   .Returns(GetSampleCards());

                var cardUpdate = mock.Create<CardUpdate>();

                //Act
                cardUpdate.MakeAllCardsInactive(new Guid());

                //Assert
                cardMockDataAccess
                    .Verify(x => x.UpdateRange(It.Is<List<Card>>(c => c[0].IsActive == false)), Times.Once);
                cardMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        private List<Card> GetSampleCards()
        {
            var output = new List<Card>()
            {
                new Card(Guid.NewGuid(), "1", "barcode", DateTime.Today, true)
            };

            return output;
        }
    }
}
