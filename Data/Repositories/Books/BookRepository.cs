using Data.Context;
using Data.Repositories.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.Books
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public LibraryContext LibraryContext
        {
            get { return DatabaseContext as LibraryContext; }
        }

        public BookRepository(LibraryContext context) : base(context) { }

        public Book Get(Guid id)
        {
            return Query().Single(b => b.Id == id);
        }

        public Book GetByISBN(string ISBN)
        {
            return Query().SingleOrDefault(b => b.ISBN == ISBN);
        }

        public IEnumerable<Book> Get(IEnumerable<Guid> ids)
        {
            IEnumerable<Book> books = Query().Where(b => ids.Contains(b.Id));

            return books;
        }

        public IEnumerable<Book> GetFilteredAndPaginated(
            int skipNumber,
            int takeNumber,
            string title,
            string author,
            string subject,
            string publicationDate)
        {
            return GetFiltered(title, author, subject, publicationDate)
                .Skip(skipNumber).Take(takeNumber);
        }

        public int GetCount(string title, string author, string subject, string publicationDate)
        {
            return GetFiltered(title, author, subject, publicationDate).Count();
        }

        public string GetISBN(Guid id)
        {
            return Get(id).ISBN;
        }

        public bool HasBookItems(Guid bookId)
        {
            return Get(bookId).BookItems.Any();
        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> ids)
        {
            return AuthorsQuery().Where(a => ids.Contains(a.Id));
        }

        public IEnumerable<Author> GetAuthorsByNames(IEnumerable<string> names)
        {
            IEnumerable<Author> authors = AuthorsQuery().Where(a => names.Contains(a.Name));

            return authors;
        }

        public IEnumerable<Author> GetAuthorsByBookId(Guid bookId)
        {
            IEnumerable<Author> authors = Get(bookId).BookAuthors.Select(ba => ba.Author);

            return authors;
        }

        public void RemoveAuthors(List<Author> authors)
        {
            LibraryContext.Authors.RemoveRange(authors);
        }

        public void AddAuthors(List<Author> authors)
        {
            LibraryContext.Authors.AddRange(authors);
        }

        private IQueryable<Book> Query()
        {
            return LibraryContext.Books
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.BookItems)
                .OrderByDescending(b => b.CreatedAt);
        }

        private IQueryable<Book> GetFiltered(string title, string author, string subject, string publicationDate)
        {
            return Query()
                .Where(b =>
                    (title == null || b.Title.ToLower().Contains(title.ToLower())) &&
                    (author == null || b.BookAuthors.Where(ba =>
                            ba.Author.Name.ToLower().Contains(author.ToLower())).Any()) &&
                    (subject == null || b.Subject.ToLower().Contains(subject.ToLower())) &&
                    (publicationDate == null || b.BookItems.Where(bi =>
                            bi.PublicationDate.ToString().Contains(publicationDate)).Any()));
        }

        private IQueryable<Author> AuthorsQuery()
        {
            return LibraryContext.Authors
                .Include(a => a.BookAuthors);
        }
    }
}
