using Common.Constants;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.BookItems;
using Data.Repositories.Cards;
using Data.Repositories.Members;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Web.ViewModels.Lending
{
    public class CheckOutViewModel
    {
        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.CardBarcode)]
        public string CardBarcode { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.BookBarcode)]
        public string BookBarcode { get; set; }
    }

    public class CheckOutViewModelValidator : AbstractValidator<CheckOutViewModel>
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public CheckOutViewModelValidator(IBookItemRepository bookItemRepository,
            IMemberRepository memberRepository,
            ICardRepository cardRepository,
            IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _memberRepository = memberRepository;
            _cardRepository = cardRepository;
            _bookItemRepository = bookItemRepository;
            _localizer = stringLocalizer;

            RuleFor(s => s.CardBarcode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(Consts.MaxBarcodeLength)
                .Must(CardExists).WithMessage(_localizer[LocalizationKeys.CardNotFound])
                .Must(CardIsActive).WithMessage(_localizer[LocalizationKeys.CardNotActive])
                .Must(MemberCanBorrow).WithMessage(_localizer[LocalizationKeys.MemberCannotBorrow]);

            RuleFor(s => s.BookBarcode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(Consts.MaxBarcodeLength)
                .Must(BookItemExists).WithMessage(_localizer[LocalizationKeys.BookItemNotFound])
                .Must(BookCanBeIssued).WithMessage(_localizer[LocalizationKeys.BookCannotBeIssued]);
        }

        private bool CardExists(CheckOutViewModel model, string barcode)
        {
            Card card = _cardRepository.GetByBarcode(barcode);

            return card != null;
        }

        private bool BookItemExists(CheckOutViewModel model, string barcode)
        {
            BookItem bookItem = _bookItemRepository.GetByBarcode(barcode);

            return bookItem != null;
        }

        private bool CardIsActive(CheckOutViewModel model, string barcode)
        {
            Card card = _cardRepository.GetByBarcode(barcode);

            return card.IsActive;
        }

        private bool MemberCanBorrow(CheckOutViewModel model, string cardBarcode)
        {
            Member member = _memberRepository.GetByCardBarcode(cardBarcode);

            return _bookItemRepository.GetByBorrowedMemberId(member.Id).Count() < Consts.MaxBooksPerMember;
        }

        private bool BookCanBeIssued(CheckOutViewModel model, string bookBarcode)
        {
            BookItem bookItem = _bookItemRepository.GetByBarcode(bookBarcode);

            Member member = _memberRepository.GetByCardBarcode(model.CardBarcode);

            return bookItem.Status == BookStatus.Available ||
                (bookItem.Status == BookStatus.Reserved && bookItem.ReservedMemberId == member.Id);
        }
    }
}
