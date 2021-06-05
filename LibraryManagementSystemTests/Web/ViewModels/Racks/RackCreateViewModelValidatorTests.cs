using Autofac.Extras.Moq;
using Common.Resources;
using Data.Repositories.Racks;
using Microsoft.Extensions.Localization;
using Moq;
using Web.ViewModels.Racks;
using Xunit;

namespace LibraryManagementTests.ViewModels.Racks
{
    public class RackCreateViewModelValidatorTests
    {
        [Fact]
        public void RackCreateViewModelValidator_ModelIsValid_ReturnsTrue()
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

                var validator = mock.Create<RackCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void RackCreateViewModelValidator_LocationIdentifierIsEmpty_ReturnsFalse()
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
                model.LocationIdentifier = string.Empty;

                var validator = mock.Create<RackCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void RackCreateViewModelValidator_NotUniqueRack_ReturnsFalse()
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

                mock.Mock<IRackRepository>()
                    .Setup(x => x.Exists(model.RackNumber, model.LocationIdentifier))
                    .Returns(true);

                var validator = mock.Create<RackCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private RackCreateViewModel GetValidSampleModel()
        {
            var output = new RackCreateViewModel()
            {
                LocationIdentifier = "identifier",
                RackNumber = 1
            };

            return output;
        }
    }
}
