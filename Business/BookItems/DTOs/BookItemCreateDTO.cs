using Common.Enumeration;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Business.BookItems.DTOs
{
    public class BookItemCreateDTO
    {
        public string ISBN { get; set; }
        public Guid BookId { get; set; }
        public string Barcode { get; set; }
        public DateTime? PublicationDate { get; set; }
        public double? Price { get; set; }
        public BookFormat Format { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public int? RackNumber { get; set; }
        public string LocationIdentifier { get; set; }
        public List<SelectListItem> RackNumbers { get; set; }
        public List<SelectListItem> LocationIdentifiers { get; set; }
        public bool HasOtherBookItems { get; set; }
    }
}
