using Autofac.Extras.Moq;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.BookItems;
using Domain.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using Web.ViewModels.Lending;
using Xunit;

namespace LibraryManagementTests.ViewModels.Lending
{
    public class ReturnViewModelValidatorTests
    {
        [Fact]
        public void ReturnViewModelValidator_ModelIsValid_ReturnsTrue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = GetValidSampleModel();
                var bookItem = GetSampleBookItem();
                bookItem.Lend(Guid.NewGuid());

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(bookItem);

                var validator = mock.Create<ReturnViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void ReturnViewModelValidator_BookItemDoesNotExist_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = GetValidSampleModel();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns((BookItem)null);

                var validator = mock.Create<ReturnViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ReturnViewModelValidator_BookItemIsNotLoaned_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = GetValidSampleModel();

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(GetSampleBookItem());

                var validator = mock.Create<ReturnViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private ReturnViewModel GetValidSampleModel()
        {
            var output = new ReturnViewModel()
            {
                BookBarcode = "barcode"
            };

            return output;
        }

        private BookItem GetSampleBookItem()
        {
            var output = new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null);

            return output;
        }
    }
}
