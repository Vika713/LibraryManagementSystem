using Common.Enumeration;
using System;

namespace Business.Members.DTOs
{
    public class MemberStatusChangeDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public MemberStatus CurrentStatus { get; set; }
    }
}
