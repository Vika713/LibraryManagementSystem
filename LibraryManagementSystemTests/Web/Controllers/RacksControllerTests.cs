using Autofac.Extras.Moq;
using AutoMapper;
using Business.Racks;
using Business.Racks.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Controllers;
using Web.ViewModels.Racks;
using Xunit;

namespace LibraryManagementTests.Controllers
{
    public class RacksControllerTests
    {
        [Fact]
        public void Index_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var items = GetSampleItemsViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<List<RackItemViewModel>>(It.IsAny<List<RacksIndexItemDTO>>()))
                    .Returns(items);

                var controller = mock.Create<RacksController>();

                //Act
                var result = (ViewResult)controller.Index(new RacksIndexViewModel());
                var model = (RacksIndexViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(items[0].Id, model.Index[0].Id);
            }
        }

        [Fact]
        public void Index_ReturnsCorrectModelSize()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var items = GetSampleItemsViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<List<RackItemViewModel>>(It.IsAny<List<RacksIndexItemDTO>>()))
                    .Returns(items);

                var controller = mock.Create<RacksController>();

                //Act
                var result = (ViewResult)controller.Index(new RacksIndexViewModel());
                var model = (RacksIndexViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(items.Count(), model.Index.Count());
            }
        }

        [Fact]
        public void Create_ModelStateIsValid_Creates()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<RacksController>();
                var mockDataAccess = mock.Mock<IRackUpdateService>();

                //Act
                var result = controller.Create(new RackCreateViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Create(It.IsAny<RackCreateDTO>()), Times.Once);
            }
        }

        [Fact]
        public void Edit_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleEditViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<RackEditViewModel>(It.IsAny<RackEditDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<RacksController>();

                //Act
                var result = (ViewResult)controller.Edit(new Guid(), null, null);
                var model = (RackEditViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.RackId, model.RackId);
            }
        }

        [Fact]
        public void Edit_ModelStateIsValid_Edits()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<RacksController>();
                var mockDataAccess = mock.Mock<IRackUpdateService>();

                //Act
                var result = controller.Edit(new RackEditViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Edit(It.IsAny<RackEditDTO>()), Times.Once);
            }
        }

        [Fact]
        public void Delete_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleDeleteViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<RackDeleteViewModel>(It.IsAny<RackDeleteDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<RacksController>();

                //Act
                var result = (ViewResult)controller.Delete(new Guid());
                var model = (RackDeleteViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.Id, model.Id);
            }
        }

        [Fact]
        public void Delete_ModelStateIsValid_Deletes()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<RacksController>();
                var mockDataAccess = mock.Mock<IRackUpdateService>();

                //Act
                var result = controller.DeleteConfirmed(new Guid());

                //Assert
                mockDataAccess.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
            }
        }

        private List<RackItemViewModel> GetSampleItemsViewModel()
        {
            var output = new List<RackItemViewModel>()
            {
                new RackItemViewModel()
                {
                   Id = Guid.NewGuid()
                },
                new RackItemViewModel()
                {
                   Id = Guid.NewGuid()
                }
            };

            return output;
        }

        private RackEditViewModel GetSampleEditViewModel()
        {
            var output = new RackEditViewModel()
            {
                RackId = Guid.NewGuid()
            };

            return output;
        }

        private RackDeleteViewModel GetSampleDeleteViewModel()
        {
            var output = new RackDeleteViewModel()
            {
                Id = Guid.NewGuid()
            };

            return output;
        }
    }
}
