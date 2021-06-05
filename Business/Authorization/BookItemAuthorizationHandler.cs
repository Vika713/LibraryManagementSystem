using Common.Enumeration;
using Data.Repositories.BookItems;
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
    public class BookItemAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Guid>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonRepository _personRepository;
        private readonly IBookItemRepository _bookItemRepository;

        public BookItemAuthorizationHandler(
                    UserManager<ApplicationUser> userManager,
                    IPersonRepository personRepository,
                    IBookItemRepository bookItemRepository)
        {
            _userManager = userManager;
            _personRepository = personRepository;
            _bookItemRepository = bookItemRepository;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Guid bookItemId)
        {
            if (context.User == null || _userManager.GetUserAsync(context.User).Result == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != OperationNames.CancelReservationName)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Roles.Librarian) || context.User.IsInRole(Roles.Admin))
            {
                context.Succeed(requirement);
            }

            BookItem bookItem = _bookItemRepository.Get(bookItemId);
            Person person = _personRepository.GetByEmail(
                _userManager.FindByIdAsync(_userManager.GetUserId(context.User)).Result.Email);

            if (context.User.IsInRole(Roles.Member) &&
                person.Member.Id == bookItem.ReservedMemberId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
