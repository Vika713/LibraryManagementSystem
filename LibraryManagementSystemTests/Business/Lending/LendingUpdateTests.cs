using Autofac.Extras.Moq;
using Business.Lending;
using Business.Lending.DTOs;
using Common.Constants;
using Common.Enumeration;
using Data.Repositories.BookItems;
using Data.Repositories.Cards;
using Domain.Models;
using Moq;
using System;
using Xunit;

namespace LibraryManagementTests.Business.Lending
{
    public class LendingUpdateTests
    {
        [Fact]
        public void Lend_UpdatesBookItemWithCorrectMemberId()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var bookItemMockDataAccess = mock.Mock<IBookItemRepository>();

                var card = GetSampleCard();
                var bookItem = GetSampleBookItem();

                bookItemMockDataAccess
                    .Setup(x => x.GetByBarcode(It.IsAny<string>()))
                    .Returns(bookItem);

                bookItem.Lend(card.MemberId);

                mock.Mock<ICardRepository>()
                    .Setup(x => x.GetByBarcode(It.IsAny<string>()))
                    .Returns(card);

                var lendingUpdate = mock.Create<LendingUpdate>();

                //Act
                lendingUpdate.Lend(new ScanDTO());

                //Assert
                bookItemMockDataAccess.Verify(x => x.Update(bookItem), Times.Once);
                bookItemMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Return_UpdatesBookItemWithCorrectDueDate()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var bookItemMockDataAccess = mock.Mock<IBookItemRepository>();

                var bookItem = GetSampleBookItem();
                bookItem.Lend(Guid.NewGuid());

                bookItemMockDataAccess
                    .Setup(x => x.GetByBarcode(It.IsAny<string>()))
                    .Returns(bookItem);

                bookItem.Return();

                var lendingUpdate = mock.Create<LendingUpdate>();

                //Act
                lendingUpdate.Renew(string.Empty);

                //Assert
                bookItemMockDataAccess.Verify(x => x.Update(bookItem), Times.Once);
                bookItemMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Renew_UpdatesBookItemWithCorrectDueDate()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var bookItemMockDataAccess = mock.Mock<IBookItemRepository>();

                var card = GetSampleCard();
                var bookItem = GetSampleBookItem();

                bookItemMockDataAccess
                    .Setup(x => x.GetByBarcode(It.IsAny<string>()))
                    .Returns(bookItem);

                bookItem.SetDueDate(DateTime.Today.Date.AddDays(Consts.MaxLendingDays));

                var lendingUpdate = mock.Create<LendingUpdate>();

                //Act
                lendingUpdate.Renew(string.Empty);

                //Assert
                bookItemMockDataAccess.Verify(x => x.Update(bookItem), Times.Once);
                bookItemMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        private BookItem GetSampleBookItem()
        {
            var output = new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null);

            return output;
        }

        private Card GetSampleCard()
        {
            var output = new Card(Guid.NewGuid(), "number", "barcode", DateTime.Today, true);

            return output;
        }
    }
}
