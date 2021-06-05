using Autofac.Extras.Moq;
using AutoMapper;
using Business.Authorization;
using Business.Cards;
using Business.Cards.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Security.Claims;
using Web.Controllers;
using Web.ViewModels.Cards;
using Xunit;

namespace LibraryManagementTests.Controllers
{
    public class CardsControllerTests
    {
        [Fact]
        public void Create_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleCreateViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<CardCreateViewModel>(It.IsAny<CardCreateDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<CardsController>();

                //Act
                var result = (ViewResult)controller.Create(new Guid());
                var model = (CardCreateViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.MemberId, viewModel.MemberId);
            }
        }

        [Fact]
        public void Create_ModelStateIsValid_Creates()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<CardsController>();
                var mockDataAccess = mock.Mock<ICardUpdateService>();

                //Act
                var result = controller.Create(new CardCreateViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Create(It.IsAny<CardCreateDTO>()), Times.Once);
            }
        }

        [Fact]
        public void Create_ModelStateIsValid_MakesCardsInactive()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<CardsController>();
                var mockDataAccess = mock.Mock<ICardUpdateService>();

                //Act
                var result = controller.Create(new CardCreateViewModel());

                //Assert
                mockDataAccess.Verify(x => x.MakeAllCardsInactive(It.IsAny<Guid>()), Times.Once);
            }
        }

        [Fact]
        public void Block_Authorized_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var viewModel = GetSampleBlockViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<CardBlockViewModel>(It.IsAny<CardBlockDTO>()))
                    .Returns(viewModel);

                mock.Mock<ICustomAuthorizationService>()
                   .Setup(x => x.AuthorizeAsync(
                       It.IsAny<ClaimsPrincipal>(),
                       It.IsAny<Guid>(),
                       OperationAuthorizationRequirements.CardBlock))
                   .ReturnsAsync(AuthorizationResult.Success());

                var controller = mock.Create<CardsController>();

                //Act
                var result = (ViewResult)controller.Block(new Guid());
                var model = (CardBlockViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.Id, viewModel.Id);
            }
        }

        [Fact]
        public void Block_MakesCardsInactive()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<CardsController>();
                var mockDataAccess = mock.Mock<ICardUpdateService>();

                //Act
                var result = controller.Block(new CardBlockViewModel());

                //Assert
                mockDataAccess.Verify(x => x.MakeAllCardsInactive(It.IsAny<Guid>()), Times.Once);
            }
        }

        private CardCreateViewModel GetSampleCreateViewModel()
        {
            var output = new CardCreateViewModel()
            {
                MemberId = Guid.NewGuid()
            };

            return output;
        }

        private CardBlockViewModel GetSampleBlockViewModel()
        {
            var output = new CardBlockViewModel()
            {
                Id = Guid.NewGuid()
            };

            return output;
        }
    }
}
