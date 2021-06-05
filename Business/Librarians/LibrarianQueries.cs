using Business.Librarians.DTOs;
using Data.Repositories.Librarians;
using Data.Repositories.People;
using Domain.Models;
using System;

namespace Business.Librarians
{
    public class LibrarianQueries : ILibrarianQueriesService
    {
        private readonly ILibrarianRepository _librarianRepository;
        private readonly IPersonRepository _personRepository;

        public LibrarianQueries(ILibrarianRepository librarianRepository, IPersonRepository personRepository)
        {
            _librarianRepository = librarianRepository;
            _personRepository = personRepository;
        }

        public LibrarianDetailsDTO GetDetailsDTO(Guid librarianId)
        {
            Librarian librarian = _librarianRepository.Get(librarianId);

            LibrarianDetailsDTO detailsDTO = new LibrarianDetailsDTO()
            {
                Id = librarian.Id,
                PersonId = librarian.PersonId,
                PersonalCode = _personRepository.GetPersonalCode(librarian.PersonId),
                Code = librarian.Code,
                AccountStatus = librarian.Status
            };

            return detailsDTO;
        }

        public LibrarianStatusChangeDTO GetStatusChangeDTO(Guid librarianId)
        {
            Librarian librarian = _librarianRepository.Get(librarianId);

            LibrarianStatusChangeDTO statusChangeDTO = new LibrarianStatusChangeDTO()
            {
                Id = librarian.Id,
                Code = librarian.Code,
                CurrentStatus = librarian.Status,
            };

            return statusChangeDTO;
        }

        public string GetPersonEmail(Guid librarianId)
        {
            return _personRepository.GetByLibrarianId(librarianId).Email;
        }
    }
}
