using Data.Context;
using Data.Repositories.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.People
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public LibraryContext LibraryContext
        {
            get { return DatabaseContext as LibraryContext; }
        }

        public PersonRepository(LibraryContext context) : base(context) { }



        public Person Get(Guid id)
        {
            return Query().Single(p => p.Id == id);
        }

        public Person GetByEmail(string email)
        {
            return Query().SingleOrDefault(p => p.Email == email);
        }

        public Person GetByMemberId(Guid id)
        {
            return Query().Single(p => p.Member.Id == id);
        }

        public Person GetByLibrarianId(Guid id)
        {
            return Query().Single(p => p.Librarian.Id == id);
        }

        public IEnumerable<Person> GetFilteredAndPaginated(
            int skipNumber,
            int takeNumber,
            string personalCode,
            string email,
            string memberId,
            string cardNumber,
            string librarianId)
        {
            return GetFiltered(personalCode, email, memberId, cardNumber, librarianId)
                .Skip(skipNumber).Take(takeNumber);
        }

        public Guid GetIdByPersonalCode(string personalCode)
        {
            return LibraryContext.People
                .Single(p => p.PersonalCode == personalCode)
                .Id;
        }

        public Guid GetIdByEmail(string email)
        {
            return LibraryContext.People
                .Single(p => p.Email == email)
                .Id;
        }

        public int GetCount(
            string personalCode, string email, string memberId, string cardNumber, string librarianId)
        {
            return GetFiltered(personalCode, email, memberId, cardNumber, librarianId).Count();
        }

        public string GetPersonalCode(Guid id)
        {
            return Get(id).PersonalCode;
        }

        public bool ExistsByEmail(string email)
        {
            return LibraryContext.People
                .SingleOrDefault(p => p.Email == email) != null;
        }

        public bool ExistsByPersonalCode(string personalCode)
        {
            return LibraryContext.People
                .SingleOrDefault(p => p.PersonalCode == personalCode) != null;
        }

        private IQueryable<Person> Query()
        {
            return LibraryContext.People
                .Include(p => p.Member)
                    .ThenInclude(m => m.Cards)
                .Include(p => p.Librarian)
                .Include(p => p.Address)
                .OrderByDescending(b => b.CreatedAt);
        }

        private IEnumerable<Person> GetFiltered(
            string personalCode, string email, string memberId, string cardNumber, string librarianId)
        {
            return Query()
                .Where(p =>
                    (personalCode == null || p.PersonalCode == personalCode) &&
                    (email == null || p.Email.Contains(email)) &&
                    (memberId == null || p.Member.Code.ToLower().Contains(memberId.ToLower())) &&
                    (cardNumber == null || p.Member.Cards.Where(c =>
                            c.Number.ToLower().Contains(cardNumber.ToLower()) && c.IsActive).Any()) &&
                    (librarianId == null || p.Librarian.Code.ToLower().Contains(librarianId.ToLower())));
        }
    }
}
