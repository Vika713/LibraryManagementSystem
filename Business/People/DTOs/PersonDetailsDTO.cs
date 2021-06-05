using System;

namespace Business.People.DTOs
{
    public class PersonDetailsDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? LibrarianId { get; set; }
        public string PersonalCode { get; set; }
        public string Email { get; set; }
        public string MemberCode { get; set; }
        public string CardNumber { get; set; }
        public string LibrarianCode { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
