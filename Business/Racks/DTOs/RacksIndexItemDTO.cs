using System;

namespace Business.Racks.DTOs
{
    public class RacksIndexItemDTO
    {
        public Guid Id { get; set; }
        public int RackNumber { get; set; }
        public string LocationIdentifier { get; set; }
        public bool HasBookItems { get; set; }
    }
}
