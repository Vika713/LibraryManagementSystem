using Autofac.Extras.Moq;
using Business.BookItems;
using Business.BookItems.DTOs;
using Common.Enumeration;
using Data.Repositories.BookItems;
using Data.Repositories.Racks;
using Domain.Models;
using Moq;
using System;
using Xunit;

namespace LibraryManagementTests.Business.BookItems
{
    public class BookItemUpdateTests
    {
        [Fact]
        public void Create_RackNotNull_AddsWithRackId()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var DTO = GetSampleCreateDTO();
                var rackId = Guid.NewGuid();

                mock.Mock<IRackRepository>()
                    .Setup(x => x.GetIdByNumberLocationIdentifier(DTO.RackNumber, DTO.LocationIdentifier))
                    .Returns(rackId);

                var bookItemUpdate = mock.Create<BookItemUpdate>();
                var mockDataAccess = mock.Mock<IBookItemRepository>();

                //Act
                bookItemUpdate.Create(DTO);

                //Assert
                mockDataAccess.Verify(x => x.Add(It.Is<BookItem>(bi => bi.RackId == rackId)), Times.Once);
                mockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Edit_RackNotNull_Hardcover_Updates()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var bookItemMockDataAccess = mock.Mock<IBookItemRepository>();
                var bookItem = GetSampleBookItem();
                bookItemMockDataAccess
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(bookItem);

                var DTO = GetSampleEditDTO();
                var rackId = Guid.NewGuid();
                mock.Mock<IRackRepository>()
                    .Setup(x => x.GetIdByNumberLocationIdentifier(DTO.RackNumber, DTO.LocationIdentifier))
                    .Returns(rackId);

                bookItem.SetRack(rackId);

                var bookItemUpdate = mock.Create<BookItemUpdate>();

                //Act
                bookItemUpdate.Edit(DTO);

                //Assert
                bookItemMockDataAccess.Verify(x => x.Update(bookItem), Times.Once);
                bookItemMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Delete_Removes()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var bookItemMockDataAccess = mock.Mock<IBookItemRepository>();
                var bookItem = GetSampleBookItem();
                bookItemMockDataAccess
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(bookItem);
                var bookItemUpdate = mock.Create<BookItemUpdate>();

                //Act
                bookItemUpdate.Delete(new Guid());

                //Assert
                bookItemMockDataAccess.Verify(x => x.Remove(bookItem), Times.Once);
                bookItemMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Reserve_StatusIsAvailable_ChangesStatusToReserved()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var bookItemMockDataAccess = mock.Mock<IBookItemRepository>();

                bookItemMockDataAccess
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(GetSampleBookItem());

                var bookItemUpdate = mock.Create<BookItemUpdate>();

                //Act
                bookItemUpdate.Reserve(new BookItemReserveDTO());

                //Assert
                bookItemMockDataAccess.Verify(x => x.Update(It.Is<BookItem>(bi => bi.Status == BookStatus.Reserved)), Times.Once);
                bookItemMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void CancelReservation_StatusIsReserved_ChangesStatusToAvailable()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var bookItemMockDataAccess = mock.Mock<IBookItemRepository>();

                var bookItem = GetSampleBookItem();
                bookItem.SetStatus(BookStatus.Reserved);

                bookItemMockDataAccess
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(bookItem);

                var bookItemUpdate = mock.Create<BookItemUpdate>();

                //Act
                bookItemUpdate.CancelReservation(new Guid());

                //Assert
                bookItemMockDataAccess
                    .Verify(x => x.Update(It.Is<BookItem>(bi => bi.Status == BookStatus.Available)), Times.Once);
                bookItemMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        private BookItemCreateDTO GetSampleCreateDTO()
        {
            BookItemCreateDTO output = new BookItemCreateDTO()
            {
                RackNumber = 1,
                LocationIdentifier = "id"
            };

            return output;
        }

        private BookItemEditDTO GetSampleEditDTO()
        {
            BookItemEditDTO output = new BookItemEditDTO()
            {
                Format = BookFormat.Hardcover,
                RackNumber = 1,
                LocationIdentifier = "id"
            };

            return output;
        }

        private BookItem GetSampleBookItem()
        {
            var output = new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null);

            return output;
        }
    }
}
