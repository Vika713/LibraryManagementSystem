using Autofac.Extras.Moq;
using AutoMapper;
using Business.Books;
using Business.Books.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Web.Controllers;
using Web.ViewModels.Books;
using Xunit;

namespace LibraryManagementTests.Controllers
{
    public class BooksControllerTests
    {
        [Fact]
        public void Index_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var items = GetSampleItemsViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<List<BookItemViewModel>>(It.IsAny<List<BooksIndexItemDTO>>()))
                    .Returns(items);

                var controller = mock.Create<BooksController>();

                //Act
                var result = (ViewResult)controller.Index(new BooksIndexViewModel());
                var model = (BooksIndexViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(items[0].BookId, model.Index[0].BookId);
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
                    .Setup(m => m.Map<List<BookItemViewModel>>(It.IsAny<List<BooksIndexItemDTO>>()))
                    .Returns(items);

                var controller = mock.Create<BooksController>();

                //Act
                var result = (ViewResult)controller.Index(new BooksIndexViewModel());
                var model = (BooksIndexViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(items.Count, model.Index.Count);
            }
        }

        [Fact]
        public void Create_ModelStateIsValid_BookExists_RedirectsToCorrectAction()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IBookQueriesService>()
                    .Setup(x => x.BookExists(It.IsAny<string>()))
                    .Returns(true);

                var controller = mock.Create<BooksController>();

                //Act
                var result = (RedirectToActionResult)controller.Create(new ISBNCreateViewModel());

                //Assert
                Assert.Equal(nameof(MVC.BookItems.Create), result.ActionName);
                Assert.Equal(nameof(MVC.BookItems), result.ControllerName);
            }
        }

        [Fact]
        public void Create_ModelStateIsValid_BookDoesNotExist_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IBookQueriesService>()
                    .Setup(x => x.BookExists(It.IsAny<string>()))
                    .Returns(false);

                BookCreateViewModel viewModel = GetSampleCreateViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<BookCreateViewModel>(It.IsAny<BookCreateDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<BooksController>();

                //Act
                var result = (ViewResult)controller.Create(new ISBNCreateViewModel());
                var model = (BookCreateViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.ISBN, model.ISBN);
            }
        }

        [Fact]
        public void CreateBook_ModelStateIsValid_Creates()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<BooksController>();
                var mockDataAccess = mock.Mock<IBookUpdateService>();

                //Act
                var result = controller.CreateBook(new BookCreateViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Create(It.IsAny<BookCreateDTO>()), Times.Once);
            }
        }

        [Fact]
        public void Edit_ReturnsCorrectModel()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                BookEditViewModel viewModel = GetSampleEditViewModel();

                mock.Mock<IMapper>()
                    .Setup(m => m.Map<BookEditViewModel>(It.IsAny<BookEditDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<BooksController>();

                //Act
                var result = (ViewResult)controller.Edit(new Guid());
                var model = (BookEditViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.BookId, model.BookId);
            }
        }

        [Fact]
        public void Edit_ModelStateIsValid_Edits()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<BooksController>();
                var mockDataAccess = mock.Mock<IBookUpdateService>();

                //Act
                var result = controller.Edit(new BookEditViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Edit(It.IsAny<BookEditDTO>()), Times.Once);
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
                    .Setup(m => m.Map<BookDeleteViewModel>(It.IsAny<BookDeleteDTO>()))
                    .Returns(viewModel);

                var controller = mock.Create<BooksController>();

                //Act
                var result = (ViewResult)controller.Delete(new Guid());
                var model = (BookDeleteViewModel)result.ViewData.Model;

                //Assert
                Assert.Equal(viewModel.BookId, model.BookId);
            }
        }

        [Fact]
        public void Delete_ModelStateIsValid_Deletes()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var controller = mock.Create<BooksController>();
                var mockDataAccess = mock.Mock<IBookUpdateService>();

                //Act
                var result = controller.Delete(new BookDeleteViewModel());

                //Assert
                mockDataAccess.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
            }
        }

        private List<BookItemViewModel> GetSampleItemsViewModel()
        {
            var output = new List<BookItemViewModel>()
            {
                new BookItemViewModel()
                {
                   BookId = Guid.NewGuid()
                },
                new BookItemViewModel()
                {
                   BookId = Guid.NewGuid()
                }
            };

            return output;
        }

        private BookEditDTO GetSampleEditDTO()
        {
            var output = new BookEditDTO()
            {
                BookId = Guid.NewGuid()
            };

            return output;
        }

        private BookEditViewModel GetSampleEditViewModel()
        {
            var output = new BookEditViewModel()
            {
                BookId = Guid.NewGuid()
            };

            return output;
        }

        private BookCreateViewModel GetSampleCreateViewModel()
        {
            var output = new BookCreateViewModel()
            {
                ISBN = "ISBN"
            };

            return output;
        }

        private BookDeleteViewModel GetSampleDeleteViewModel()
        {
            var output = new BookDeleteViewModel()
            {
                BookId = Guid.NewGuid()
            };

            return output;
        }
    }
}
