using Autofac.Extras.Moq;
using Common.Constants;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.People;
using Domain.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Linq;
using Xunit;
using static Web.Areas.Identity.Pages.Account.RegisterModel;

namespace LibraryManagementTests.Areas.Identity.Pages.Account
{
    public class RegisterInputModelValidatorTests
    {
        [Fact]
        public void InputModelValidator_ModelIsValid_ReturnsTrue()
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

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.ExistsByEmail(model.Email))
                    .Returns(true);

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.GetByEmail(model.Email))
                    .Returns(GetSamplePersonAsMemberAndLibrarian());

                var validator = mock.Create<InputModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void InputModelValidator_EmailNotRegistered_ReturnsFalse()
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

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.ExistsByEmail(model.Email))
                    .Returns(false);

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.GetByEmail(model.Email))
                    .Returns(GetSamplePersonAsMemberAndLibrarian());

                var validator = mock.Create<InputModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void InputModelValidator_PersonIsNotMemberOrLibrarian_ReturnsFalse()
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

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.ExistsByEmail(model.Email))
                    .Returns(true);

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.GetByEmail(model.Email))
                    .Returns(GetSamplePerson());

                var validator = mock.Create<InputModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void InputModelValidator_PersonAccountsAreClosed_ReturnsFalse()
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
                var person = GetSamplePersonAsMemberAndLibrarian();
                person.Member.ChangeStatus(MemberStatus.Closed);
                person.Librarian.ChangeStatus(LibrarianStatus.Closed);

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.ExistsByEmail(model.Email))
                    .Returns(true);

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.GetByEmail(model.Email))
                    .Returns(person);

                var validator = mock.Create<InputModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void InputModelValidator_MemberStatusIsClosed_ReturnsTrue()
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
                var person = GetSamplePersonAsMemberAndLibrarian();
                person.Member.ChangeStatus(MemberStatus.Closed);

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.ExistsByEmail(model.Email))
                    .Returns(true);

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.GetByEmail(model.Email))
                    .Returns(person);

                var validator = mock.Create<InputModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void InputModelValidator_LibrarianStatusIsClosed_ReturnsTrue()
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
                var person = GetSamplePersonAsMemberAndLibrarian();
                person.Librarian.ChangeStatus(LibrarianStatus.Closed);

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.ExistsByEmail(model.Email))
                    .Returns(true);

                mock.Mock<IPersonRepository>()
                    .Setup(x => x.GetByEmail(model.Email))
                    .Returns(person);

                var validator = mock.Create<InputModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        private InputModel GetSampleModel()
        {
            var output = new InputModel()
            {
                Email = "email@email.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            return output;
        }

        private Person GetSamplePersonAsMemberAndLibrarian()
        {
            var output = GetSamplePerson();
            output.CreateMember("memberId", DateTime.Now);
            output.CreateLibrarian("librarianId");

            return output;
        }

        private Person GetSamplePerson()
        {
            const string chars = "0123456789";
            string phone = new string(Enumerable.Repeat(chars, Consts.PhoneLength1)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            var output = new Person("personal", "name", "email", phone, null, null, null, null, null);

            return output;
        }
    }
}
