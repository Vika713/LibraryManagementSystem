using System;

namespace Business.People.DTOs
{
    public class PersonEditDTO
    {
        public Guid Id { get; set; }
        public string PersonalCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public bool IsMember { get; set; }
        public bool IsLibrarian { get; set; }
    }
}
