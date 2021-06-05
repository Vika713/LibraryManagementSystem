using Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Books
{
    public class BookItemViewModel
    {
        public Guid BookId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Publisher { get; set; }

        public string Language { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.NumberOfPages)]
        public int NumberOfPages { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.Authors)]
        public List<string> AuthorsNames { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PublicationDates)]
        public List<DateTimeItem> PublicationDates { get; set; }
        public class DateTimeItem
        {
            [DataType(DataType.Date)]
            public DateTime? Value { get; set; }
        }

        public bool? HasBookItems { get; set; }
    }
}
