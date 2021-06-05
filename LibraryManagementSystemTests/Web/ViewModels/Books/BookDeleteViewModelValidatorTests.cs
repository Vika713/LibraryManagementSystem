using Autofac.Extras.Moq;
using Common.Resources;
using Data.Repositories.Books;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using Web.ViewModels.Books;
using Xunit;

namespace LibraryManagementTests.ViewModels.Books
{
    public class BookDeleteViewModelValidatorTests
    {
        [Fact]
        public void BookDeleteViewModelValidator_ModelIsValid_ReturnsTrue()
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

                var validator = mock.Create<BookDeleteViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void BookDeleteViewModelValidator_HasBookItems_ReturnsFalse()
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

                mock.Mock<IBookRepository>()
                    .Setup(x => x.HasBookItems(model.BookId))
                    .Returns(true);

                var validator = mock.Create<BookDeleteViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private BookDeleteViewModel GetSampleModel()
        {
            var output = new BookDeleteViewModel()
            {
                BookId = Guid.NewGuid()
            };

            return output;
        }
    }
}
