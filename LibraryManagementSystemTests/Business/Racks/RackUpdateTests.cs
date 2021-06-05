using Autofac.Extras.Moq;
using Business.Racks;
using Business.Racks.DTOs;
using Data.Repositories.Racks;
using Domain.Models;
using Moq;
using System;
using Xunit;

namespace LibraryManagementTests.Business.Racks
{
    public class RackUpdateTests
    {
        [Fact]
        public void Create_Adds()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var rackMockDataAccess = mock.Mock<IRackRepository>();
                var rackUpdate = mock.Create<RackUpdate>();

                //Act
                rackUpdate.Create(new RackCreateDTO());

                //Assert
                rackMockDataAccess.Verify(x => x.Add(It.IsAny<Rack>()), Times.Once);
                rackMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Edit_Updates()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var rackMockDataAccess = mock.Mock<IRackRepository>();

                var rack = GetSampleRack();

                rackMockDataAccess
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(rack);

                var rackUpdate = mock.Create<RackUpdate>();

                //Act
                rackUpdate.Edit(new RackEditDTO());

                //Assert
                rackMockDataAccess.Verify(x => x.Update(rack), Times.Once);
                rackMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        [Fact]
        public void Delete_Removes()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var rackMockDataAccess = mock.Mock<IRackRepository>();

                var rack = GetSampleRack();

                rackMockDataAccess
                    .Setup(x => x.Get(It.IsAny<Guid>()))
                    .Returns(rack);

                var rackUpdate = mock.Create<RackUpdate>();

                //Act
                rackUpdate.Delete(new Guid());

                //Assert
                rackMockDataAccess.Verify(x => x.Remove(rack), Times.Once);
                rackMockDataAccess.Verify(x => x.SaveChanges(), Times.Once);
            }
        }

        private Rack GetSampleRack()
        {
            var output = new Rack(1, "id");

            return output;
        }
    }
}
