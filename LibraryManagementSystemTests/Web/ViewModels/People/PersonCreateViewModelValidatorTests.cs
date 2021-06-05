using Autofac.Extras.Moq;
using Common.Constants;
using Common.Resources;
using Data.Repositories.People;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Linq;
using Web.ViewModels.People;
using Xunit;

namespace LibraryManagementTests.ViewModels.People
{
    public class PersonCreateViewModelValidatorTests
    {
        [Fact]
        public void PersonCreateViewModelValidator_ModelIsValid_ReturnsTrue()
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

                var validator = mock.Create<PersonCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void PersonCreateViewModelValidator_PersonalCodeIsEmpty_ReturnsFalse()
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
                model.PersonalCode = string.Empty;

                var validator = mock.Create<PersonCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void PersonCreateViewModelValidator_PersonalCodeLengthIs1_ReturnsFalse()
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
                model.PersonalCode = "1";

                var validator = mock.Create<PersonCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void PersonCreateViewModelValidator_NotUniquePersonalCode_ReturnsFalse()
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

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.ExistsByPersonalCode(model.PersonalCode))
                    .Returns(true);

                var validator = mock.Create<PersonCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void PersonCreateViewModelValidator_NameIsEmpty_ReturnsFalse()
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

                var validator = mock.Create<PersonCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void PersonCreateViewModelValidator_EmailWrongFormat_ReturnsFalse()
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
                model.Email = "email";

                var validator = mock.Create<PersonCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void PersonCreateViewModelValidator_NotUniqueEmail_ReturnsFalse()
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

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.ExistsByEmail(model.Email))
                    .Returns(true);

                var validator = mock.Create<PersonCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void PersonCreateViewModelValidator_PhoneLengthIs1_ReturnsFalse()
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

                var validator = mock.Create<PersonCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Theory]
        [InlineData(Consts.PhoneLength1)]
        [InlineData(Consts.PhoneLength2)]
        public void PersonCreateViewModelValidator_PhoneLengthIsValid_ReturnsTrue(int length)
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

                var validator = mock.Create<PersonCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void PersonCreateViewModelValidator_ZipCodeLengthIs1_ReturnsFalse()
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

                var validator = mock.Create<PersonCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private PersonCreateViewModel GetValidSampleModel()
        {
            const string chars = "0123456789";

            var output = GetValidSampleModelWithoutPhone();
            output.Phone = new string(Enumerable.Repeat(chars, Consts.PhoneLength1)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());

            return output;
        }

        private PersonCreateViewModel GetValidSampleModelWithoutPhone()
        {
            const string chars = "0123456789";

            var output = new PersonCreateViewModel()
            {
                PersonalCode = new string(Enumerable.Repeat(chars, Consts.PersonalCodeLength)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray()),
                Name = "name",
                Email = "email@email.com",
                ZipCode = new string(Enumerable.Repeat(chars, Consts.ZipCodeLength)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray())
            };

            return output;
        }
    }
}
