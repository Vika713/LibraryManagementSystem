using Business.Librarians.DTOs;
using System;

namespace Business.Librarians
{
    public interface ILibrarianQueriesService
    {
        LibrarianDetailsDTO GetDetailsDTO(Guid librarianId);
        LibrarianStatusChangeDTO GetStatusChangeDTO(Guid librarianId);
        string GetPersonEmail(Guid librarianId);
    }
}
