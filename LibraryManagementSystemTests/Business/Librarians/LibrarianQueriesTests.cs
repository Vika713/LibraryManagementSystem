using Autofac.Extras.Moq;
using Business.Librarians;
using Data.Repositories.Librarians;
using Domain.Models;
using Moq;
using System;
using Xunit;

namespace LibraryManagementTests.Business.Librarians
{
    public class LibrarianQueriesTests
    {
        [Fact]
        public void GetDetailsDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var librarian = GetSampleLibrarian();

                mock.Mock<ILibrarianRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(librarian);

                LibrarianQueries librarianQueries = mock.Create<LibrarianQueries>();

                //Act
                var actual = librarianQueries.GetDetailsDTO(new Guid());

                //Assert
                Assert.Equal(librarian.Code, actual.Code);
            }
        }

        [Fact]
        public void GetStatusChangeDTO_ReturnsCorrectDTO()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var librarian = GetSampleLibrarian();

                mock.Mock<ILibrarianRepository>()
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(librarian);

                LibrarianQueries librarianQueries = mock.Create<LibrarianQueries>();

                //Act
                var actual = librarianQueries.GetStatusChangeDTO(new Guid());

                //Assert
                Assert.Equal(librarian.Code, actual.Code);
            }
        }

        private Librarian GetSampleLibrarian()
        {
            var output = new Librarian("librarianId", Guid.NewGuid());

            return output;
        }
    }
}
