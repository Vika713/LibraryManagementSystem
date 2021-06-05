using Common.Enumeration;
using System;

namespace Business.BookItems.DTOs
{
    public class BookItemDeleteDTO
    {
        public Guid BookItemId { get; set; }
        public Guid BookId { get; set; }
        public string Barcode { get; set; }
        public string ISBN { get; set; }
        public DateTime? PublicationDate { get; set; }
        public double? Price { get; set; }
        public BookFormat? Format { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public BookStatus Status { get; set; }
        public int? RackNumber { get; set; }
        public string LocationIdentifier { get; set; }
        public bool HasOtherBookItems { get; set; }
    }
}
