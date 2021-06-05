using Common.Enumeration;
using Common.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Librarians
{
    public class LibrarianStatusChangeViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LibrarianId)]
        public string Code { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.CurrentStatus)]
        public LibrarianStatus CurrentStatus { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.AccountStatus)]
        public LibrarianStatus AccountStatus { get; set; }
    }

    public class LibrarianStatusChangeViewModelValidator : AbstractValidator<LibrarianStatusChangeViewModel>
    {
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public LibrarianStatusChangeViewModelValidator(IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _localizer = stringLocalizer;

            RuleFor(a => new { a.AccountStatus, a.CurrentStatus })
                .Must(a => a.AccountStatus != a.CurrentStatus)
                    .WithMessage(_localizer[LocalizationKeys.NewStatusTheSame]);
        }
    }
}
