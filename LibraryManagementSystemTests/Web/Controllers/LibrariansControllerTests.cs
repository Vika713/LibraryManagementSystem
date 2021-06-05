using Autofac.Extras.Moq;
using AutoMapper;
using Business.Authorization;
using Business.Identity;
using Business.Librarians;
using Business.Librarians.DTOs;
using Common.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Security.Claims;
using Web.Controllers;
using Web.ViewModels.Librarians;
using Xunit;

namespace LibraryManagementTests.Controllers
{
    public class LibrariansControllerTests
    {
        [Fact]
        public void Details_Authorized_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleDetailsViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<LibrarianDetailsViewModel>(It.IsAny<LibrarianDetailsDTO>()))
                    .Returns(viewModel);

                mock.Mock<ICustomAuthorizationService>()
                    .Setup(x => x.AuthorizeAsync(
                        It.IsAny<ClaimsPrincipal>(),
                        It.IsAny<Guid>(),
                        OperationAuthorizationRequirements.LibrarianDetails))
                    .ReturnsAsync(AuthorizationResult.Success());

                var controller = mock.Create<LibrariansController>();

                //Act
                var result = (ViewResult)controller.Details(new Guid());
                var model = (LibrarianDetailsViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.Id, model.Id);
            }
        }

        [Fact]
        public void AccountStatusChange_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleAccountStatusChangeViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<LibrarianStatusChangeViewModel>(It.IsAny<LibrarianStatusChangeDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<LibrariansController>();

                //Act
                var result = (ViewResult)controller.AccountStatusChange(new Guid());
                var model = (LibrarianStatusChangeViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.Id, model.Id);
            }
        }

        [Fact]
        public void AccountStatusChange_ModelStateIsValid_ChangesStatus()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<LibrariansController>();
                var mockDataAccess = mock.Mock<ILibrarianUpdateService>();

                //Act
                var result = controller.AccountStatusChange(new LibrarianStatusChangeViewModel());

                //Assert
                mockDataAccess.Verify(x => x.ChangeStatus(It.IsAny<Guid>(), It.IsAny<LibrarianStatus>()), Times.Once);
            }
        }

        [Fact]
        public void AccountStatusChange_UserExists_Updates()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<LibrariansController>();
                var mockDataAccess = mock.Mock<IIdentityService>();
                mockDataAccess
                   .Setup(m => m.UserExists(It.IsAny<string>()))
                   .Returns(true);

                //Act
                var result = controller.AccountStatusChange(new LibrarianStatusChangeViewModel());

                //Assert
                mockDataAccess.Verify(x => x.UpdateRoles(It.IsAny<string>()), Times.Once);
                mockDataAccess.Verify(x => x.UpdateUserAccount(It.IsAny<string>()), Times.Once);
            }
        }

        private LibrarianDetailsViewModel GetSampleDetailsViewModel()
        {
            var output = new LibrarianDetailsViewModel()
            {
                Id = Guid.NewGuid()
            };

            return output;
        }

        private LibrarianStatusChangeViewModel GetSampleAccountStatusChangeViewModel()
        {
            var output = new LibrarianStatusChangeViewModel()
            {
                Id = Guid.NewGuid()
            };

            return output;
        }
    }
}
