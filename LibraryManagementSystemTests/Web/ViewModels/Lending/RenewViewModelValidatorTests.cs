using Autofac.Extras.Moq;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.BookItems;
using Data.Repositories.Cards;
using Domain.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using Web.ViewModels.Lending;
using Xunit;

namespace LibraryManagementTests.ViewModels.Lending
{
    public class RenewViewModelValidatorTests
    {
        [Fact]
        public void RenewViewModelValidator_ModelIsValid_ReturnsTrue()
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
                var memberId = Guid.NewGuid();

                mock.Mock<ICardRepository>()
                    .Setup(x => x.GetByBarcode(model.CardBarcode))
                    .Returns(GetSampleCard(memberId));

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(GetSampleBorrowedBookItem(memberId));

                var validator = mock.Create<RenewViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void RenewViewModelValidator_CardDoesNotExist_ReturnsFalse()
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
                var memberId = Guid.NewGuid();

                mock.Mock<ICardRepository>()
                    .Setup(x => x.GetByBarcode(model.CardBarcode))
                    .Returns((Card)null);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(GetSampleBorrowedBookItem(memberId));

                var validator = mock.Create<RenewViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void RenewViewModelValidator_BookItemDoesNotExist_ReturnsFalse()
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
                var memberId = Guid.NewGuid();

                mock.Mock<ICardRepository>()
                    .Setup(x => x.GetByBarcode(model.CardBarcode))
                    .Returns(GetSampleCard(memberId));

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns((BookItem)null);

                var validator = mock.Create<RenewViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void RenewViewModelValidator_BookItemLoanedToAnotherMember_ReturnsFalse()
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

                mock.Mock<ICardRepository>()
                    .Setup(x => x.GetByBarcode(model.CardBarcode))
                    .Returns(GetSampleCard(Guid.NewGuid()));

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(GetSampleBorrowedBookItem(Guid.NewGuid()));

                var validator = mock.Create<RenewViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private RenewViewModel GetValidSampleModel()
        {
            var output = new RenewViewModel()
            {
                BookBarcode = "book barcode",
                CardBarcode = "card barcode"
            };

            return output;
        }

        private Card GetSampleCard(Guid memberId)
        {
            var output = new Card(memberId, "number", "barcode", DateTime.Now, true);

            return output;
        }

        private BookItem GetSampleBorrowedBookItem(Guid memberId)
        {
            var output = new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null);
            output.Lend(memberId);

            return output;
        }
    }
}
