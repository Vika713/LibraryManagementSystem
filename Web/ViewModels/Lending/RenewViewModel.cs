using Common.Constants;
using Common.Resources;
using Data.Repositories.BookItems;
using Data.Repositories.Cards;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Lending
{
    public class RenewViewModel
    {
        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.CardBarcode)]
        public string CardBarcode { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.BookBarcode)]
        public string BookBarcode { get; set; }
    }

    public class RenewViewModelValidator : AbstractValidator<RenewViewModel>
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public RenewViewModelValidator(IBookItemRepository bookItemRepository, ICardRepository cardRepository, IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _cardRepository = cardRepository;
            _bookItemRepository = bookItemRepository;
            _localizer = stringLocalizer;

            RuleFor(s => s.CardBarcode)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(Consts.MaxBarcodeLength)
                .Must(CardExists).WithMessage(_localizer[LocalizationKeys.CardNotFound]);

            RuleFor(s => s.BookBarcode)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(Consts.MaxBarcodeLength)
                .Must(BookItemExists).WithMessage(_localizer[LocalizationKeys.BookItemNotFound]);

            RuleFor(s => new { s.BookBarcode, s.CardBarcode })
                .Must(BookIsLoanedToMember).WithMessage(_localizer[LocalizationKeys.BookNotLoanedToUser]);
        }

        private bool BookItemExists(RenewViewModel model, string barcode)
        {
            BookItem bookItem = _bookItemRepository.GetByBarcode(barcode);

            return bookItem != null;
        }

        private bool CardExists(RenewViewModel model, string barcode)
        {
            Card card = _cardRepository.GetByBarcode(barcode);

            return card != null;
        }

        private bool BookIsLoanedToMember(RenewViewModel model, dynamic barcodes)
        {
            Guid? borrowerId = _bookItemRepository.GetByBarcode(barcodes.BookBarcode)?.BorrowedMemberId;

            Guid? userId = _cardRepository.GetByBarcode(barcodes.CardBarcode)?.MemberId;

            return (borrowerId == userId && userId != null);
        }
    }
}
