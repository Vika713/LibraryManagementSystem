using System;

namespace Business.People.DTOs
{
    public class PeopleIndexItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? LibrarianId { get; set; }
        public string Email { get; set; }
        public string MemberCode { get; set; }
        public string CardNumber { get; set; }
        public string LibrarianCode { get; set; }
    }
}
