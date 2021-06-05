using Common.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Lending
{
    public class LendingFineViewModel
    {
        public decimal Fine { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PaidAmount)]
        public decimal PaidAmount { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.BookBarcode)]
        public string BookBarcode { get; set; }
    }

    public class LendingFineViewModelValidator : AbstractValidator<LendingFineViewModel>
    {
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public LendingFineViewModelValidator(IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _localizer = stringLocalizer;

            RuleFor(b => b.PaidAmount).Must(EqualsFine).WithMessage(_localizer[LocalizationKeys.AmountNotValid]);
        }

        private bool EqualsFine(LendingFineViewModel model, decimal paidAmount)
        {
            return paidAmount == model.Fine;
        }
    }
}
