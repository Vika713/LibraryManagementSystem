using Autofac.Extras.Moq;
using Common.Constants;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.BookItems;
using Data.Repositories.Cards;
using Data.Repositories.Members;
using Domain.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using Web.ViewModels.Lending;
using Xunit;

namespace LibraryManagementTests.ViewModels.Lending
{
    public class CheckOutViewModelValidatorTests
    {
        [Fact]
        public void CheckOutViewModelValidator_ModelIsValid_ReturnsTrue()
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
                    .Returns(GetSampleCard());

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(GetSampleBookItem());

                mock.Mock<IMemberRepository>()
                   .Setup(x => x.GetByCardBarcode(model.CardBarcode))
                   .Returns(GetSampleMember());

                var validator = mock.Create<CheckOutViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void CheckOutViewModelValidator_CardDoesNotExist_ReturnsFalse()
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
                    .Returns((Card)null);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(GetSampleBookItem());

                mock.Mock<IMemberRepository>()
                   .Setup(x => x.GetByCardBarcode(model.CardBarcode))
                   .Returns(GetSampleMember());

                var validator = mock.Create<CheckOutViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void CheckOutViewModelValidator_CardIsNotActive_ReturnsFalse()
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
                var card = GetSampleCard();
                card.MakeInactive();

                mock.Mock<ICardRepository>()
                    .Setup(x => x.GetByBarcode(model.CardBarcode))
                    .Returns(card);

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(GetSampleBookItem());

                mock.Mock<IMemberRepository>()
                   .Setup(x => x.GetByCardBarcode(model.CardBarcode))
                   .Returns(GetSampleMember());

                var validator = mock.Create<CheckOutViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void CheckOutViewModelValidator_MemberCannotBorrow_ReturnsFalse()
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
                    .Returns(GetSampleCard());

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(GetSampleBookItem());

                mock.Mock<IMemberRepository>()
                   .Setup(x => x.GetByCardBarcode(model.CardBarcode))
                   .Returns(GetSampleMember());

                mock.Mock<IBookItemRepository>()
                   .Setup(x => x.GetByBorrowedMemberId(It.IsAny<Guid>()))
                   .Returns(GetSampleBookItems());

                var validator = mock.Create<CheckOutViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void CheckOutViewModelValidator_BookItemDoesNotExist_ReturnsFalse()
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
                    .Returns(GetSampleCard());

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns((BookItem)null);

                mock.Mock<IMemberRepository>()
                   .Setup(x => x.GetByCardBarcode(model.CardBarcode))
                   .Returns(GetSampleMember());

                var validator = mock.Create<CheckOutViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void CheckOutViewModelValidator_BookItemIsNotAvailable_ReturnsFalse()
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
                var bookItem = GetSampleBookItem();
                bookItem.SetStatus(BookStatus.Loaned);

                mock.Mock<ICardRepository>()
                    .Setup(x => x.GetByBarcode(model.CardBarcode))
                    .Returns(GetSampleCard());

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(bookItem);

                mock.Mock<IMemberRepository>()
                   .Setup(x => x.GetByCardBarcode(model.CardBarcode))
                   .Returns(GetSampleMember());

                var validator = mock.Create<CheckOutViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void CheckOutViewModelValidator_BookItemIsReservedToAnotherMember_ReturnsFalse()
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
                var bookItem = GetSampleBookItem();
                bookItem.Reserve(Guid.NewGuid());

                mock.Mock<ICardRepository>()
                    .Setup(x => x.GetByBarcode(model.CardBarcode))
                    .Returns(GetSampleCard());

                mock.Mock<IBookItemRepository>()
                    .Setup(x => x.GetByBarcode(model.BookBarcode))
                    .Returns(bookItem);

                mock.Mock<IMemberRepository>()
                   .Setup(x => x.GetByCardBarcode(model.CardBarcode))
                   .Returns(GetSampleMember());

                var validator = mock.Create<CheckOutViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private CheckOutViewModel GetValidSampleModel()
        {
            var output = new CheckOutViewModel()
            {
                BookBarcode = "book barcode",
                CardBarcode = "card barcode"
            };

            return output;
        }

        private Card GetSampleCard()
        {
            var output = new Card(Guid.NewGuid(), "number", "barcode", DateTime.Now, true);

            return output;
        }

        private Member GetSampleMember()
        {
            var output = new Member("id", DateTime.Now, Guid.NewGuid());

            return output;
        }

        private BookItem GetSampleBookItem()
        {
            var output = new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null);

            return output;
        }

        private List<BookItem> GetSampleBookItems()
        {
            var output = new List<BookItem>();

            for (int i = 0; i < Consts.MaxBooksPerMember; i++)
            {
                output.Add(new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null));
            };

            return output;
        }
    }
}
