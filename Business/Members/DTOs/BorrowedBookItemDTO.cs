using Common.Enumeration;
using System;
using System.Collections.Generic;

namespace Business.Members.DTOs
{
    public class BorrowedBookItemDTO
    {
        public Guid BookId { get; set; }
        public string Barcode { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public List<string> AuthorsNames { get; set; }
        public BookFormat? Format { get; set; }
        public DateTime? BorrowingDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
