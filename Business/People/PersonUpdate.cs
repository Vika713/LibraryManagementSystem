using Business.People.DTOs;
using Common.Constants;
using Data.Repositories.Librarians;
using Data.Repositories.Members;
using Data.Repositories.People;
using Domain.Models;
using System;

namespace Business.People
{
    public class PersonUpdate : IPersonUpdateService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ILibrarianRepository _librarianRepository;

        public PersonUpdate(IPersonRepository personRepository, IMemberRepository memberRepository, ILibrarianRepository librarianRepository)
        {
            _personRepository = personRepository;
            _memberRepository = memberRepository;
            _librarianRepository = librarianRepository;
        }

        public Guid Create(PersonCreateDTO createDTO)
        {
            Person person = new Person(
                createDTO.PersonalCode,
                createDTO.Name,
                createDTO.Email,
                createDTO.Phone,
                createDTO.StreetAddress,
                createDTO.City,
                createDTO.State,
                createDTO.ZipCode,
                createDTO.Country);

            _personRepository.Add(person);
            _personRepository.SaveChanges();

            return person.Id;
        }

        public void Edit(PersonEditDTO editDTO)
        {
            Person person = _personRepository.Get(editDTO.Id);

            person.Edit(
                editDTO.Name,
                editDTO.Phone,
                editDTO.StreetAddress,
                editDTO.City,
                editDTO.State,
                editDTO.ZipCode,
                editDTO.Country);

            _personRepository.Update(person);
            _personRepository.SaveChanges();
        }

        public void AddAsMember(Guid personId)
        {
            DateTime dateOfMembership = DateTime.Today;
            string code = GetCode(Consts.MemberCodePrefix);

            Member member = new Member(code, dateOfMembership, personId);

            _memberRepository.Add(member);
            _memberRepository.SaveChanges();
        }

        public void AddAsLibrarian(Guid personId)
        {
            string code = GetCode(Consts.LibrarianCodePrefix);

            Librarian librarian = new Librarian(code, personId);

            _librarianRepository.Add(librarian);
            _librarianRepository.SaveChanges();
        }

        private string GetCode(string prefix)
        {
            return prefix + DateTime.Today.ToString("yy") + new Random().Next(0, 9999).ToString("D4");
        }
    }
}
