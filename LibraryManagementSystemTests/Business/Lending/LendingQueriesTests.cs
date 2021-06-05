using Autofac.Extras.Moq;
using Business.Lending;
using Common.Constants;
using Common.Enumeration;
using Data.Repositories.BookItems;
using Domain.Models;
using Moq;
using System;
using Xunit;

namespace LibraryManagementTests.Business.Lending
{
    public class LendingQueriesTests
    {
        [Fact]
        public void GetFineDTO_Overdue_ReturnsCorrectFineAmount()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                DateTime currentDate = DateTime.Today.Date;
                DateTime dueDate = currentDate.AddDays(-1);
                TimeSpan overdue = currentDate - dueDate;

                var bookItem = GetSampleBookItem();
                bookItem.SetDueDate(dueDate);

                decimal fine = overdue.Days * Consts.FineRate;

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(It.IsAny<string>()))
                    .Returns(bookItem);

                var lendingQueries = mock.Create<LendingQueries>();

                //Act
                var actual = lendingQueries.GetFineDTO(string.Empty);

                //Assert
                Assert.Equal(fine, actual.Fine);
            }
        }

        [Fact]
        public void IsOverdue_Overdue_ReturnsTrue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                DateTime currentDate = DateTime.Today.Date;
                DateTime dueDate = currentDate.AddDays(-1);

                var bookItem = GetSampleBookItem();
                bookItem.SetDueDate(dueDate);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(It.IsAny<string>()))
                    .Returns(bookItem);

                var lendingQueries = mock.Create<LendingQueries>();

                //Act
                var actual = lendingQueries.IsOverdue(string.Empty);

                //Assert
                Assert.True(actual);
            }
        }

        private BookItem GetSampleBookItem()
        {
            var output = new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null);

            return output;
        }
    }
}
