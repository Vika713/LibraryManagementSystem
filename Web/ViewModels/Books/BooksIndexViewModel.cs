using Common.Constants;
using Common.Resources;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.ViewModels.Pagination;

namespace Web.ViewModels.Books
{
    public class BooksIndexViewModel : PaginationViewModel
    {
        public List<BookItemViewModel> Index { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.Title)]
        public string TitleFilter { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.Author)]
        public string AuthorFilter { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.Subject)]
        public string SubjectFilter { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PublicationDate)]
        public string PublicationDateFilter { get; set; }
    }

    public class BooksIndexViewModelValidator : AbstractValidator<BooksIndexViewModel>
    {
        public BooksIndexViewModelValidator()
        {
            RuleFor(b => b.TitleFilter).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.AuthorFilter).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.SubjectFilter).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.PublicationDateFilter).MaximumLength(Consts.MaxDbCharCount);
        }
    }
}
