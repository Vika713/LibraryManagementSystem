using Autofac.Extras.Moq;
using Common.Resources;
using Data.Repositories.BookItems;
using Data.Repositories.Racks;
using Domain.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using Web.ViewModels.BookItems;
using Xunit;

namespace LibraryManagementTests.ViewModels.BookItems
{
    public class BookItemEditViewModelValidatorTests
    {
        [Fact]
        public void BookItemEditViewModelValidator_ModelIsValid_ReturnsTrue()
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

                var validator = mock.Create<BookItemEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void BookItemEditViewModelValidator_EmptyBarcode_ReturnsFalse()
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
                model.Barcode = string.Empty;

                var validator = mock.Create<BookItemEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void BookItemEditViewModelValidator_NotUniqueBarcode_ReturnsFalse()
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
                    .Setup(x => x.GetByBarcode(model.Barcode))
                    .Returns(new BookItem());

                var validator = mock.Create<BookItemEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void BookItemEditViewModelValidator_RackDoesNotExist_ReturnsFalse()
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
                model.RackNumber = 1;
                model.LocationIdentifier = "identifier";

                mock.Mock<IRackRepository>()
                    .Setup(x => x.Exists((int)model.RackNumber, model.LocationIdentifier))
                    .Returns(false);

                var validator = mock.Create<BookItemEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private BookItemEditViewModel GetValidSampleModel()
        {
            var output = new BookItemEditViewModel()
            {
                Barcode = "barcode",
                BookItemId = Guid.NewGuid()
            };

            return output;
        }
    }
}
