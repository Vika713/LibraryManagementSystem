using Common.Enumeration;
using Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.BookItems
{
    public class BookItemDetailsViewModel
    {
        public Guid BookItemId { get; set; }

        public Guid BookId { get; set; }

        public string Barcode { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PublicationDate)]
        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }

        public double? Price { get; set; }

        public BookFormat? Format { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.DateOfPurchase)]
        [DataType(DataType.Date)]
        public DateTime? DateOfPurchase { get; set; }

        public BookStatus Status { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.BorrowingDate)]
        [DataType(DataType.Date)]
        public DateTime? BorrowingDate { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.DueDate)]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public Guid? BorrowerId { get; set; }

        public Guid? ReserverId { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.RackNumber)]
        public int? RackNumber { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LocationIdentifier)]
        public string LocationIdentifier { get; set; }

        public bool HasOtherBookItems { get; set; }
    }
}
