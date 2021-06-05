using Autofac.Extras.Moq;
using Business.BookItems;
using Business.BookItems.DTOs;
using Common.Constants;
using Common.Enumeration;
using Data.Repositories.BookItems;
using Data.Repositories.Books;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryManagementTests.Business.BookItems
{
    public class BookItemQueriesTests
    {
        [Fact]
        public void GetIndexItems_ReturnsCorrectSizeList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                List<Book> books = GetSampleBooks();
                List<BookItem> bookItems = GetSampleBookItems(books[0]);

                int count = bookItems.Count();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetFilteredAndPaginated(
                        0, count, It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(bookItems);

                mock.Mock<IBookRepository>()
                    .Setup(x => x.Get(It.IsAny<IEnumerable<Guid>>()))
                    .Returns(books);

                BookItemQueries bookItemQueries = mock.Create<BookItemQueries>();

                //Act
                var actual = bookItemQueries.GetIndexItems(new BookItemsFilterDTO(), 1, count);

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
                List<Book> books = GetSampleBooks();
                List<BookItem> bookItems = GetSampleBookItems(books[0]);

                int count = bookItems.Count();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetFilteredAndPaginated(
                        0, count, It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(bookItems);

                mock.Mock<IBookRepository>()
                    .Setup(x => x.Get(It.IsAny<IEnumerable<Guid>>()))
                    .Returns(books);

                BookItemQueries bookItemQueries = mock.Create<BookItemQueries>();

                //Act
                var actual = bookItemQueries.GetIndexItems(new BookItemsFilterDTO(), 1, count);

                //Assert
                for (int i = 0; i < count; i++)
                {
                    Assert.Equal(bookItems[i].Barcode, actual[i].Barcode);
                };
            }
        }

        [Fact]
        public void GetIndexItemsByBook_ReturnsCorrectSizeList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                List<Book> books = GetSampleBooks();
                List<BookItem> bookItems = GetSampleBookItems(books[0]);

                int count = bookItems.Count();
                var id = Guid.NewGuid();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetPaginatedByBookId(
                        id, 0, count))
                    .Returns(bookItems);

                mock.Mock<IBookRepository>()
                    .Setup(x => x.Get(It.IsAny<IEnumerable<Guid>>()))
                    .Returns(books);

                BookItemQueries bookItemQueries = mock.Create<BookItemQueries>();

                //Act
                var actual = bookItemQueries.GetIndexItemsByBook(id, 1, count);

                //Assert
                Assert.Equal(count, actual.Count());
            }
        }

        [Fact]
        public void GetIndexItemsByBook_ReturnsCorrectList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                List<Book> books = GetSampleBooks();
                List<BookItem> bookItems = GetSampleBookItems(books[0]);

                int count = bookItems.Count();
                var id = Guid.NewGuid();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetPaginatedByBookId(
                        id, 0, count))
                    .Returns(bookItems);

                mock.Mock<IBookRepository>()
                    .Setup(x => x.Get(It.IsAny<IEnumerable<Guid>>()))
                    .Returns(books);

                BookItemQueries bookItemQueries = mock.Create<BookItemQueries>();

                //Act
                var actual = bookItemQueries.GetIndexItemsByBook(id, 1, count);

                //Assert
                for (int i = 0; i < count; i++)
                {
                    Assert.Equal(bookItems[i].Barcode, actual[i].Barcode);
                };
            }
        }

        [Fact]
        public void GetIndexItemsByRack_ReturnsCorrectSizeList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                List<Book> books = GetSampleBooks();
                List<BookItem> bookItems = GetSampleBookItems(books[0]);

                int count = bookItems.Count();
                var id = Guid.NewGuid();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetPaginatedByRackId(
                        id, 0, count))
                    .Returns(bookItems);

                mock.Mock<IBookRepository>()
                    .Setup(x => x.Get(It.IsAny<IEnumerable<Guid>>()))
                    .Returns(books);

                BookItemQueries bookItemQueries = mock.Create<BookItemQueries>();

                //Act
                var actual = bookItemQueries.GetIndexItemsByRack(id, 1, count);

                //Assert
                Assert.Equal(count, actual.Count());
            }
        }

        [Fact]
        public void GetIndexItemsByRack_ReturnsCorrectList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                List<Book> books = GetSampleBooks();
                List<BookItem> bookItems = GetSampleBookItems(books[0]);

                int count = bookItems.Count();
                var id = Guid.NewGuid();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetPaginatedByRackId(
                        id, 0, count))
                    .Returns(bookItems);

                mock.Mock<IBookRepository>()
                    .Setup(x => x.Get(It.IsAny<IEnumerable<Guid>>()))
                    .Returns(books);

                BookItemQueries bookItemQueries = mock.Create<BookItemQueries>();

                //Act
                var actual = bookItemQueries.GetIndexItemsByRack(id, 1, count);

                //Assert
                for (int i = 0; i < count; i++)
                {
                    Assert.Equal(bookItems[i].Barcode, actual[i].Barcode);
                };
            }
        }

        [Fact]
        public void GetDetailsDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                BookItem bookItem = GetSampleBookItem();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(bookItem);

                BookItemQueries bookItemQueries = mock.Create<BookItemQueries>();

                //Act
                var actual = bookItemQueries.GetDetailsDTO(new Guid());

                //Assert
                Assert.Equal(bookItem.Barcode, actual.Barcode);
            }
        }

        [Fact]
        public void GetEditDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                BookItem bookItem = GetSampleBookItem();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(bookItem);

                BookItemQueries bookItemQueries = mock.Create<BookItemQueries>();

                //Act
                var actual = bookItemQueries.GetEditDTO(new Guid());

                //Assert
                Assert.Equal(bookItem.Barcode, actual.Barcode);
            }
        }

        [Fact]
        public void GetDeleteDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                BookItem bookItem = GetSampleBookItem();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(bookItem);

                BookItemQueries bookItemQueries = mock.Create<BookItemQueries>();

                //Act
                var actual = bookItemQueries.GetDeleteDTO(new Guid());

                //Assert
                Assert.Equal(bookItem.Barcode, actual.Barcode);
            }
        }

        [Fact]
        public void GetReserveDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                BookItem bookItem = GetSampleBookItem();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(bookItem);

                BookItemQueries bookItemQueries = mock.Create<BookItemQueries>();

                //Act
                var actual = bookItemQueries.GetEditDTO(new Guid());

                //Assert
                Assert.Equal(bookItem.Barcode, actual.Barcode);
            }
        }

        private BookItem GetSampleBookItem()
        {
            var output = new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null);

            return output;
        }

        private List<BookItem> GetSampleBookItems(Book book)
        {
            var output = new List<BookItem>()
            {
                new BookItem(book.Id, "barcode1", null, null, BookFormat.Hardcover, null, null),
                new BookItem(book.Id, "barcode2", null, null, BookFormat.Hardcover, null, null),
                new BookItem(book.Id, "barcode3", null, null, BookFormat.Hardcover, null, null)
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
