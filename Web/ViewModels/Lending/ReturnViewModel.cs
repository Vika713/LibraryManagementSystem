using Common.Constants;
using Common.Resources;
using Data.Repositories.BookItems;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Lending
{
    public class ReturnViewModel
    {
        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.BookBarcode)]
        public string BookBarcode { get; set; }
    }

    public class ReturnViewModelValidator : AbstractValidator<ReturnViewModel>
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public ReturnViewModelValidator(IBookItemRepository bookItemRepository, IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _bookItemRepository = bookItemRepository;
            _localizer = stringLocalizer;

            RuleFor(b => b.BookBarcode).Cascade(CascadeMode.Stop)
                .MaximumLength(Consts.MaxBarcodeLength)
                .Must(BookItemExists).WithMessage(_localizer[LocalizationKeys.BookItemNotFound])
                .Must(BookItemIsLoaned).WithMessage(_localizer[LocalizationKeys.BookCannotBeReturned]);
        }

        private bool BookItemExists(ReturnViewModel model, string barcode)
        {
            BookItem bookItem = _bookItemRepository.GetByBarcode(barcode);

            return bookItem != null;
        }

        private bool BookItemIsLoaned(ReturnViewModel model, string barcode)
        {
            BookItem bookItem = _bookItemRepository.GetByBarcode(barcode);

            return bookItem.BorrowedMemberId != null;
        }
    }
}
