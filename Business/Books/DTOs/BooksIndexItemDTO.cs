using System;
using System.Collections.Generic;

namespace Business.Books.DTOs
{
    public class BooksIndexItemDTO
    {
        public Guid BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int NumberOfPages { get; set; }
        public List<string> AuthorsNames { get; set; }
        public List<DateTimeItem> PublicationDates { get; set; }
        public class DateTimeItem
        {
            public DateTime? Value { get; set; }
        }
        public bool? HasBookItems { get; set; }
    }
}
