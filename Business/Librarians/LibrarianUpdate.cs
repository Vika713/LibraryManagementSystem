using Common.Enumeration;
using Data.Repositories.Librarians;
using Domain.Models;
using System;

namespace Business.Librarians
{
    public class LibrarianUpdate : ILibrarianUpdateService
    {
        private readonly ILibrarianRepository _librarianRepository;

        public LibrarianUpdate(ILibrarianRepository librarianRepository)
        {
            _librarianRepository = librarianRepository;
        }

        public void ChangeStatus(Guid librarianId, LibrarianStatus newStatus)
        {
            Librarian librarian = _librarianRepository.Get(librarianId);

            librarian.ChangeStatus(newStatus);

            _librarianRepository.Update(librarian);
            _librarianRepository.SaveChanges();
        }
    }
}
