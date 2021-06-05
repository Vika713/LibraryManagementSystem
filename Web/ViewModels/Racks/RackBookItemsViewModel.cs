using Common.Enumeration;
using Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Racks
{
    public class RackBookItemsViewModel
    {
        public Guid BookItemId { get; set; }

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

        public double? Price { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.DateOfPurchase)]
        [DataType(DataType.Date)]
        public DateTime? DateOfPurchase { get; set; }

        public bool Selected { get; set; }
    }
}
