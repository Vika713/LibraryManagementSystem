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
    public class BookUpdateTests
    {
        [Fact]
        public void Create_BookWithAuthors_AddsWithAuthors()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var DTO = GetSampleBookCreateDTO();
                var expected = DTO.AuthorsNames.Count;
                var bookMockDataAccess = mock.Mock<IBookRepository>();
                var bookUpdate = mock.Create<BookUpdate>();

                //Act
                bookUpdate.Create(DTO);

                //Assert
                bookMockDataAccess
                    .Verify(x => x.Add(It.Is<Book>(b => b.BookAuthors.Count == expected)), Times.Once);
                bookMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Edit_AuthorRemoved_UpdatesWithoutAuthor()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var bookMockDataAccess = mock.Mock<IBookRepository>();
                var authors = GetSampleAuthors();
                var book = GetSampleBook(authors);
                var DTO = GetSampleBookEditDTO(book.Id);

                bookMockDataAccess
                    .Setup(x => x.GetAuthorsByBookId(DTO.BookId))
                    .Returns(authors);

                bookMockDataAccess
                    .Setup(x => x.GetAuthorsByNames(It.IsAny<IEnumerable<string>>()))
                    .Returns(authors.Where(a => DTO.AuthorsNames.Any(an => an.Name == a.Name)));

                bookMockDataAccess
                    .Setup(x => x.Get(DTO.BookId))
                    .Returns(book);

                var bookUpdate = mock.Create<BookUpdate>();

                //Act
                bookUpdate.Edit(DTO);

                //Assert
                bookMockDataAccess
                    .Verify(x => x.Update(It.Is<Book>(b => b.BookAuthors.Count == DTO.AuthorsNames.Count)), Times.Once);
                bookMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Edit_AuthorRemoved_RemovesAuthors()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var DTO = GetSampleBookEditDTO(Guid.NewGuid());
                var authors = GetSampleAuthors();
                var bookMockDataAccess = mock.Mock<IBookRepository>();

                bookMockDataAccess
                    .Setup(x => x.GetAuthorsByBookId(DTO.BookId))
                    .Returns(GetSampleAuthors);

                bookMockDataAccess
                    .Setup(x => x.Get(DTO.BookId))
                    .Returns(GetSampleBook());

                bookMockDataAccess
                    .Setup(x => x.GetAuthors(It.IsAny<IEnumerable<Guid>>()))
                    .Returns(authors);

                var bookUpdate = mock.Create<BookUpdate>();

                //Act
                bookUpdate.Edit(DTO);

                //Assert
                bookMockDataAccess.Verify(x => x.RemoveAuthors(authors), Times.Once);
            }
        }

        [Fact]
        public void Delete_BookWithoutBookItems_Removes()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var bookMockDataAccess = mock.Mock<IBookRepository>();
                var book = GetSampleBook(GetSampleAuthors());

                bookMockDataAccess
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(book);

                var bookUpdate = mock.Create<BookUpdate>();

                //Act
                bookUpdate.Delete(new Guid());

                //Assert
                bookMockDataAccess.Verify(x => x.Remove(book), Times.Once);
                bookMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Delete_BookWithoutBookItems_RemovesAuthors()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var bookMockDataAccess = mock.Mock<IBookRepository>();
                var authors = GetSampleAuthors();
                var book = GetSampleBook(authors);

                bookMockDataAccess
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(book);

                bookMockDataAccess
                    .Setup(x => x.GetAuthors(It.IsAny<IEnumerable<Guid>>()))
                    .Returns(authors);

                var bookUpdate = mock.Create<BookUpdate>();

                //Act
                bookUpdate.Delete(new Guid());

                //Assert
                bookMockDataAccess.Verify(x => x.RemoveAuthors(authors), Times.Once);
            }
        }

        private Book GetSampleBook(List<Author> authors)
        {
            Book output = GetSampleBook();
            output.AddAuthors(authors);

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

        private List<Author> GetSampleAuthors()
        {
            List<Author> output = new List<Author>()
            {
                new Author("author1"),
                new Author("author2")
            };

            return output;
        }

        private BookCreateDTO GetSampleBookCreateDTO()
        {
            const string chars = "0123456789";
            string ISBN = new string(Enumerable.Repeat(chars, Consts.ISBNLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            BookCreateDTO output = new BookCreateDTO()
            {
                ISBN = ISBN,
                AuthorsNames = new List<AuthorNameDTO>()
                {
                    new AuthorNameDTO() { Name = "author1" },
                    new AuthorNameDTO() { Name = "author2" }
                }
            };

            return output;
        }

        private BookEditDTO GetSampleBookEditDTO(Guid bookId)
        {
            const string chars = "0123456789";
            string ISBN = new string(Enumerable.Repeat(chars, Consts.ISBNLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            BookEditDTO output = new BookEditDTO()
            {
                BookId = bookId,
                ISBN = ISBN,
                AuthorsNames = new List<AuthorNameDTO>()
                {
                    new AuthorNameDTO() { Name = "author1" }
                }
            };

            return output;
        }
    }
}
