using Microsoft.AspNetCore.Identity;

namespace Domain.Areas.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public bool? IsEnabled { get; set; }
    }
}
