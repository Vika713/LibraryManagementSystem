using Common.Enumeration;
using System;
using System.Collections.Generic;

namespace Business.Racks.DTOs
{
    public class RackBookItemsDTO
    {
        public Guid BookItemId { get; set; }
        public Guid BookId { get; set; }
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
        public double? Price { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public bool Selected { get; set; }
    }
}
