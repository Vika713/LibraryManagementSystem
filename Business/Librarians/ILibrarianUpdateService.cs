using Common.Enumeration;
using System;

namespace Business.Librarians
{
    public interface ILibrarianUpdateService
    {
        void ChangeStatus(Guid librarianId, LibrarianStatus newStatus);
    }
}
