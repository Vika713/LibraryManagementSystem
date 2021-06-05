using Business.People.DTOs;
using System;
using System.Collections.Generic;

namespace Business.People
{
    public interface IPersonQueriesService
    {
        List<PeopleIndexItemDTO> GetIndexItems(PeopleFilterDTO filter, int pageIndex, int pageSize);
        PersonDetailsDTO GetDetailsDTO(Guid personId);
        PersonEditDTO GetEditDTO(Guid personId);
        int GetItemsCount(PeopleFilterDTO filter);
        Guid GetPersonId(string personalCode);
        bool PersonIsOnlyMember(Guid personId);
    }
}
