using Autofac.Extras.Moq;
using Common.Resources;
using Microsoft.Extensions.Localization;
using Moq;
using Web.ViewModels.Lending;
using Xunit;

namespace LibraryManagementTests.ViewModels.Lending
{
    public class LendingFineViewModelValidatorTests
    {
        [Fact]
        public void LendingFineViewModelValidator_PaidAmountEqualsFine_ReturnsTrue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                string key = "Message";
                var localizedString = new LocalizedString(key, key);

                mock.Mock<IStringLocalizer<ValidationMessagesResource>>()
                    .Setup(m => m[It.IsAny<string>()])
                    .Returns(localizedString);

                var model = new LendingFineViewModel()
                {
                    Fine = 1,
                    PaidAmount = 1
                };

                var validator = mock.Create<LendingFineViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.True(result);
            }
        }
    }
}
