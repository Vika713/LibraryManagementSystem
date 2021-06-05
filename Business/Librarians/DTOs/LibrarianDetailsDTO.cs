using Common.Enumeration;
using System;

namespace Business.Librarians.DTOs
{
    public class LibrarianDetailsDTO
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string PersonalCode { get; set; }
        public string Code { get; set; }
        public LibrarianStatus? AccountStatus { get; set; }
    }
}
