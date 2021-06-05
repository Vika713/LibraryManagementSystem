using Autofac.Extras.Moq;
using Common.Enumeration;
using Common.Resources;
using Microsoft.Extensions.Localization;
using Moq;
using Web.ViewModels.Librarians;
using Xunit;

namespace LibraryManagementTests.ViewModels.Librarians
{
    public class LibrarianStatusChangeViewModelValidatorTests
    {
        [Fact]
        public void LibrarianStatusChangeViewModelValidator_ModelIsValid_ReturnsTrue()
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

                var validator = mock.Create<LibrarianStatusChangeViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void LibrarianStatusChangeViewModelValidator_CurrentAndNewStatusAreTheSame_ReturnsFalse()
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
                model.AccountStatus = LibrarianStatus.Active;

                var validator = mock.Create<LibrarianStatusChangeViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private LibrarianStatusChangeViewModel GetValidSampleModel()
        {
            var output = new LibrarianStatusChangeViewModel()
            {
                CurrentStatus = LibrarianStatus.Active,
                AccountStatus = LibrarianStatus.Closed
            };

            return output;
        }
    }
}
