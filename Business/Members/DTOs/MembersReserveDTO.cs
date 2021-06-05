using System;
using System.Collections.Generic;

namespace Business.Members.DTOs
{
    public class MembersReserveDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public List<ReservedBookItemDTO> ReservedBookItems { get; set; }
    }
}
