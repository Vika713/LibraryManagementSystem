using Common.Enumeration;
using System;

namespace Business.Librarians.DTOs
{
    public class LibrarianStatusChangeDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public LibrarianStatus CurrentStatus { get; set; }
    }
}
