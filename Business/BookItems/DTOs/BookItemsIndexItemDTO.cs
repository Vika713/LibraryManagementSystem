using Common.Enumeration;
using System;
using System.Collections.Generic;

namespace Business.BookItems.DTOs
{
    public class BookItemsIndexItemDTO
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
        public int? NumberOfPages { get; set; }
        public List<string> AuthorsNames { get; set; }
        public DateTime? PublicationDate { get; set; }
        public BookFormat? Format { get; set; }
        public BookStatus? Status { get; set; }
        public int? RackNumber { get; set; }
        public string LocationIdentifier { get; set; }
        public bool CanBeReserved { get; set; }
        public bool IsReserved { get; set; }
    }
}
