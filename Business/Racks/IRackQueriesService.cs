using Business.Racks.DTOs;
using System;
using System.Collections.Generic;

namespace Business.Racks
{
    public interface IRackQueriesService
    {
        List<RacksIndexItemDTO> GetIndexItems(RacksFilterDTO filter, int pageIndex, int itemsCount);
        RackEditDTO GetEditDTO(Guid rackId, int pageIndex, int itemsCount);
        RackDeleteDTO GetDeleteDTO(Guid rackId);
        int GetItemsCount(RacksFilterDTO filter);
        int GetBookItemsCountByRack(Guid rackId);
    }
}
