using Autofac.Extras.Moq;
using Common.Resources;
using Data.Repositories.BookItems;
using Data.Repositories.Racks;
using Microsoft.Extensions.Localization;
using Moq;
using Web.ViewModels.BookItems;
using Xunit;

namespace LibraryManagementTests.ViewModels.BookItems
{
    public class BookItemCreateViewModelValidatorTests
    {
        [Fact]
        public void BookItemCreateViewModelValidator_ModelIsValid_ReturnsTrue()
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

                var validator = mock.Create<BookItemCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void BookItemCreateViewModelValidator_EmptyBarcode_ReturnsFalse()
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

                var validator = mock.Create<BookItemCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void BookItemCreateViewModelValidator_NotUniqueBarcode_ReturnsFalse()
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
                    .Setup(x => x.ExistsByBarcode(model.Barcode))
                    .Returns(true);

                var validator = mock.Create<BookItemCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void BookItemCreateViewModelValidator_RackDoesNotExist_ReturnsFalse()
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

                var validator = mock.Create<BookItemCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private BookItemCreateViewModel GetValidSampleModel()
        {
            var output = new BookItemCreateViewModel()
            {
                Barcode = "barcode"
            };

            return output;
        }
    }
}
