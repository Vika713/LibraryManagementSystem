using Autofac.Extras.Moq;
using Common.Constants;
using Common.Resources;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Linq;
using Web.ViewModels.Books;
using Xunit;

namespace LibraryManagementTests.ViewModels.Books
{
    public class ISBNCreateViewModelValidatorTests
    {
        [Fact]
        public void ISBNCreateViewModelValidator_EmptyISBN_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = new ISBNCreateViewModel
                {
                    ISBN = string.Empty
                };

                var validator = mock.Create<ISBNCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void ISBNCreateViewModelValidator_ISBNLengthEquals1_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = new ISBNCreateViewModel
                {
                    ISBN = "1"
                };

                var validator = mock.Create<ISBNCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Theory]
        [InlineData(Consts.ISBNLength1)]
        [InlineData(Consts.ISBNLength2)]
        public void ISBNCreateViewModelValidator_ISBNLengthIsValid_ReturnsTrue(int length)
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                const string chars = "0123456789";
                var str = new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[new Random().Next(s.Length)]).ToArray());

                var model = new ISBNCreateViewModel
                {
                    ISBN = str
                };

                var validator = mock.Create<ISBNCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }
    }
}
