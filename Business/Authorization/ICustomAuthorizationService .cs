using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Business.Authorization
{
    public interface ICustomAuthorizationService
    {
        Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, IAuthorizationRequirement requirement);
    }
}
