using Autofac.Extras.Moq;
using Business.Books;
using Business.Books.DTOs;
using Common.Constants;
using Data.Repositories.Books;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryManagementTests.Business.Books
{
    public class BookQueriesTests
    {
        [Fact]
        public void GetIndexItems_ReturnsCorrectSizeList()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                List<Book> books = GetSampleBooks();

                int count = books.Count();

                mock.Mock<IBookRepository>()
                    .Setup(x => x.GetFilteredAndPaginated(
                        0, count, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(books);

                BookQueries bookQueries = mock.Create<BookQueries>();

                //Act
                var actual = bookQueries.GetIndexItems(new BooksFilterDTO(), 1, count);

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

                int count = books.Count();

                mock.Mock<IBookRepository>()
                    .Setup(x => x.GetFilteredAndPaginated(
                        0, count, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(books);

                BookQueries bookQueries = mock.Create<BookQueries>();

                //Act
                var actual = bookQueries.GetIndexItems(new BooksFilterDTO(), 1, count);

                //Assert
                for (int i = 0; i < count; i++)
                {
                    Assert.Equal(books[i].ISBN, actual[i].ISBN);
                };
            }
        }

        [Fact]
        public void GetEditDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                Book book = GetSampleBook();

                mock.Mock<IBookRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(book);

                BookQueries bookQueries = mock.Create<BookQueries>();

                //Act
                var actual = bookQueries.GetEditDTO(new Guid());

                //Assert
                Assert.NotNull(actual);
                Assert.Equal(book.ISBN, actual.ISBN);
            }
        }

        [Fact]
        public void GetDeleteDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                Book book = GetSampleBook();

                mock.Mock<IBookRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(book);

                BookQueries bookQueries = mock.Create<BookQueries>();

                //Act
                var actual = bookQueries.GetDeleteDTO(new Guid());

                //Assert
                Assert.Equal(book.ISBN, actual.ISBN);
            }
        }

        private List<Book> GetSampleBooks()
        {
            const string chars = "0123456789";
            string ISBN1 = new string(Enumerable.Repeat(chars, Consts.ISBNLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());
            string ISBN2 = new string(Enumerable.Repeat(chars, Consts.ISBNLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());
            string ISBN3 = new string(Enumerable.Repeat(chars, Consts.ISBNLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            List<Book> output = new List<Book>()
            {
                new Book(ISBN1, "title1", "subject1", "publisher1", "language1", 1),
                new Book(ISBN2, "title2", "subject2", "publisher2", "language2", 2),
                new Book(ISBN3, "title3", "subject3", "publisher3", "language3", 3),
            };

            return output;
        }

        private Book GetSampleBook()
        {
            const string chars = "0123456789";
            string ISBN = new string(Enumerable.Repeat(chars, Consts.ISBNLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            Book output = new Book(ISBN, "title", "subject", "publisher", "language", 1);

            return output;
        }
    }
}
