using Autofac.Extras.Moq;
using Common.Constants;
using Common.Resources;
using Data.Repositories.Books;
using Domain.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Linq;
using Web.ViewModels.Books;
using Xunit;

namespace LibraryManagementTests.ViewModels.Books
{
    public class BookEditViewModelValidatorTests
    {
        [Fact]
        public void BookEditViewModelValidator_EmptyISBN_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = new BookEditViewModel
                {
                    ISBN = string.Empty
                };

                var validator = mock.Create<BookEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void BookEditViewModelValidator_ISBNLengthEquals1_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = new BookEditViewModel
                {
                    ISBN = "1"
                };

                var validator = mock.Create<BookEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Theory]
        [InlineData(Consts.ISBNLength1)]
        [InlineData(Consts.ISBNLength2)]
        public void BookEditViewModelValidator_ISBNLengthIsValid_ReturnsTrue(int length)
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

                var model = new BookEditViewModel
                {
                    ISBN = str
                };

                var validator = mock.Create<BookEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void BookEditViewModelValidator_ISBNNotUnique_ReturnsFalse()
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

                mock.Mock<IBookRepository>()
                    .Setup(x => x.GetByISBN(model.ISBN))
                    .Returns(new Book());

                var validator = mock.Create<BookEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private BookEditViewModel GetValidSampleModel()
        {
            const string chars = "0123456789";

            var output = new BookEditViewModel()
            {
                BookId = Guid.NewGuid(),
                ISBN = new string(Enumerable.Repeat(chars, Consts.ISBNLength1)
                  .Select(s => s[new Random().Next(s.Length)]).ToArray())
            };

            return output;
        }
    }
}
