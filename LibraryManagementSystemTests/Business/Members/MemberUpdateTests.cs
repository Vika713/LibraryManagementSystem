using Autofac.Extras.Moq;
using Business.Cards;
using Business.Members;
using Common.Enumeration;
using Data.Repositories.BookItems;
using Data.Repositories.Members;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryManagementTests.Business.Members
{
    public class MemberUpdateTests
    {
        [Fact]
        public void ChangeStatus_StatusIsActive_UpdatesWithCorrectStatus()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var newStatus = MemberStatus.Closed;

                var memberMockDataAccess = mock.Mock<IMemberRepository>();

                memberMockDataAccess
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(new Member());

                var memberUpdate = mock.Create<MemberUpdate>();

                //Act
                memberUpdate.ChangeStatus(new Guid(), newStatus);

                //Assert
                memberMockDataAccess
                    .Verify(x => x.Update(It.Is<Member>(l => l.Status == newStatus)), Times.Once);
                memberMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void ChangeStatus_NewStatusIsClosed_MakesAllCardsInactive()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var newStatus = MemberStatus.Closed;
                var id = Guid.NewGuid();

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(new Member());

                var cardMockDataAccess = mock.Mock<ICardUpdateService>();

                var memberUpdate = mock.Create<MemberUpdate>();

                //Act
                memberUpdate.ChangeStatus(id, newStatus);

                //Assert
                cardMockDataAccess.Verify(x => x.MakeAllCardsInactive(id), Times.Once);
            }
        }

        [Fact]
        public void ChangeStatus_NewStatusIsClosed_RemovesReservations()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var newStatus = MemberStatus.Closed;
                var bookItems = GetSampleBookItems();
                var id = Guid.NewGuid();

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(new Member());

                var bookItemMockDataAccess = mock.Mock<IBookItemRepository>();

                bookItemMockDataAccess
                    .Setup(x => x.GetByReservedMemberId(It.IsAny<Guid>()))
                    .Returns(bookItems);

                var memberUpdate = mock.Create<MemberUpdate>();

                //Act
                memberUpdate.ChangeStatus(new Guid(), newStatus);

                //Assert
                bookItemMockDataAccess.Verify(x => x.UpdateRange(
                    It.Is<List<BookItem>>
                        (i => i.All(bi => bi.ReservedMemberId == null && bi.Status == BookStatus.Available))),
                    Times.Once);
            }
        }

        private List<BookItem> GetSampleBookItems()
        {
            var output = new List<BookItem>()
            {
                new BookItem(Guid.Empty, "barcode1", null, null, BookFormat.Hardcover, null, null),
                new BookItem(Guid.Empty, "barcode2", null, null, BookFormat.Hardcover, null, null)
            };

            foreach (var book in output)
            {
                book.Reserve(Guid.NewGuid());
            };

            return output;
        }
    }
}
