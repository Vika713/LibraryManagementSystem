using System;

namespace Web.ViewModels.BookItems
{
    public class BookItemReservationCancelViewModel
    {
        public Guid BookItemId { get; set; }

        public Guid BookId { get; set; }

        public Guid MemberId { get; set; }

        public bool HasOtherBookItems { get; set; }
    }
}
