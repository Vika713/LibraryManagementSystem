using Common.Constants;
using Common.Resources;
using Data.Repositories.Racks;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Racks
{
    public class RackCreateViewModel
    {
        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.RackNumber)]
        public int RackNumber { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LocationIdentifier)]
        public string LocationIdentifier { get; set; }
    }

    public class RackCreateViewModelValidator : AbstractValidator<RackCreateViewModel>
    {
        private readonly IRackRepository _rackRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public RackCreateViewModelValidator(IRackRepository rackRepository, IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _rackRepository = rackRepository;
            _localizer = stringLocalizer;

            RuleFor(r => new { r.RackNumber, r.LocationIdentifier })
                .Must(UniqueRack).WithMessage(_localizer[LocalizationKeys.RackExists]);
            RuleFor(r => r.LocationIdentifier).Cascade(CascadeMode.Stop)
                .NotEmpty().MaximumLength(Consts.MaxDbCharCount);
            RuleFor(r => r.RackNumber).Cascade(CascadeMode.Stop)
                .NotEmpty().LessThanOrEqualTo(Consts.MaxIntNumber);
        }

        private bool UniqueRack(RackCreateViewModel model, dynamic rack)
        {
            return !_rackRepository.Exists(rack.RackNumber, rack.LocationIdentifier);
        }
    }
}
