using System;

namespace Business.Identity.DTOs
{
    public class ProfileNavigationDTO
    {
        public Guid PersonId { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? LibrarianId { get; set; }
    }
}
