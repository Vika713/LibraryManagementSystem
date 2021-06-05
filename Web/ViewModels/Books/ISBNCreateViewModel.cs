using Common.Constants;
using Common.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Web.ViewModels.Books
{
    public class ISBNCreateViewModel
    {
        public string ISBN { get; set; }
    }

    public class ISBNCreateViewModelValidator : AbstractValidator<ISBNCreateViewModel>
    {
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public ISBNCreateViewModelValidator(IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _localizer = stringLocalizer;

            RuleFor(i => i.ISBN).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(x => x.Length == Consts.ISBNLength1 || x.Length == Consts.ISBNLength2)
                    .WithMessage(_localizer[LocalizationKeys.FormatNotValid]);
        }
    }
}
