using Autofac.Extras.Moq;
using Common.Constants;
using Common.Resources;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Linq;
using Web.ViewModels.People;
using Xunit;

namespace LibraryManagementTests.ViewModels.People
{
    public class PersonEditViewModelValidatorTests
    {
        [Fact]
        public void PersonEditViewModelValidator_ModelIsValid_ReturnsTrue()
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

                var validator = mock.Create<PersonEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void PersonEditViewModelValidator_NameIsEmpty_ReturnsFalse()
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
                model.Name = string.Empty;

                var validator = mock.Create<PersonEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void PersonEditViewModelValidator_PhoneLengthIs1_ReturnsFalse()
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
                model.Phone = "1";

                var validator = mock.Create<PersonEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Theory]
        [InlineData(Consts.PhoneLength1)]
        [InlineData(Consts.PhoneLength2)]
        public void PersonEditViewModelValidator_PhoneLengthIsValid_ReturnsTrue(int length)
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);


                var model = GetValidSampleModelWithoutPhone();
                const string chars = "0123456789";
                model.Phone = new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[new Random().Next(s.Length)]).ToArray()); ;

                var validator = mock.Create<PersonEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void PersonEditViewModelValidator_ZipCodeLengthIs1_ReturnsFalse()
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
                model.ZipCode = "1";

                var validator = mock.Create<PersonEditViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private PersonEditViewModel GetValidSampleModel()
        {
            const string chars = "0123456789";

            var output = GetValidSampleModelWithoutPhone();
            output.Phone = new string(Enumerable.Repeat(chars, Consts.PhoneLength1)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());

            return output;
        }

        private PersonEditViewModel GetValidSampleModelWithoutPhone()
        {
            const string chars = "0123456789";

            var output = new PersonEditViewModel()
            {
                Name = "name",
                ZipCode = new string(Enumerable.Repeat(chars, Consts.ZipCodeLength)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray())
            };

            return output;
        }
    }
}
