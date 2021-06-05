using System;

namespace Business.BookItems.DTOs
{
    public class BookItemReserveDTO
    {
        public Guid BookItemId { get; set; }
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }
        public bool HasOtherBookItems { get; set; }
    }
}
