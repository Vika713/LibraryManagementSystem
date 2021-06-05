using Common.Constants;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.Cards;
using Data.Repositories.Members;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Cards
{
    public class CardCreateViewModel
    {
        public Guid PersonId { get; set; }

        public Guid MemberId { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.MemberId)]
        public string MemberCode { get; set; }

        public string Number { get; set; }

        public string Barcode { get; set; }
    }

    public class CardCreateViewModelValidator : AbstractValidator<CardCreateViewModel>
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public CardCreateViewModelValidator(
            ICardRepository cardRepository,
            IMemberRepository memberRepository,
            IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _cardRepository = cardRepository;
            _memberRepository = memberRepository;
            _localizer = stringLocalizer;

            RuleFor(c => c.Number).Cascade(CascadeMode.Stop)
                .NotEmpty().Length(Consts.CardNumberLength)
                .Must(UniqueNumber).WithMessage(_localizer[LocalizationKeys.CardNumberExists]);
            RuleFor(c => c.Barcode).Cascade(CascadeMode.Stop)
                .NotEmpty().MaximumLength(Consts.MaxBarcodeLength)
                .Must(UniqueBarcode).WithMessage(_localizer[LocalizationKeys.CardBarcodeExists]);
            RuleFor(c => c.MemberId).Must(MemberIsActive).WithMessage(_localizer[LocalizationKeys.MemberNotActive]);
        }

        private bool UniqueNumber(CardCreateViewModel model, string number)
        {
            return !_cardRepository.ExistsByNumber(number);
        }

        private bool UniqueBarcode(CardCreateViewModel model, string barcode)
        {
            return !_cardRepository.ExistsByBarcode(barcode);
        }

        private bool MemberIsActive(CardCreateViewModel model, Guid id)
        {
            return _memberRepository.Get(id).Status == MemberStatus.Active;
        }
    }
}
