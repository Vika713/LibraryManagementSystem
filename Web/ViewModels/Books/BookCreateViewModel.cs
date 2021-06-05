using Common.Constants;
using Common.Resources;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Books
{
    public class BookCreateViewModel
    {
        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Publisher { get; set; }

        public string Language { get; set; }
        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.NumberOfPages)]

        public int NumberOfPages { get; set; }
        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.Authors)]

        public List<AuthorName> AuthorsNames { get; set; }

        public BookCreateViewModel()
        {
            AuthorsNames = new List<AuthorName>();
        }
    }

    public class BookCreateViewModelValidator : AbstractValidator<BookCreateViewModel>
    {
        public BookCreateViewModelValidator()
        {
            RuleFor(b => b.Title).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.Subject).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.Publisher).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.Language).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.NumberOfPages).LessThanOrEqualTo(Consts.MaxIntNumber);
        }
    }
}
