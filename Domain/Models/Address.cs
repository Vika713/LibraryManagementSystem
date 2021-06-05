using Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; protected set; }

        [MaxLength(Consts.MaxDbCharCount)]
        public string StreetAddress { get; protected set; }

        [MaxLength(Consts.MaxDbCharCount)]
        public string City { get; protected set; }

        [MaxLength(Consts.MaxDbCharCount)]
        public string State { get; protected set; }

        [StringLength(Consts.ZipCodeLength)]
        public string ZipCode { get; protected set; }

        [MaxLength(Consts.MaxDbCharCount)]
        public string Country { get; protected set; }

        [Required]
        public Guid PersonId { get; protected set; }

        public Person Person { get; protected set; }

        public Address()
        {
        }

        public Address(string streetAddress, string city, string state, string zipCode, string country)
        {
            Id = Guid.NewGuid();
            StreetAddress = streetAddress;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }

        public void Edit(string streetAddress, string city, string state, string zipCode, string country)
        {
            StreetAddress = streetAddress;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }
    }
}
