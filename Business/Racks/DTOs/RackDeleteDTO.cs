using System;

namespace Business.Racks.DTOs
{
    public class RackDeleteDTO
    {
        public Guid Id { get; set; }
        public int RackNumber { get; set; }
        public string LocationIdentifier { get; set; }
    }
}
