using Data.Repositories.Generic;
using Domain.Models;
using System;

namespace Data.Repositories.Librarians
{
    public interface ILibrarianRepository : IRepository<Librarian>
    {
        Librarian Get(Guid id);
    }
}
