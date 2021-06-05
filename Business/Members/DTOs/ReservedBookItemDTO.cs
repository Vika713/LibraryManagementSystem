using Common.Enumeration;
using System;
using System.Collections.Generic;

namespace Business.Members.DTOs
{
    public class ReservedBookItemDTO
    {
        public Guid BookItemId { get; set; }
        public Guid BookId { get; set; }
        public string Barcode { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public List<string> AuthorsNames { get; set; }
        public BookFormat? Format { get; set; }
    }
}
