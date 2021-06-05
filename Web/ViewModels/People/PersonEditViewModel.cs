using Common.Constants;
using Common.Resources;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.People
{
    public class PersonEditViewModel
    {
        public Guid Id { get; set; }

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

        public bool IsMember { get; set; }

        public bool IsLibrarian { get; set; }

        public bool Member { get; set; }

        public bool Librarian { get; set; }
    }

    public class PersonEditViewModelValidator : AbstractValidator<PersonEditViewModel>
    {
        public PersonEditViewModelValidator()
        {
            RuleFor(p => p.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().MaximumLength(Consts.MaxDbCharCount);
            RuleFor(p => p.Phone).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(x => x.Length == Consts.PhoneLength1 || x.Length == Consts.PhoneLength2);
            RuleFor(a => a.StreetAddress).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(a => a.City).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(a => a.State).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(a => a.Country).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(a => a.ZipCode).Length(Consts.ZipCodeLength);
        }
    }
}
