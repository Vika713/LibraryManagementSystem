using Common.Constants;
using Common.Resources;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.ViewModels.Pagination;

namespace Web.ViewModels.Racks
{
    public class RacksIndexViewModel : PaginationViewModel
    {
        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.RackNumber)]
        public int? NumberFilter { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LocationIdentifier)]
        public string LocationIdFilter { get; set; }

        public List<RackItemViewModel> Index { get; set; }
    }

    public class RacksIndexViewModelValidator : AbstractValidator<RacksIndexViewModel>
    {
        public RacksIndexViewModelValidator()
        {
            RuleFor(r => r.NumberFilter).LessThanOrEqualTo(Consts.MaxIntNumber);
            RuleFor(r => r.LocationIdFilter).MaximumLength(Consts.PersonalCodeLength);
        }
    }
}
