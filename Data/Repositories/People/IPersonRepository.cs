using Data.Repositories.Generic;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Data.Repositories.People
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Get(Guid id);
        Person GetByEmail(string email);
        Person GetByMemberId(Guid id);
        Person GetByLibrarianId(Guid id);
        IEnumerable<Person> GetFilteredAndPaginated(
            int skipNumber,
            int takeNumber,
            string personalCode,
            string email,
            string memberId,
            string cardNumber,
            string librarianId);
        Guid GetIdByPersonalCode(string personalCode);
        Guid GetIdByEmail(string email);
        int GetCount(
            string personalCode, string email, string memberId, string cardNumber, string librarianId);
        string GetPersonalCode(Guid id);
        bool ExistsByEmail(string email);
        bool ExistsByPersonalCode(string personalCode);
    }
}
