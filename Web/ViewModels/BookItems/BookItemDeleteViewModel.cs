using Common.Enumeration;
using Common.Resources;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.BookItems
{
    public class BookItemDeleteViewModel
    {
        public Guid BookItemId { get; set; }

        public Guid BookId { get; set; }

        public string Barcode { get; set; }

        public string ISBN { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PublicationDate)]
        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }

        public double? Price { get; set; }

        public BookFormat? Format { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.DateOfPurchase)]
        [DataType(DataType.Date)]
        public DateTime? DateOfPurchase { get; set; }

        public BookStatus Status { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.RackNumber)]
        public int? RackNumber { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LocationIdentifier)]
        public string LocationIdentifier { get; set; }

        public bool HasOtherBookItems { get; set; }
    }

    public class BookItemDeleteViewModelValidator : AbstractValidator<BookItemDeleteViewModel>
    {
        public BookItemDeleteViewModelValidator()
        {
            RuleFor(bi => bi.Status).NotEqual(BookStatus.Loaned);
        }
    }
}
