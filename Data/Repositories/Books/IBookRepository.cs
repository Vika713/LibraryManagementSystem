using Data.Repositories.Generic;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Data.Repositories.Books
{
    public interface IBookRepository : IRepository<Book>
    {
        Book Get(Guid id);
        Book GetByISBN(string ISBN);
        IEnumerable<Book> Get(IEnumerable<Guid> ids);
        IEnumerable<Book> GetFilteredAndPaginated(
            int skipNumber,
            int takeNumber,
            string title,
            string author,
            string subject,
            string publicationDate);
        int GetCount(string title, string author, string subject, string publicationDate);
        string GetISBN(Guid id);
        bool HasBookItems(Guid bookId);
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> Ids);
        IEnumerable<Author> GetAuthorsByNames(IEnumerable<string> names);
        IEnumerable<Author> GetAuthorsByBookId(Guid bookId);
        void RemoveAuthors(List<Author> authors);
        void AddAuthors(List<Author> authors);
    }
}
