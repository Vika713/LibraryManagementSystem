using Autofac.Extras.Moq;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.BookItems;
using Domain.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using Web.ViewModels.Members;
using Xunit;

namespace LibraryManagementTests.ViewModels.Members
{
    public class MemberStatusChangeViewModelValidatorTests
    {
        [Fact]
        public void MemberStatusChangeViewModelValidator_ModelIsValid_ReturnsTrue()
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

                var validator = mock.Create<MemberStatusChangeViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void MemberStatusChangeViewModelValidator_CurrentAndNewStatusAreTheSame_ReturnsFalse()
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
                model.AccountStatus = MemberStatus.Active;

                var validator = mock.Create<MemberStatusChangeViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void MemberStatusChangeViewModelValidator_NewStatusIsClosed_WithBorrowedBookItems_ReturnsFalse()
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
                model.AccountStatus = MemberStatus.Closed;

                mock.Mock<IBookItemRepository>()
                   .Setup(x => x.GetByBorrowedMemberId(model.Id))
                   .Returns(GetSampleBookItems());

                var validator = mock.Create<MemberStatusChangeViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }

        private MemberStatusChangeViewModel GetValidSampleModel()
        {
            var output = new MemberStatusChangeViewModel()
            {
                Id = Guid.NewGuid(),
                CurrentStatus = MemberStatus.Active,
                AccountStatus = MemberStatus.Closed
            };

            return output;
        }

        private List<BookItem> GetSampleBookItems()
        {
            var output = new List<BookItem>()
            {
                new BookItem(Guid.NewGuid(), "barcode", null, null, BookFormat.Hardcover, null, null)
            };

            return output;
        }
    }
}
