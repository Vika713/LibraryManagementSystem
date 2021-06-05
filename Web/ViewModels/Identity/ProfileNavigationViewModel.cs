using System;

namespace Web.ViewModels.Identity
{
    public class ProfileNavigationViewModel
    {
        public Guid PersonId { get; set; }

        public Guid? MemberId { get; set; }

        public Guid? LibrarianId { get; set; }
    }
}
