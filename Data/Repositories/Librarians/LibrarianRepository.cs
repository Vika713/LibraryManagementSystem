using Data.Context;
using Data.Repositories.Generic;
using Domain.Models;
using System;
using System.Linq;

namespace Data.Repositories.Librarians
{
    public class LibrarianRepository : Repository<Librarian>, ILibrarianRepository
    {
        public LibraryContext LibraryContext
        {
            get { return DatabaseContext as LibraryContext; }
        }

        public LibrarianRepository(LibraryContext context) : base(context) { }

        public Librarian Get(Guid id)
        {
            return Query().Single(l => l.Id == id);
        }

        private IQueryable<Librarian> Query()
        {
            return LibraryContext.Librarians;
        }
    }
}
