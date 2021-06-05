using Autofac.Extras.Moq;
using Business.Racks;
using Business.Racks.DTOs;
using Common.Constants;
using Common.Enumeration;
using Data.Repositories.BookItems;
using Data.Repositories.Books;
using Data.Repositories.Racks;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryManagementTests.Business.Racks
{
    public class RackQueriesTests
    {
        [Fact]
        public void GetIndexItems_ReturnsCorrectSizeList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                List<Rack> racks = GetSampleRacks();
                int count = racks.Count;

                mock.Mock<IRackRepository>()
                    .Setup(x => x.GetFilteredAndPaginated(
                        0, count, It.IsAny<int?>(), It.IsAny<string>()))
                    .Returns(racks);

                var rackQueries = mock.Create<RackQueries>();

                //Act
                var actual = rackQueries.GetIndexItems(new RacksFilterDTO(), 1, count);

                //Assert
                Assert.Equal(count, actual.Count);
            }
        }

        [Fact]
        public void GetIndexItems_ReturnsCorrectList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                List<Rack> racks = GetSampleRacks();
                int count = racks.Count;

                mock.Mock<IRackRepository>()
                    .Setup(x => x.GetFilteredAndPaginated(
                        0, count, It.IsAny<int?>(), It.IsAny<string>()))
                    .Returns(racks);

                var rackQueries = mock.Create<RackQueries>();

                //Act
                var actual = rackQueries.GetIndexItems(new RacksFilterDTO(), 1, count);

                //Assert
                for (int i = 0; i < count; i++)
                {
                    Assert.Equal(racks[i].RackNumber, actual[i].RackNumber);
                }
            }
        }

        [Fact]
        public void GetEditDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var rack = GetSampleRack();
                var id = Guid.NewGuid();

                mock.Mock<IRackRepository>()
                    .Setup(x => x.Get(id))
                    .Returns(rack);

                var rackQueries = mock.Create<RackQueries>();

                //Act
                var actual = rackQueries.GetEditDTO(id, 1, 0);

                //Assert
                Assert.Equal(rack.RackNumber, actual.RackNumber);
            }
        }

        [Fact]
        public void GetEditDTO_ReturnsCorrectBookItemsOnRackListSize()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var rack = GetSampleRack();
                var id = Guid.NewGuid();
                var books = GetSampleBooks();
                var bookItems = GetSampleBookItems(books[0].Id);
                var count = bookItems.Count;

                mock.Mock<IRackRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(rack);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetPaginatedByRackId(It.IsAny<Guid>(), 0, count))
                    .Returns(bookItems);

                mock.Mock<IBookRepository>()
                   .Setup(x => x.Get(It.IsAny<IEnumerable<Guid>>()))
                   .Returns(books);

                var rackQueries = mock.Create<RackQueries>();

                //Act
                var actual = rackQueries.GetEditDTO(id, 1, count);

                //Assert
                Assert.Equal(count, actual.BookItemsOnRack.Count);
            }
        }

        [Fact]
        public void GetEditDTO_ReturnsCorrectBookItemsOnRackList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var rack = GetSampleRack();
                var id = Guid.NewGuid();
                var books = GetSampleBooks();
                var bookItems = GetSampleBookItems(books[0].Id);
                var count = bookItems.Count;

                mock.Mock<IRackRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(rack);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetPaginatedByRackId(It.IsAny<Guid>(), 0, count))
                    .Returns(bookItems);

                mock.Mock<IBookRepository>()
                   .Setup(x => x.Get(It.IsAny<IEnumerable<Guid>>()))
                   .Returns(books);

                var rackQueries = mock.Create<RackQueries>();

                //Act
                var actual = rackQueries.GetEditDTO(id, 1, count);

                //Assert
                for (int i = 0; i < count; i++)
                {
                    Assert.Equal(bookItems[i].Barcode, actual.BookItemsOnRack[i].Barcode);
                };
            }
        }

        [Fact]
        public void GetDeleteDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var rack = GetSampleRack();

                mock.Mock<IRackRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(rack);

                var rackQueries = mock.Create<RackQueries>();

                //Act
                var actual = rackQueries.GetDeleteDTO(new Guid());

                //Assert
                Assert.Equal(rack.RackNumber, actual.RackNumber);
            }
        }

        private Rack GetSampleRack()
        {
            var output = new Rack(1, "id");

            return output;
        }

        private List<Rack> GetSampleRacks()
        {
            var output = new List<Rack>()
            {
                new Rack(1, "id1"),
                new Rack(2, "id2"),
                new Rack(3, "id3")
            };

            return output;
        }

        private List<BookItem> GetSampleBookItems(Guid bookId)
        {
            var output = new List<BookItem>()
            {
                new BookItem(bookId, "barcode1", null, null, BookFormat.Hardcover, null, null),
                new BookItem(bookId, "barcode2", null, null, BookFormat.Hardcover, null, null),
                new BookItem(bookId, "barcode3", null, null, BookFormat.Hardcover, null, null)
            };

            return output;
        }

        private List<Book> GetSampleBooks()
        {
            const string chars = "0123456789";
            string ISBN = new string(Enumerable.Repeat(chars, Consts.ISBNLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            var output = new List<Book>()
            {
                new Book(ISBN, "title", "subject", "publisher", "language", 1)
            };

            return output;
        }
    }
}
