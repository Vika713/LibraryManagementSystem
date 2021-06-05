using Data.Context;
using Data.Repositories.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.Racks
{
    public class RackRepository : Repository<Rack>, IRackRepository
    {
        public LibraryContext LibraryContext
        {
            get { return DatabaseContext as LibraryContext; }
        }

        public RackRepository(LibraryContext context) : base(context) { }

        public Rack Get(Guid id)
        {
            return Query().Single(r => r.Id == id);
        }

        public Rack GetByNumberLocationIdentifier(int? rackNumber, string locationIdentifier)
        {
            return Query()
                .SingleOrDefault(r => r.RackNumber == rackNumber && r.LocationIdentifier == locationIdentifier);
        }

        public IEnumerable<Rack> Get(IEnumerable<Guid> ids)
        {
            return Query().Where(r => ids.Contains(r.Id));
        }

        public IEnumerable<Rack> GetFilteredAndPaginated(
            int skipNumber,
            int takeNumber,
            int? rackNumber,
            string locationIdentifier)
        {
            return GetFiltered(rackNumber, locationIdentifier)
                .Skip(skipNumber).Take(takeNumber);
        }

        public IEnumerable<int> GetRackNumbers(string searchString)
        {
            return Query()
                .Where(r => (searchString == null || r.RackNumber.ToString().Contains(searchString)))
                .Select(r => r.RackNumber).Distinct();
        }

        public IEnumerable<string> GetLocationIdentifiers(string searchString)
        {
            return Query()
                .Where(r => (searchString == null || r.LocationIdentifier.Contains(searchString)))
                .Select(r => r.LocationIdentifier).Distinct();
        }

        public Guid GetIdByNumberLocationIdentifier(int? rackNumber, string locationIdentifier)
        {
            Rack rack = LibraryContext.Racks
                .Single(r => r.RackNumber == rackNumber && r.LocationIdentifier == locationIdentifier);

            return rack.Id;
        }

        public int GetCount(int? rackNumber, string locationIdentifier)
        {
            return GetFiltered(rackNumber, locationIdentifier).Count();
        }

        public int GetRackNumber(Guid id)
        {
            return Query().Single(r => r.Id == id).RackNumber;
        }

        public string GetLocationIdentifier(Guid id)
        {
            return Query().Single(r => r.Id == id).LocationIdentifier;
        }

        public bool Exists(int rackNumber, string locationIdentifier)
        {
            return GetByNumberLocationIdentifier(rackNumber, locationIdentifier) != null;
        }

        private IQueryable<Rack> Query()
        {
            return LibraryContext.Racks
                .OrderByDescending(b => b.CreatedAt);
        }

        private IEnumerable<Rack> GetFiltered(int? rackNumber, string locationIdentifier)
        {
            return Query()
                .Where(r =>
                    (rackNumber == null || r.RackNumber.ToString().Contains(rackNumber.ToString())) &&
                    (locationIdentifier == null || r.LocationIdentifier.ToLower()
                                                        .Contains(locationIdentifier.ToLower())));
        }
    }
}
