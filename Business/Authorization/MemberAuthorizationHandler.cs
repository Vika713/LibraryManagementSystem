using Common.Enumeration;
using Data.Repositories.People;
using Domain.Areas.Identity;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Business.Authorization
{
    public class MemberAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Guid>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonRepository _personRepository;

        public MemberAuthorizationHandler(
            UserManager<ApplicationUser> userManager,
            IPersonRepository personRepository)
        {
            _userManager = userManager;
            _personRepository = personRepository;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Guid memberId)
        {
            if (context.User == null || _userManager.GetUserAsync(context.User).Result == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != OperationNames.MemberDetailsName &&
                requirement.Name != OperationNames.MemberBookItemsName)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Roles.Admin) || context.User.IsInRole(Roles.Librarian))
            {
                context.Succeed(requirement);
            }

            Person person = _personRepository.GetByMemberId(memberId);

            if (person.Email == _userManager.FindByIdAsync(_userManager.GetUserId(context.User)).Result.Email)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
