using Common.Constants;
using Common.Resources;
using Data.Repositories.People;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.People
{
    public class PersonCreateViewModel
    {
        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PersonalCode)]
        public string PersonalCode { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.StreetAddress)]
        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.ZipCode)]
        public string ZipCode { get; set; }

        public string Country { get; set; }

        public bool Librarian { get; set; }

        public bool Member { get; set; }
    }

    public class PersonCreateViewModelValidator : AbstractValidator<PersonCreateViewModel>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public PersonCreateViewModelValidator(IPersonRepository personRepository, IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _personRepository = personRepository;
            _localizer = stringLocalizer;

            RuleFor(p => p.PersonalCode).Cascade(CascadeMode.Stop)
                .NotEmpty().Length(Consts.PersonalCodeLength)
                .Must(UniquePersonalCode).WithMessage(_localizer[LocalizationKeys.PersonalCodeExists]);
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().EmailAddress()
                .Must(UniqueEmail).WithMessage(_localizer[LocalizationKeys.EmailExists]);
            RuleFor(p => p.Phone).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(x => x.Length == Consts.PhoneLength1 || x.Length == Consts.PhoneLength2)
                    .WithMessage(_localizer[LocalizationKeys.FormatNotValid]);
            RuleFor(a => a.StreetAddress).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(a => a.City).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(a => a.State).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(a => a.Country).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(a => a.ZipCode).Length(Consts.ZipCodeLength);
        }

        private bool UniquePersonalCode(PersonCreateViewModel model, string personalCode)
        {
            return !_personRepository.ExistsByPersonalCode(personalCode);
        }

        private bool UniqueEmail(PersonCreateViewModel model, string email)
        {
            return !_personRepository.ExistsByEmail(email);
        }
    }
}
