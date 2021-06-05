using Common.Constants;
using Common.Resources;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.ViewModels.Pagination;

namespace Web.ViewModels.BookItems
{
    public class BookItemsIndexViewModel : PaginationViewModel
    {
        public List<BookItemItemViewModel> Index { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.ISBN)]
        public string ISBNFilter { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.Barcode)]
        public string BarcodeFilter { get; set; }
    }

    public class BookItemsIndexViewModelValidator : AbstractValidator<BookItemsIndexViewModel>
    {
        public BookItemsIndexViewModelValidator()
        {
            RuleFor(bi => bi.ISBNFilter).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(bi => bi.BarcodeFilter).MaximumLength(Consts.MaxDbCharCount);
        }
    }
}
