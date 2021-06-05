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
    public class PersonAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Guid>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonRepository _personRepository;

        public PersonAuthorizationHandler(
            UserManager<ApplicationUser> userManager,
            IPersonRepository personRepository)
        {
            _userManager = userManager;
            _personRepository = personRepository;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Guid personId)
        {
            if (context.User == null || _userManager.GetUserAsync(context.User).Result == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != OperationNames.PersonEditName &&
                requirement.Name != OperationNames.PersonDetailsName)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Roles.Admin))
            {
                context.Succeed(requirement);
            }

            Person person = _personRepository.Get(personId);

            if (context.User.IsInRole(Roles.Librarian) && person.Librarian == null)
            {
                context.Succeed(requirement);
            }

            if (person.Email == _userManager.FindByIdAsync(_userManager.GetUserId(context.User)).Result.Email)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
