using Business.People.DTOs;
using Data.Repositories.People;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.People
{
    public class PersonQueries : IPersonQueriesService
    {
        private readonly IPersonRepository _personRepository;

        public PersonQueries(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public List<PeopleIndexItemDTO> GetIndexItems(PeopleFilterDTO filter, int pageIndex, int itemsCount)
        {
            IEnumerable<Person> paginatedPeople = _personRepository.GetFilteredAndPaginated(
                (pageIndex - 1) * itemsCount, itemsCount,
                filter?.PersonalCode, filter?.Email, filter?.MemberCode, filter?.CardNumber, filter?.LibrarianCode);

            List<PeopleIndexItemDTO> indexItems = GetIndexItemsList(paginatedPeople);

            return indexItems;
        }

        public PersonDetailsDTO GetDetailsDTO(Guid personId)
        {
            Person person = _personRepository.Get(personId);

            PersonDetailsDTO detailsDTO = new PersonDetailsDTO()
            {
                Id = person.Id,
                PersonalCode = person.PersonalCode,
                Name = person.Name,
                Email = person.Email,
                Phone = person.Phone,
                StreetAddress = person.Address.StreetAddress,
                City = person.Address.City,
                State = person.Address.State,
                ZipCode = person.Address.ZipCode,
                Country = person.Address.Country,
                MemberCode = person.Member?.Code,
                MemberId = person.Member?.Id,
                CardNumber = person.Member?.Cards?
                        .Where(c => c.IsActive == true)
                        .Select(c => c.Number)
                        .FirstOrDefault(),
                LibrarianCode = person.Librarian?.Code,
                LibrarianId = person.Librarian?.Id
            };

            return detailsDTO;
        }

        public PersonEditDTO GetEditDTO(Guid personId)
        {
            Person person = _personRepository.Get(personId);

            PersonEditDTO editDTO = new PersonEditDTO()
            {
                Id = person.Id,
                PersonalCode = person.PersonalCode,
                Name = person.Name,
                Email = person.Email,
                Phone = person.Phone,
                StreetAddress = person.Address.StreetAddress,
                City = person.Address.City,
                State = person.Address.State,
                ZipCode = person.Address.ZipCode,
                Country = person.Address.Country,
                IsMember = person.Member != null,
                IsLibrarian = person.Librarian != null
            };

            return editDTO;
        }

        public int GetItemsCount(PeopleFilterDTO filter)
        {
            return _personRepository.GetCount(
                        filter?.PersonalCode,
                        filter?.Email,
                        filter?.MemberCode,
                        filter?.CardNumber,
                        filter?.LibrarianCode);
        }

        public Guid GetPersonId(string personalCode)
        {
            return _personRepository.GetIdByPersonalCode(personalCode);
        }

        public bool PersonIsOnlyMember(Guid personId)
        {
            Person person = _personRepository.Get(personId);

            return person.Member != null && person.Librarian == null;
        }

        private List<PeopleIndexItemDTO> GetIndexItemsList(IEnumerable<Person> people)
        {
            List<PeopleIndexItemDTO> indexItems = people
                .Select(db => new PeopleIndexItemDTO()
                {
                    Id = db.Id,
                    Name = db.Name,
                    Email = db.Email,
                    MemberCode = db.Member?.Code,
                    MemberId = db.Member?.Id,
                    CardNumber = db.Member?.Cards?
                        .Where(c => c.IsActive == true).Select(c => c.Number).FirstOrDefault(),
                    LibrarianCode = db.Librarian?.Code,
                    LibrarianId = db.Librarian?.Id
                })
                .ToList();

            return indexItems;
        }
    }
}
