using System;

namespace Business.Cards.DTOs
{
    public class CardCreateDTO
    {
        public string Number { get; set; }
        public Guid PersonId { get; set; }
        public Guid MemberId { get; set; }
        public string MemberCode { get; set; }
        public string Barcode { get; set; }
    }
}
