using Autofac.Extras.Moq;
using Business.Members;
using Common.Constants;
using Common.Enumeration;
using Data.Repositories.BookItems;
using Data.Repositories.Books;
using Data.Repositories.Members;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryManagementTests.Business.Members
{
    public class MemberQueriesTests
    {
        [Fact]
        public void GetDetailsDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var member = GetSampleMember();

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(member);

                MemberQueries memberQueries = mock.Create<MemberQueries>();

                //Act
                var actual = memberQueries.GetDetailsDTO(new Guid());

                //Assert
                Assert.Equal(member.Code, actual.Code);
            }
        }

        [Fact]
        public void GetStatusChangeDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var member = GetSampleMember();

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(member);

                MemberQueries memberQueries = mock.Create<MemberQueries>();

                //Act
                var actual = memberQueries.GetStatusChangeDTO(new Guid());

                //Assert
                Assert.Equal(member.Code, actual.Code);
            }
        }

        [Fact]
        public void GetBorrowedBookItemsDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var books = GetSampleBooks();
                var bookItems = GetSampleBookItems(books[0].Id);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBorrowedMemberId(It.IsAny<Guid>()))
                    .Returns(bookItems);

                mock.Mock<IBookRepository>()
                    .Setup(x => x.Get(It.IsAny<IEnumerable<Guid>>()))
                    .Returns(books);

                MemberQueries memberQueries = mock.Create<MemberQueries>();

                //Act
                var actual = memberQueries.GetBorrowedBookItemsDTO(new Guid());

                //Assert
                Assert.Equal(books[0].ISBN, actual.BorrowedBookItems[0].ISBN);
            }
        }

        [Fact]
        public void GetReservedBookItemsDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var books = GetSampleBooks();
                var bookItems = GetSampleBookItems(books[0].Id);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByReservedMemberId(It.IsAny<Guid>()))
                    .Returns(bookItems);

                mock.Mock<IBookRepository>()
                    .Setup(x => x.Get(It.IsAny<IEnumerable<Guid>>()))
                    .Returns(books);

                MemberQueries memberQueries = mock.Create<MemberQueries>();

                //Act
                var actual = memberQueries.GetReservedBookItemsDTO(new Guid());

                //Assert
                Assert.Equal(books[0].ISBN, actual.ReservedBookItems[0].ISBN);
            }
        }

        private Member GetSampleMember()
        {
            var output = new Member("memberId", DateTime.Today, Guid.NewGuid());

            return output;
        }

        private List<BookItem> GetSampleBookItems(Guid bookId)
        {
            var output = new List<BookItem>()
            {
                new BookItem(bookId, "barcode", null, null, BookFormat.Hardcover, null, null)
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
