using Common.Enumeration;
using Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.BookItems
{
    public class BookItemItemViewModel
    {
        public Guid BookItemId { get; set; }

        public Guid BookId { get; set; }

        public Guid? RackId { get; set; }

        public string Barcode { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Publisher { get; set; }

        public string Language { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.NumberOfPages)]
        public int? NumberOfPages { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.Authors)]
        public List<string> AuthorsNames { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PublicationDate)]
        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }

        public BookFormat? Format { get; set; }

        public BookStatus? Status { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.RackNumber)]
        public int? RackNumber { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LocationIdentifier)]
        public string LocationIdentifier { get; set; }

        public bool CanBeReserved { get; set; }

        public bool IsReserved { get; set; }
    }
}
