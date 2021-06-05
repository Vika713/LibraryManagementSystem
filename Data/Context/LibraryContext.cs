using Common.Enumeration;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Context
{
    public class LibraryContext : DbContext, ILibraryContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.AuthorId, ba.BookId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<Author>()
                .HasIndex(a => a.Name)
                .IsUnique();

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();

            modelBuilder.Entity<BookItem>()
                .HasIndex(bi => bi.Barcode)
                .IsUnique();

            modelBuilder.Entity<BookItem>()
                 .HasOne(bi => bi.Book)
                 .WithMany(b => b.BookItems)
                 .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<BookItem>()
                 .HasOne(bi => bi.Rack)
                 .WithMany(b => b.BookItems)
                 .HasForeignKey(bi => bi.RackId);

            modelBuilder.Entity<BookItem>()
                .HasOne(bi => bi.BorrowedMember)
                .WithMany(m => m.BorrowedBookItems)
                .HasForeignKey(bi => bi.BorrowedMemberId);

            modelBuilder.Entity<BookItem>()
                .HasOne(bi => bi.ReservedMember)
                .WithMany(m => m.ReservedBookItems)
                .HasForeignKey(bi => bi.ReservedMemberId);

            modelBuilder.Entity<Person>()
                .HasIndex(p => p.PersonalCode)
                .IsUnique();

            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Address)
                .WithOne(a => a.Person)
                .HasForeignKey<Address>(p => p.PersonId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Member)
                .WithOne(m => m.Person)
                .HasForeignKey<Member>(p => p.PersonId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Librarian)
                .WithOne(m => m.Person)
                .HasForeignKey<Librarian>(p => p.PersonId);

            modelBuilder.Entity<Card>()
               .HasIndex(c => c.Number)
               .IsUnique();

            modelBuilder.Entity<Card>()
               .HasIndex(c => c.Barcode)
               .IsUnique();

            modelBuilder.Entity<Card>()
                 .HasOne(c => c.Member)
                 .WithMany(m => m.Cards)
                 .HasForeignKey(c => c.MemberId);

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.Code)
                .IsUnique();

            modelBuilder.Entity<Librarian>()
                .HasIndex(l => l.Code)
                .IsUnique();

            modelBuilder.Entity<Rack>()
                .HasIndex(r => new { r.RackNumber, r.LocationIdentifier })
                .IsUnique();

            //Seeds

            Guid[] peopleIds = GetIds();
            Object[] people = GetPeople(peopleIds);
            Object[] addresses = GetAddresses(peopleIds);
            Librarian librarian = new Librarian("L210000", peopleIds[0]);
            Member member = new Member("M210000", DateTime.Today, peopleIds[1]);
            Card card = new Card(member.Id, "12345", "123456", DateTime.Today, true);

            modelBuilder.Entity<Person>().HasData(people);
            modelBuilder.Entity<Address>().HasData(addresses);
            modelBuilder.Entity<Librarian>().HasData(librarian);
            modelBuilder.Entity<Member>().HasData(member);
            modelBuilder.Entity<Card>().HasData(card);

            Rack rack = new Rack(0, "A");
            Author[] authors = GetAuthors();
            Guid[] booksIds = GetIds();
            Object[] books = GetBooks(booksIds);
            Object[] booksAuthors = GetBooksAuthors(new Guid[] { authors[0].Id, authors[1].Id, authors[2].Id }, booksIds);
            BookItem[] bookItems = GetBookItems(booksIds, rack.Id);

            modelBuilder.Entity<Rack>().HasData(rack);
            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<BookAuthor>().HasData(booksAuthors);
            modelBuilder.Entity<BookItem>().HasData(bookItems);
        }

        private Guid[] GetIds()
        {
            return new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
        }

        private Object[] GetPeople(Guid[] personIds)
        {
            Object person1 = new
            {
                Id = personIds[0],
                PersonalCode = "41234567890",
                Name = "Sarah Smith",
                Email = "librarian@lms.com",
                Phone = "860000000",
                CreatedAt = DateTime.Now,
            };
            Object person2 = new
            {
                Id = personIds[1],
                PersonalCode = "31234567890",
                Name = "John Adams",
                Email = "member@lms.com",
                Phone = "861111111",
                CreatedAt = DateTime.Now,
            };
            Object person3 = new
            {
                Id = personIds[2],
                PersonalCode = "00000000000",
                Name = "Admin",
                Email = "admin@lms.com",
                Phone = "000000000",
                CreatedAt = DateTime.Now,
            };

            return new Object[] { person1, person2, person3 };
        }

        private Object[] GetAddresses(Guid[] personIds)
        {
            Object address1 = new 
            {
                Id = Guid.NewGuid(),
                StreetAddress = "Gabijos",
                City = "Vilnius",
                ZipCode = "LT-00000",
                Country = "Lithuania",
                PersonId = personIds[0] 
            };
            Object address2 = new
            {
                Id = Guid.NewGuid(),
                StreetAddress = "Vilniaus",
                City = "Vilnius",
                ZipCode = "LT-00000",
                Country = "Lithuania",
                PersonId = personIds[1]
            };
            Object address3 = new
            {
                Id = Guid.NewGuid(),
                StreetAddress = "Medeinos",
                City = "Vilnius",
                ZipCode = "LT-00000",
                Country = "Lithuania",
                PersonId = personIds[2]
            };

            return new Object[] { address1, address2, address3 };
        }

        private Author[] GetAuthors()
        {
            return new Author[] { new Author("Harari, Yuval Noah"), new Author("King, Stephen"), new Author("Straub, Peter") };
        }

        private Object[] GetBooks(Guid[] ids)
        {
            Object book1 = new
            {
                Id = ids[0],
                CreatedAt = DateTime.Now,
                ISBN = "0062316095",
                Title = "Sapiens: A Brief History of Humankind",
                Subject = "History",
                Publisher = "Harper",
                Language = "English",
                NumberOfPages = 464
            };
            Object book2 = new
            {
                Id = ids[1],
                CreatedAt = DateTime.Now,
                ISBN = "9781451697216",
                Title = "The Talisman: A Novel",
                Subject = "Dark Fantasy",
                Publisher = "Pocket Books",
                Language = "English",
                NumberOfPages = 921
            };

            return new Object[] { book1, book2 };
        }

        private Object[] GetBooksAuthors(Guid[] authorsIds, Guid[] booksIds)
        {
            return new Object[] 
            {
                new { AuthorId = authorsIds[0], BookId = booksIds[0] },
                new { AuthorId = authorsIds[1], BookId = booksIds[1] },
                new { AuthorId = authorsIds[2], BookId = booksIds[1] } 
            };
        }

        private BookItem[] GetBookItems(Guid[] bookIds, Guid rackId)
        {
            BookItem bookItem1 = new BookItem(
                bookIds[0], 
                "001", 
                new DateTime(2015, 02, 10), 
                25.70, 
                BookFormat.Hardcover, 
                new DateTime(2021, 06, 01), 
                rackId
            );

            BookItem bookItem2 = new BookItem(
                bookIds[1],
                "002",
                new DateTime(2012, 09, 01),
                44.87,
                BookFormat.Paperback,
                new DateTime(2021, 05, 03),
                rackId
            );

            return new BookItem[] { bookItem1, bookItem2 };
        }
    }
}
