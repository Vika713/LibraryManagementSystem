using Common.Constants;
using Common.Resources;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.ViewModels.Pagination;

namespace Web.ViewModels.People
{
    public class PeopleIndexViewModel : PaginationViewModel
    {
        public List<PersonItemViewModel> Index { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PersonalCode)]
        public string PersonalCodeFilter { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.Email)]
        public string EmailFilter { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.MemberId)]
        public string MemberCodeFilter { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.CardNumber)]
        public string CardNumberFilter { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LibrarianId)]
        public string LibrarianCodeFilter { get; set; }
    }

    public class PeopleIndexViewModelValidator : AbstractValidator<PeopleIndexViewModel>
    {
        public PeopleIndexViewModelValidator()
        {
            RuleFor(p => p.PersonalCodeFilter).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(p => p.EmailFilter).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(p => p.MemberCodeFilter).MaximumLength(Consts.MemberCodeLength);
            RuleFor(p => p.CardNumberFilter).MaximumLength(Consts.CardNumberLength);
            RuleFor(p => p.LibrarianCodeFilter).MaximumLength(Consts.LibrarianCodeLength);
        }
    }
}
