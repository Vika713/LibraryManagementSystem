using Common.Constants;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Person
    {
        [Key]
        public Guid Id { get; protected set; }

        [Required]
        [StringLength(Consts.PersonalCodeLength)]
        public string PersonalCode { get; protected set; }

        [Required]
        [MaxLength(Consts.MaxDbCharCount)]
        public string Name { get; protected set; }

        public Address Address { get; protected set; }

        [Required]
        [EmailAddress]
        [MaxLength(Consts.MaxDbCharCount)]
        public string Email { get; protected set; }

        [Required]
        public string Phone { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public Member Member { get; protected set; }

        public Librarian Librarian { get; protected set; }

        public Person()
        {
        }

        public Person(string personalCode, string name, string email, string phone,
            string street, string city, string state, string zipCode, string country)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            PersonalCode = personalCode;
            Name = name;
            Email = email;
            Phone = phone;
            Address = new Address(street, city, state, zipCode, country);

            new PersonValidator().ValidateAndThrow(this);
        }

        public void Edit(string name, string phone,
            string street, string city, string state, string zipCode, string country)
        {
            Name = name;
            Phone = phone;
            Address.Edit(street, city, state, zipCode, country);

            new PersonValidator().ValidateAndThrow(this);
        }

        public void Edit(string email)
        {
            Email = email;
        }

        public void CreateMember(string memberId, DateTime dateOfMembership)
        {
            Member = new Member(memberId, dateOfMembership, Id);
        }

        public void CreateLibrarian(string librarianId)
        {
            Librarian = new Librarian(librarianId, Id);
        }
    }

    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Phone)
                .Must(x => x.Length == Consts.PhoneLength1 || x.Length == Consts.PhoneLength2);
        }
    }
}
