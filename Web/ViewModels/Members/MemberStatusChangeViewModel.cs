using Common.Enumeration;
using Common.Resources;
using Data.Repositories.BookItems;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Web.ViewModels.Members
{
    public class MemberStatusChangeViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.MemberId)]
        public string Code { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.CurrentStatus)]
        public MemberStatus CurrentStatus { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.AccountStatus)]
        public MemberStatus AccountStatus { get; set; }
    }

    public class MemberStatusChangeViewModelValidator : AbstractValidator<MemberStatusChangeViewModel>
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public MemberStatusChangeViewModelValidator(IBookItemRepository bookItemRepository,
            IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _bookItemRepository = bookItemRepository;
            _localizer = stringLocalizer;

            RuleFor(a => new { a.AccountStatus, a.CurrentStatus })
                .Must(a => a.AccountStatus != a.CurrentStatus)
                    .WithMessage(_localizer[LocalizationKeys.NewStatusTheSame]);

            RuleFor(a => a.AccountStatus).Cascade(CascadeMode.Stop)
                .Must(Conditions).WithMessage(_localizer[LocalizationKeys.HasBorrowedBookItems]);
        }

        private bool Conditions(MemberStatusChangeViewModel model, MemberStatus newStatus)
        {
            if (newStatus == MemberStatus.Closed)
                return !_bookItemRepository.GetByBorrowedMemberId(model.Id).Any();
            else
                return true;
        }
    }
}
