using System;
using System.Collections.Generic;

namespace Business.Racks.DTOs
{
    public class RackEditDTO
    {
        public Guid RackId { get; set; }
        public int RackNumber { get; set; }
        public string LocationIdentifier { get; set; }
        public List<RackBookItemsDTO> BookItemsOnRack { get; set; }
    }
}
