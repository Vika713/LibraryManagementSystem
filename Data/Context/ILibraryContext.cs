using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public interface ILibraryContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookItem> BookItems { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Card> Cards { get; set; }
        public object Card { get; set; }
    }
}
