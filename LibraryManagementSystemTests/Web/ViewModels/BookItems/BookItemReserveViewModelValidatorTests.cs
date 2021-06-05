using Autofac.Extras.Moq;
using Common.Constants;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.BookItems;
using Domain.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using Web.ViewModels.BookItems;
using Xunit;

namespace LibraryManagementTests.ViewModels.BookItems
{
    public class BookItemReserveViewModelValidatorTests
    {
        [Fact]
        public void BookItemReserveViewModelValidator_ModelIsValid_ReturnsTrue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = GetSampleModel();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.Get(model.BookItemId))
                    .Returns(GetSampleBookItem());

                var validator = mock.Create<BookItemReserveViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void BookItemReserveViewModelValidator_MemberConnotReserve_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = GetSampleModel();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.Get(model.BookItemId))
                    .Returns(GetSampleBookItem());

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByReservedMemberId(model.MemberId))
                    .Returns(GetSampleBookItems());

                var validator = mock.Create<BookItemReserveViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void BookItemReserveViewModelValidator_BookItemStatusIsLost_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = GetSampleModel();
                var bookItem = GetSampleBookItem();
                bookItem.SetStatus(BookStatus.Lost);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.Get(model.BookItemId))
                    .Returns(bookItem);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByReservedMemberId(model.MemberId))
                    .Returns(GetSampleBookItems());

                var validator = mock.Create<BookItemReserveViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void BookItemReserveViewModelValidator_BookItemIsReserved_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = GetSampleModel();
                var bookItem = GetSampleBookItem();
                bookItem.Reserve(Guid.NewGuid());

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.Get(model.BookItemId))
                    .Returns(bookItem);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByReservedMemberId(model.MemberId))
                    .Returns(GetSampleBookItems());

                var validator = mock.Create<BookItemReserveViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void BookItemReserveViewModelValidator_MemberCannotReserveBookItem_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = GetSampleModel();
                var bookItem = GetSampleBookItem();
                bookItem.Lend(model.MemberId);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.Get(model.BookItemId))
                    .Returns(bookItem);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByReservedMemberId(model.MemberId))
                    .Returns(GetSampleBookItems());

                var validator = mock.Create<BookItemReserveViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private BookItemReserveViewModel GetSampleModel()
        {
            var output = new BookItemReserveViewModel()
            {
                BookItemId = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                MemberId = Guid.NewGuid(),
            };

            return output;
        }

        private BookItem GetSampleBookItem()
        {
            var output = new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null);

            return output;
        }

        private List<BookItem> GetSampleBookItems()
        {
            var output = new List<BookItem>();

            for (int i = 0; i < Consts.MaxBooksPerMember; i++)
            {
                output.Add(new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null));
            };

            return output;
        }
    }
}
