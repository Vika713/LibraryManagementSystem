using Autofac.Extras.Moq;
using AutoMapper;
using Business.Authorization;
using Business.Identity;
using Business.People;
using Business.People.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Web.Controllers;
using Web.ViewModels.People;
using Xunit;

namespace LibraryManagementTests.Controllers
{
    public class PeopleControllerTests
    {
        [Fact]
        public void Index_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var items = GetSampleItemsViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<List<PersonItemViewModel>>(It.IsAny<List<PeopleIndexItemDTO>>()))
                    .Returns(items);

                var controller = mock.Create<PeopleController>();

                //Act
                var result = (ViewResult)controller.Index(new PeopleIndexViewModel());
                var model = (PeopleIndexViewModel)result.ViewData.Model;

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
                    .Setup(m => m.Map<List<PersonItemViewModel>>(It.IsAny<List<PeopleIndexItemDTO>>()))
                    .Returns(items);

                var controller = mock.Create<PeopleController>();

                //Act
                var result = (ViewResult)controller.Index(new PeopleIndexViewModel());
                var model = (PeopleIndexViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(items.Count(), model.Index.Count());
            }
        }

        [Fact]
        public void Details_Authorized_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<ICustomAuthorizationService>()
                    .Setup(x => x.AuthorizeAsync(
                        It.IsAny<ClaimsPrincipal>(),
                        It.IsAny<Guid>(),
                        OperationAuthorizationRequirements.PersonDetails))
                    .ReturnsAsync(AuthorizationResult.Success());

                var viewModel = GetSampleDetailsViewModel();

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PersonDetailsViewModel>(It.IsAny<PersonDetailsDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<PeopleController>();

                //Act
                var result = (ViewResult)controller.Details(new Guid());
                var model = (PersonDetailsViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.Id, model.Id);
            }
        }

        [Fact]
        public void Create_ModelStateIsValid_Member_AddsAsMember()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PersonCreateDTO>(It.IsAny<PersonCreateViewModel>()))
                    .Returns(new PersonCreateDTO());

                mock.Mock<IPersonQueriesService>()
                    .Setup(x => x.GetPersonId(It.IsAny<string>()))
                    .Returns(new Guid());

                var mockPersonUpdateServiceAccess = mock.Mock<IPersonUpdateService>();

                var controller = mock.Create<PeopleController>();

                //Act
                var result = controller.Create(GetSampleCreateViewModel());

                //Assert
                mock.Mock<IPersonUpdateService>()
                    .Verify(x => x.AddAsMember(It.IsAny<Guid>()), Times.Once);
            }
        }

        [Fact]
        public void Create_ModelStateIsValid_Librarian_AddsAsLibrarian()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PersonCreateDTO>(It.IsAny<PersonCreateViewModel>()))
                    .Returns(new PersonCreateDTO());

                mock.Mock<IPersonQueriesService>()
                    .Setup(x => x.GetPersonId(It.IsAny<string>()))
                    .Returns(new Guid());

                var controller = mock.Create<PeopleController>();

                //Act
                var result = controller.Create(GetSampleCreateViewModel());

                //Assert
                mock.Mock<IPersonUpdateService>()
                   .Verify(x => x.AddAsLibrarian(It.IsAny<Guid>()), Times.Once);
            }
        }

        [Fact]
        public void Create_ModelStateIsValid_Creates()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var mockDataAccess = mock.Mock<IPersonUpdateService>();
                var controller = mock.Create<PeopleController>();

                //Act
                var result = controller.Create(new PersonCreateViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Create(It.IsAny<PersonCreateDTO>()), Times.Once);
            }
        }

        [Fact]
        public void Edit_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<ICustomAuthorizationService>()
                    .Setup(x => x.AuthorizeAsync(
                        It.IsAny<ClaimsPrincipal>(),
                        It.IsAny<Guid>(),
                        OperationAuthorizationRequirements.PersonEdit))
                    .ReturnsAsync(AuthorizationResult.Success());

                PersonEditViewModel viewModel = GetSampleEditViewModel();

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PersonEditViewModel>(It.IsAny<PersonEditDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<PeopleController>();

                //Act
                var result = (ViewResult)controller.Edit(new Guid());
                var model = (PersonEditViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.Id, model.Id);
            }
        }

        [Fact]
        public void Edit_Member_AddsAsMember()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PersonEditDTO>(It.IsAny<PersonEditViewModel>()))
                    .Returns(new PersonEditDTO());

                mock.Mock<IIdentityService>()
                    .Setup(x => x.CurrentUserIsInRole(It.IsAny<string>()))
                    .Returns(true);

                var controller = mock.Create<PeopleController>();

                //Act
                var result = controller.Edit(GetSampleEditViewModel());

                //Assert
                mock.Mock<IPersonUpdateService>()
                    .Verify(x => x.AddAsMember(It.IsAny<Guid>()), Times.Once);
            }
        }

        [Fact]
        public void Edit_Librarian_AddsAsLibrarian()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PersonEditDTO>(It.IsAny<PersonEditViewModel>()))
                    .Returns(new PersonEditDTO());

                mock.Mock<IIdentityService>()
                    .Setup(x => x.CurrentUserIsInRole(It.IsAny<string>()))
                    .Returns(true);

                var controller = mock.Create<PeopleController>();

                //Act
                var result = controller.Edit(GetSampleEditViewModel());

                //Assert
                mock.Mock<IPersonUpdateService>()
                    .Verify(x => x.AddAsLibrarian(It.IsAny<Guid>()), Times.Once);
            }
        }

        [Fact]
        public void Edit_UpdatesRoles()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PersonEditDTO>(It.IsAny<PersonEditViewModel>()))
                    .Returns(new PersonEditDTO());

                mock.Mock<IIdentityService>()
                    .Setup(x => x.UserExists(It.IsAny<string>()))
                    .Returns(true);

                var controller = mock.Create<PeopleController>();

                //Act
                var result = controller.Edit(GetSampleEditViewModel());

                //Assert
                mock.Mock<IIdentityService>()
                    .Verify(x => x.UpdateRoles(It.IsAny<string>()), Times.Once);
            }
        }

        [Fact]
        public void Edit_ModelStateIsValid_Edits()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IMapper>()
                    .Setup(x => x.Map<PersonEditDTO>(It.IsAny<PersonEditViewModel>()))
                    .Returns(new PersonEditDTO());

                var mockDataAccess = mock.Mock<IPersonUpdateService>();
                var controller = mock.Create<PeopleController>();

                //Act
                var result = controller.Edit(new PersonEditViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Edit(It.IsAny<PersonEditDTO>()), Times.Once);
            }
        }

        private PersonCreateViewModel GetSampleCreateViewModel()
        {
            var output = new PersonCreateViewModel()
            {
                PersonalCode = "personalCode",
                Member = true,
                Librarian = true
            };

            return output;
        }

        private PersonEditViewModel GetSampleEditViewModel()
        {
            var output = new PersonEditViewModel()
            {
                Id = Guid.NewGuid(),
                Member = true,
                Librarian = true
            };

            return output;
        }

        private PersonDetailsViewModel GetSampleDetailsViewModel()
        {
            var output = new PersonDetailsViewModel()
            {
                Id = Guid.NewGuid()
            };

            return output;
        }

        private List<PersonItemViewModel> GetSampleItemsViewModel()
        {
            var output = new List<PersonItemViewModel>()
            {
                new PersonItemViewModel()
                {
                   Id = Guid.NewGuid()
                },
                new PersonItemViewModel()
                {
                   Id = Guid.NewGuid()
                }
            };

            return output;
        }
    }
}
