using Autofac.Extras.Moq;
using AutoMapper;
using Business.Authorization;
using Business.Identity;
using Business.Members;
using Business.Members.DTOs;
using Common.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Security.Claims;
using Web.Controllers;
using Web.ViewModels.Members;
using Xunit;

namespace LibraryManagementTests.Controllers
{
    public class MembersControllerTests
    {
        [Fact]
        public void Details_Authorized_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleDetailsViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<MemberDetailsViewModel>(It.IsAny<MemberDetailsDTO>()))
                    .Returns(viewModel);

                mock.Mock<ICustomAuthorizationService>()
                    .Setup(x => x.AuthorizeAsync(
                        It.IsAny<ClaimsPrincipal>(),
                        It.IsAny<Guid>(),
                        OperationAuthorizationRequirements.MemberDetails))
                    .ReturnsAsync(AuthorizationResult.Success());

                var controller = mock.Create<MembersController>();

                //Act
                var result = (ViewResult)controller.Details(new Guid());
                var model = (MemberDetailsViewModel)result.ViewData.Model;

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
                    .Setup(m => m.Map<MemberStatusChangeViewModel>(It.IsAny<MemberStatusChangeDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<MembersController>();

                //Act
                var result = (ViewResult)controller.AccountStatusChange(new Guid());
                var model = (MemberStatusChangeViewModel)result.ViewData.Model;

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
                var controller = mock.Create<MembersController>();
                var mockDataAccess = mock.Mock<IMemberUpdateService>();

                //Act
                var result = controller.AccountStatusChange(new MemberStatusChangeViewModel());

                //Assert
                mockDataAccess.Verify(x => x.ChangeStatus(It.IsAny<Guid>(), It.IsAny<MemberStatus>()), Times.Once);
            }
        }

        [Fact]
        public void AccountStatusChange_UserExists_Updates()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<MembersController>();
                var mockDataAccess = mock.Mock<IIdentityService>();
                mockDataAccess
                   .Setup(m => m.UserExists(It.IsAny<string>()))
                   .Returns(true);

                //Act
                var result = controller.AccountStatusChange(new MemberStatusChangeViewModel());

                //Assert
                mockDataAccess.Verify(x => x.UpdateRoles(It.IsAny<string>()), Times.Once);
                mockDataAccess.Verify(x => x.UpdateUserAccount(It.IsAny<string>()), Times.Once);
            }
        }

        [Fact]
        public void BorrowedBookItems_Authorized_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleBorrowViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<MembersBorrowViewModel>(It.IsAny<MembersBorrowDTO>()))
                    .Returns(viewModel);

                mock.Mock<ICustomAuthorizationService>()
                    .Setup(x => x.AuthorizeAsync(
                        It.IsAny<ClaimsPrincipal>(),
                        It.IsAny<Guid>(),
                        OperationAuthorizationRequirements.MemberBookItems))
                    .ReturnsAsync(AuthorizationResult.Success());

                var controller = mock.Create<MembersController>();

                //Act
                var result = (ViewResult)controller.BorrowedBookItems(new Guid());
                var model = (MembersBorrowViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.Id, model.Id);
            }
        }

        [Fact]
        public void ReservedBookItems_Authorized_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleReserveViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<MembersReserveViewModel>(It.IsAny<MembersReserveDTO>()))
                    .Returns(viewModel);

                mock.Mock<ICustomAuthorizationService>()
                    .Setup(x => x.AuthorizeAsync(
                        It.IsAny<ClaimsPrincipal>(),
                        It.IsAny<Guid>(),
                        OperationAuthorizationRequirements.MemberBookItems))
                    .ReturnsAsync(AuthorizationResult.Success());

                var controller = mock.Create<MembersController>();

                //Act
                var result = (ViewResult)controller.ReservedBookItems(new Guid());
                var model = (MembersReserveViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.Id, model.Id);
            }
        }

        private MemberDetailsViewModel GetSampleDetailsViewModel()
        {
            var output = new MemberDetailsViewModel()
            {
                Id = Guid.NewGuid()
            };

            return output;
        }

        private MemberStatusChangeViewModel GetSampleAccountStatusChangeViewModel()
        {
            var output = new MemberStatusChangeViewModel()
            {
                Id = Guid.NewGuid()
            };

            return output;
        }

        private MembersBorrowViewModel GetSampleBorrowViewModel()
        {
            var output = new MembersBorrowViewModel()
            {
                Id = Guid.NewGuid()
            };

            return output;
        }

        private MembersReserveViewModel GetSampleReserveViewModel()
        {
            var output = new MembersReserveViewModel()
            {
                Id = Guid.NewGuid()
            };

            return output;
        }
    }
}
