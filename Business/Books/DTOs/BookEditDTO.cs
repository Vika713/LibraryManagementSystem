using System;
using System.Collections.Generic;

namespace Business.Books.DTOs
{
    public class BookEditDTO
    {
        public Guid BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public int NumberOfPages { get; set; }
        public List<AuthorNameDTO> AuthorsNames { get; set; }
    }
}
