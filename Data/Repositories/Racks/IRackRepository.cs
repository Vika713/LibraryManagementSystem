using Data.Repositories.Generic;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Data.Repositories.Racks
{
    public interface IRackRepository : IRepository<Rack>
    {
        Rack Get(Guid id);
        Rack GetByNumberLocationIdentifier(int? rackNumber, string locationIdentifier);
        IEnumerable<Rack> Get(IEnumerable<Guid> ids);
        IEnumerable<Rack> GetFilteredAndPaginated(
            int skipNumber,
            int takeNumber,
            int? rackNumber,
            string locationIdentifier);
        IEnumerable<int> GetRackNumbers(string searchString);
        IEnumerable<string> GetLocationIdentifiers(string searchString);
        Guid GetIdByNumberLocationIdentifier(int? rackNumber, string locationIdentifier);
        int GetCount(int? rackNumber, string locationIdentifier);
        int GetRackNumber(Guid id);
        string GetLocationIdentifier(Guid id);
        bool Exists(int rackNumber, string locationIdentifier);
    }
}
