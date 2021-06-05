using Common.Enumeration;
using System;

namespace Business.Members.DTOs
{
    public class MemberDetailsDTO
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Code { get; set; }
        public MemberStatus? AccountStatus { get; set; }
        public DateTime DateOfMembership { get; set; }
        public string CardNumber { get; set; }
        public string CardBarcode { get; set; }
        public DateTime? CardIssueDate { get; set; }
        public int BorrowedBooks { get; set; }
        public int ReservedBooks { get; set; }
    }
}
