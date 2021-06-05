using Autofac.Extras.Moq;
using AutoMapper;
using Business.Authorization;
using Business.BookItems;
using Business.BookItems.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Web.Controllers;
using Web.ViewModels.BookItems;
using Xunit;

namespace LibraryManagementTests.Controllers
{
    public class BookItemsControllerTests
    {
        [Fact]
        public void Index_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var items = GetSampleItemsViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<List<BookItemItemViewModel>>(It.IsAny<List<BookItemsIndexItemDTO>>()))
                    .Returns(items);

                var controller = mock.Create<BookItemsController>();

                //Act
                var result = (ViewResult)controller.Index(new BookItemsIndexViewModel());
                var model = (BookItemsIndexViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(items[0].BookItemId, model.Index[0].BookItemId);
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
                    .Setup(m => m.Map<List<BookItemItemViewModel>>(It.IsAny<List<BookItemsIndexItemDTO>>()))
                    .Returns(items);

                var controller = mock.Create<BookItemsController>();

                //Act
                var result = (ViewResult)controller.Index(new BookItemsIndexViewModel());
                var model = (BookItemsIndexViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(items.Count(), model.Index.Count());
            }
        }

        [Fact]
        public void IndexByBook_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var items = GetSampleItemsViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<List<BookItemItemViewModel>>(It.IsAny<List<BookItemsIndexItemDTO>>()))
                    .Returns(items);

                var controller = mock.Create<BookItemsController>();

                //Act
                var result = (ViewResult)controller.IndexByBook(new Guid(), null, null);
                var model = (BookItemsIndexViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(items[0].BookItemId, model.Index[0].BookItemId);
                Assert.Equal(items.Count(), model.Index.Count());
            }
        }

        [Fact]
        public void IndexByRack_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var items = GetSampleItemsViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<List<BookItemItemViewModel>>(It.IsAny<List<BookItemsIndexItemDTO>>()))
                    .Returns(items);

                var controller = mock.Create<BookItemsController>();

                //Act
                var result = (ViewResult)controller.IndexByRack(new Guid(), null, null);
                var model = (BookItemsIndexViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(items[0].BookItemId, model.Index[0].BookItemId);
                Assert.Equal(items.Count(), model.Index.Count());
            }
        }

        [Fact]
        public void Create_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleCreateViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<BookItemCreateViewModel>(It.IsAny<BookItemCreateDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<BookItemsController>();

                //Act
                var result = (ViewResult)controller.Create(new Guid());
                var model = (BookItemCreateViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.BookId, model.BookId);
            }
        }

        [Fact]
        public void CreateBookItem_ModelStateIsValid_Creates()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<BookItemsController>();
                var mockDataAccess = mock.Mock<IBookItemUpdateService>();

                //Act
                var result = controller.CreateBookItem(new BookItemCreateViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Create(It.IsAny<BookItemCreateDTO>()), Times.Once);
            }
        }

        [Fact]
        public void Edit_ModelStateIsValid_Edits()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<BookItemsController>();
                var mockDataAccess = mock.Mock<IBookItemUpdateService>();

                //Act
                var result = controller.Edit(new BookItemEditViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Edit(It.IsAny<BookItemEditDTO>()), Times.Once);
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
                    .Setup(m => m.Map<BookItemDeleteViewModel>(It.IsAny<BookItemDeleteDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<BookItemsController>();

                //Act
                var result = (ViewResult)controller.Delete(new Guid());
                var model = (BookItemDeleteViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.BookItemId, model.BookItemId);
            }
        }

        [Fact]
        public void Delete_ModelStateIsValid_Deletes()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<BookItemsController>();
                var mockDataAccess = mock.Mock<IBookItemUpdateService>();

                //Act
                var result = controller.Delete(new BookItemDeleteViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
            }
        }

        [Fact]
        public void Reserve_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleReserveViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<BookItemReserveViewModel>(It.IsAny<BookItemReserveDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<BookItemsController>();

                //Act
                var result = (ViewResult)controller.Reserve(new Guid());
                var model = (BookItemReserveViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.BookItemId, model.BookItemId);
            }
        }

        [Fact]
        public void Reserve_ModelStateIsValid_Reserves()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<BookItemsController>();
                var mockDataAccess = mock.Mock<IBookItemUpdateService>();

                //Act
                var result = controller.Reserve(new BookItemReserveViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Reserve(It.IsAny<BookItemReserveDTO>()), Times.Once);
            }
        }

        [Fact]
        public void CancelReservation_Authorized_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleReservationCancelViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<BookItemReservationCancelViewModel>(It.IsAny<BookItemReserveDTO>()))
                    .Returns(viewModel);

                mock.Mock<ICustomAuthorizationService>()
                    .Setup(x => x.AuthorizeAsync(
                        It.IsAny<ClaimsPrincipal>(),
                        It.IsAny<Guid>(),
                        OperationAuthorizationRequirements.CancelReservation))
                    .ReturnsAsync(AuthorizationResult.Success());

                var controller = mock.Create<BookItemsController>();

                //Act
                var result = (ViewResult)controller.CancelReservation(new Guid());
                var model = (BookItemReservationCancelViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.BookItemId, model.BookItemId);
            }
        }

        [Fact]
        public void CancelReservation_Cancels()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<BookItemsController>();
                var mockDataAccess = mock.Mock<IBookItemUpdateService>();

                //Act
                var result = controller.CancelReservation(new BookItemReservationCancelViewModel());

                //Assert
                mockDataAccess.Verify(x => x.CancelReservation(It.IsAny<Guid>()), Times.Once);
            }
        }

        private List<BookItemItemViewModel> GetSampleItemsViewModel()
        {
            var output = new List<BookItemItemViewModel>()
            {
                new BookItemItemViewModel()
                {
                   BookItemId = Guid.NewGuid()
                },
                new BookItemItemViewModel()
                {
                   BookItemId = Guid.NewGuid()
                }
            };

            return output;
        }

        private BookItemCreateViewModel GetSampleCreateViewModel()
        {
            var output = new BookItemCreateViewModel()
            {
                BookId = Guid.NewGuid()
            };

            return output;
        }

        private BookItemDeleteViewModel GetSampleDeleteViewModel()
        {
            var output = new BookItemDeleteViewModel()
            {
                BookItemId = Guid.NewGuid()
            };

            return output;
        }

        private BookItemReserveViewModel GetSampleReserveViewModel()
        {
            var output = new BookItemReserveViewModel()
            {
                BookItemId = Guid.NewGuid()
            };

            return output;
        }

        private BookItemReservationCancelViewModel GetSampleReservationCancelViewModel()
        {
            var output = new BookItemReservationCancelViewModel()
            {
                BookItemId = Guid.NewGuid()
            };

            return output;
        }
    }
}
