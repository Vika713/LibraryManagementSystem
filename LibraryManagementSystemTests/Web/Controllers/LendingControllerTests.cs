using Autofac.Extras.Moq;
using AutoMapper;
using Business.Lending;
using Business.Lending.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Web.Controllers;
using Web.ViewModels.Lending;
using Xunit;

namespace LibraryManagementTests.Controllers
{
    public class LendingControllerTests
    {
        [Fact]
        public void CheckOut_ModelStateIsValid_Lends()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IMapper>()
                    .Setup(m => m.Map<ScanDTO>(It.IsAny<CheckOutViewModel>()))
                    .Returns(new ScanDTO());

                var controller = mock.Create<LendingController>();
                var mockDataAccess = mock.Mock<ILendingUpdateService>();

                //Act
                var result = controller.CheckOut(new CheckOutViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Lend(It.IsAny<ScanDTO>()), Times.Once);
            }
        }

        [Fact]
        public void CheckOut_ModelStateIsValid_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IMapper>()
                    .Setup(m => m.Map<ScanDTO>(It.IsAny<CheckOutViewModel>()))
                    .Returns(new ScanDTO());

                var dueDate = DateTime.Now;

                mock.Mock<ILendingQueriesService>()
                    .Setup(m => m.GetDueDate(It.IsAny<string>()))
                    .Returns(dueDate);

                var expected = new DueDateViewModel(dueDate);

                var controller = mock.Create<LendingController>();

                //Act
                var result = (ViewResult)controller.CheckOut(new CheckOutViewModel());
                var model = (DueDateViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(expected.DueDate, model.DueDate);
            }
        }

        [Fact]
        public void Return_ModelStateIsValid_IsOverdue_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleFineViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<LendingFineViewModel>(It.IsAny<LendingFineDTO>()))
                    .Returns(viewModel);

                mock.Mock<ILendingQueriesService>()
                    .Setup(m => m.IsOverdue(It.IsAny<string>()))
                    .Returns(true);

                var controller = mock.Create<LendingController>();

                //Act
                var result = (ViewResult)controller.Return(new ReturnViewModel());
                var model = (LendingFineViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.Fine, model.Fine);
            }
        }

        [Fact]
        public void Return_ModelStateIsValid_NotOverdue_Returns()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<ILendingQueriesService>()
                    .Setup(m => m.IsOverdue(It.IsAny<string>()))
                    .Returns(false);

                var controller = mock.Create<LendingController>();
                var mockDataAccess = mock.Mock<ILendingUpdateService>();

                //Act
                var result = controller.Return(new ReturnViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Return(It.IsAny<string>()), Times.Once);
            }
        }

        [Fact]
        public void ReturnFine_ModelStateIsValid_Returns()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<LendingController>();
                var mockDataAccess = mock.Mock<ILendingUpdateService>();

                //Act
                var result = controller.ReturnFine(new LendingFineViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Return(It.IsAny<string>()), Times.Once);
            }
        }

        [Fact]
        public void Renew_ModelStateIsValid_IsOverdue_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleFineViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<LendingFineViewModel>(It.IsAny<LendingFineDTO>()))
                    .Returns(viewModel);

                mock.Mock<ILendingQueriesService>()
                    .Setup(m => m.IsOverdue(It.IsAny<string>()))
                    .Returns(true);

                var controller = mock.Create<LendingController>();

                //Act
                var result = (ViewResult)controller.Renew(new RenewViewModel());
                var model = (LendingFineViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.Fine, model.Fine);
            }
        }

        [Fact]
        public void ManageRenew_BookIsReserved_Returns()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<LendingController>();

                var mockDataAccess = mock.Mock<ILendingUpdateService>();

                mock.Mock<ILendingQueriesService>()
                    .Setup(m => m.BookIsReserved(It.IsAny<string>()))
                    .Returns(true);

                //Act
                var result = controller.ManageRenew(new string(new char[] { }));

                //Assert
                mockDataAccess.Verify(x => x.Return(It.IsAny<string>()), Times.Once);
            }
        }

        [Fact]
        public void ManageRenew_BookNotReserved_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var mockDataAccess = mock.Mock<ILendingQueriesService>();

                mock.Mock<ILendingQueriesService>()
                    .Setup(m => m.BookIsReserved(It.IsAny<string>()))
                    .Returns(false);

                var dueDate = DateTime.Now;

                mock.Mock<ILendingQueriesService>()
                    .Setup(m => m.GetDueDate(It.IsAny<string>()))
                    .Returns(dueDate);

                var expected = new DueDateViewModel(dueDate);

                var controller = mock.Create<LendingController>();

                //Act
                var result = (ViewResult)controller.ManageRenew(new string(new char[] { }));
                var model = (DueDateViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(expected.DueDate, model.DueDate);
            }
        }

        [Fact]
        public void ManageRenew_BookNotReserved_Renews()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<LendingController>();

                var mockDataAccess = mock.Mock<ILendingUpdateService>();

                mock.Mock<ILendingQueriesService>()
                    .Setup(m => m.BookIsReserved(It.IsAny<string>()))
                    .Returns(false);

                //Act
                var result = controller.ManageRenew(new string(new char[] { }));

                //Assert
                mockDataAccess.Verify(x => x.Renew(It.IsAny<string>()), Times.Once);
            }
        }

        private LendingFineViewModel GetSampleFineViewModel()
        {
            var output = new LendingFineViewModel()
            {
                Fine = 1
            };

            return output;
        }
    }
}
