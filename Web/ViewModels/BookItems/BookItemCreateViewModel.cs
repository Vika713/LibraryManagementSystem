using Common.Constants;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.BookItems;
using Data.Repositories.Racks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.BookItems
{
    public class BookItemCreateViewModel
    {
        public Guid BookId { get; set; }

        public string Barcode { get; set; }

        public string ISBN { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PublicationDate)]
        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }

        public double? Price { get; set; }

        public BookFormat Format { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.DateOfPurchase)]
        [DataType(DataType.Date)]
        public DateTime? DateOfPurchase { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.RackNumber)]
        public int? RackNumber { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LocationIdentifier)]
        public string LocationIdentifier { get; set; }

        public List<SelectListItem> RackNumbers { get; set; }

        public List<SelectListItem> LocationIdentifiers { get; set; }

        public bool HasOtherBookItems { get; set; }
    }

    public class BookItemCreateViewModelValidator : AbstractValidator<BookItemCreateViewModel>
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IRackRepository _rackRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public BookItemCreateViewModelValidator(IBookItemRepository bookItemRepository, IRackRepository rackRepository,
            IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _bookItemRepository = bookItemRepository;
            _rackRepository = rackRepository;
            _localizer = stringLocalizer;

            RuleFor(bi => bi.Barcode).Cascade(CascadeMode.Stop)
                .NotEmpty().MaximumLength(Consts.MaxBarcodeLength)
                .Must(UniqueBarcode).WithMessage(_localizer[LocalizationKeys.BookItemBarcodeExists]);
            RuleFor(bi => bi.Price).LessThanOrEqualTo(Consts.MaxDoubleNumber);
            RuleFor(bi => new { bi.RackNumber, bi.LocationIdentifier })
                .Must(RackExists).WithMessage(_localizer[LocalizationKeys.RackDoesNotExist]);
        }

        private bool UniqueBarcode(BookItemCreateViewModel model, string barcode)
        {
            return !_bookItemRepository.ExistsByBarcode(barcode);
        }

        private bool RackExists(BookItemCreateViewModel model, dynamic rack)
        {
            if (rack.RackNumber == null && rack.LocationIdentifier == null)
                return true;
            else
                return _rackRepository.Exists((int)rack.RackNumber, rack.LocationIdentifier);
        }
    }
}
