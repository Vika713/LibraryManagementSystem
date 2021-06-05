using Common.Enumeration;
using Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Members
{
    public class ReservedBookItemViewModel
    {
        public Guid BookItemId { get; set; }

        public string Barcode { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.Authors)]
        public List<string> AuthorsNames { get; set; }

        public BookFormat? Format { get; set; }
    }
}
