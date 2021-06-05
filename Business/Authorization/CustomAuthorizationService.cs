using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Business.Authorization
{
    public class CustomAuthorizationService : ICustomAuthorizationService
    {
        private readonly IAuthorizationService service;

        public CustomAuthorizationService(IAuthorizationService service)
        {
            this.service = service;
        }

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, IAuthorizationRequirement requirement)
        {
            return service.AuthorizeAsync(user, resource, requirement);
        }
    }
}
