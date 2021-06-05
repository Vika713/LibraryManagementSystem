using System;

namespace Business.Cards.DTOs
{
    public class CardBlockDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid PersonId { get; set; }
        public Guid MemberId { get; set; }
        public string MemberCode { get; set; }
    }
}
