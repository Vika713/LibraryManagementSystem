using Autofac.Extras.Moq;
using Common.Constants;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.Cards;
using Data.Repositories.Members;
using Domain.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Linq;
using Web.ViewModels.Cards;
using Xunit;

namespace LibraryManagementTests.ViewModels.Cards
{
    public class CardCreateViewModelValidatorTests
    {
        [Fact]
        public void CardCreateViewModelValidator_ModelIsValid_ReturnsTrue()
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

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(model.MemberId))
                    .Returns(GetSampleMember());

                var validator = mock.Create<CardCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void CardCreateViewModelValidator_NumberLengthIs1_ReturnsFalse()
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
                model.Number = "1";

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(model.MemberId))
                    .Returns(GetSampleMember());

                var validator = mock.Create<CardCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void CardCreateViewModelValidator_NotUniqueNumber_ReturnsFalse()
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

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(model.MemberId))
                    .Returns(GetSampleMember());

                mock.Mock<ICardRepository>()
                    .Setup(x => x.ExistsByNumber(model.Number))
                    .Returns(true);

                var validator = mock.Create<CardCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void CardCreateViewModelValidator_BarcodeIsEmpty_ReturnsFalse()
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

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(model.MemberId))
                    .Returns(GetSampleMember());

                var validator = mock.Create<CardCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void CardCreateViewModelValidator_NotUniqueBarcode_ReturnsFalse()
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

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(model.MemberId))
                    .Returns(GetSampleMember());

                mock.Mock<ICardRepository>()
                    .Setup(x => x.ExistsByBarcode(model.Barcode))
                    .Returns(true);

                var validator = mock.Create<CardCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void CardCreateViewModelValidator_MemberIsNotActive_ReturnsTrue()
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
                var member = GetSampleMember();
                member.ChangeStatus(MemberStatus.Closed);

                mock.Mock<IMemberRepository>()
                    .Setup(x => x.Get(model.MemberId))
                    .Returns(member);

                mock.Mock<ICardRepository>()
                    .Setup(x => x.ExistsByBarcode(model.Barcode))
                    .Returns(true);

                var validator = mock.Create<CardCreateViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private CardCreateViewModel GetValidSampleModel()
        {
            const string chars = "0123456789";

            var output = new CardCreateViewModel()
            {
                Number = new string(Enumerable.Repeat(chars, Consts.CardNumberLength)
                  .Select(s => s[new Random().Next(s.Length)]).ToArray()),
                Barcode = "barcode",
                MemberId = Guid.NewGuid()
            };

            return output;
        }

        private Member GetSampleMember()
        {
            var output = new Member("memberId", DateTime.Today, Guid.NewGuid());

            return output;
        }
    }
}
