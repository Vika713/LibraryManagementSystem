using Autofac.Extras.Moq;
using Common.Enumeration;
using Web.ViewModels.BookItems;
using Xunit;

namespace LibraryManagementTests.ViewModels.BookItems
{
    public class BookItemDeleteViewModelValidatorTests
    {
        [Fact]
        public void BookItemDeleteViewModelValidator_StatusIsLoaned_ReturnsFalse()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var model = new BookItemDeleteViewModel()
                {
                    Status = BookStatus.Loaned
                };

                var validator = mock.Create<BookItemDeleteViewModelValidator>();

                //Act
                var result = validator.Validate(model).IsValid;

                //Assert
                Assert.False(result);
            }
        }
    }
}
