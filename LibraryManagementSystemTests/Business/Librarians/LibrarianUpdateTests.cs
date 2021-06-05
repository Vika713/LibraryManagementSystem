using Autofac.Extras.Moq;
using Business.Librarians;
using Common.Enumeration;
using Data.Repositories.Librarians;
using Domain.Models;
using Moq;
using System;
using Xunit;

namespace LibraryManagementTests.Business.Librarians
{
    public class LibrarianUpdateTests
    {
        [Fact]
        public void ChangeStatus_StatusIsActive_UpdatesWithCorrectStatus()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var librarian = GetSampleLibrarian();
                var newStatus = LibrarianStatus.Closed;

                var librarianMockDataAccess = mock.Mock<ILibrarianRepository>();

                librarianMockDataAccess
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(librarian);

                var librarianUpdate = mock.Create<LibrarianUpdate>();

                //Act
                librarianUpdate.ChangeStatus(new Guid(), newStatus);

                //Assert
                librarianMockDataAccess
                    .Verify(x => x.Update(It.Is<Librarian>(l => l.Status == newStatus)), Times.Once);
                librarianMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        private Librarian GetSampleLibrarian()
        {
            var output = new Librarian("librarianId", Guid.NewGuid());

            return output;
        }
    }
}
