using System;
using System.Collections.Generic;

namespace Business.Members.DTOs
{
    public class MembersBorrowDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public List<BorrowedBookItemDTO> BorrowedBookItems { get; set; }
    }
}
