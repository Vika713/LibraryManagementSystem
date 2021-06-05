using Business.Identity.DTOs;
using Common.Enumeration;
using Data.Repositories.People;
using Domain.Areas.Identity;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Business.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IPersonRepository _personRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(
            IPersonRepository personRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _personRepository = personRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public ProfileNavigationDTO GetProfileNavigationDTO()
        {
            Person person = _personRepository.GetByEmail(GetCurrentUserEmail());

            return new ProfileNavigationDTO()
            {
                PersonId = person.Id,
                MemberId = person.Member?.Id,
                LibrarianId = person.Librarian?.Id
            };
        }

        public List<string> GetRoles(string email)
        {
            Person person = _personRepository.GetByEmail(email);

            List<string> roles = new List<string>();

            if (person.Member != null && person.Member?.Status == MemberStatus.Active)
                roles.Add(Roles.Member);

            if (person.Librarian != null && person.Librarian?.Status == LibrarianStatus.Active)
                roles.Add(Roles.Librarian);

            return roles;
        }

        public Guid GetPersonId(ApplicationUser user)
        {
            return _personRepository.GetIdByEmail(user.Email);
        }

        public string GetCurrentUserEmail()
        {
            return GetCurrentUser().Email;
        }

        public bool UserExists(string email)
        {
            return _userManager.FindByEmailAsync(email).Result != null;
        }

        public bool CurrentUserIsInRole(string role)
        {
            var roles = _userManager.GetRolesAsync(GetCurrentUser()).Result;

            return roles.Any(r => r == role);
        }

        public void UpdateRoles(string email)
        {
            ApplicationUser user = _userManager.FindByEmailAsync(email).Result;

            var currentRoles = _userManager.GetRolesAsync(user).Result.ToList();

            List<string> rolesToAdd = GetRoles(email);
            if (rolesToAdd.Any() && rolesToAdd != null)
            {
                foreach (string role in rolesToAdd)
                {
                    if (currentRoles.Any(currentRole => currentRole == role) == false)
                        _userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
                }
            }

            List<string> rolesToRemove = GetRolesToRemove(email);
            if (rolesToRemove.Any() && rolesToRemove != null)
            {
                foreach (string role in rolesToRemove)
                {
                    if (currentRoles.Any(currentRole => currentRole == role) == true)
                        _userManager.RemoveFromRoleAsync(user, role).GetAwaiter().GetResult();
                }
            }
        }

        public void UpdateUserAccount(string email)
        {
            ApplicationUser user = _userManager.FindByEmailAsync(email).Result;

            bool isEnabled = AccountIsActive(email);

            if (user.IsEnabled != isEnabled)
            {
                user.IsEnabled = isEnabled;
                _userManager.UpdateAsync(user).GetAwaiter().GetResult();
            }
        }

        public void ChangePersonEmail(Guid personId, string email)
        {
            Person person = _personRepository.Get(personId);
            person.Edit(email);

            _personRepository.Update(person);
            _personRepository.SaveChanges();
        }

        private List<string> GetRolesToRemove(string email)
        {
            Person person = _personRepository.GetByEmail(email);

            List<string> roles = new List<string>();

            if (person.Member != null && person.Member?.Status != MemberStatus.Active)
                roles.Add(Roles.Member);

            if (person.Librarian != null && person.Librarian?.Status != LibrarianStatus.Active)
                roles.Add(Roles.Librarian);

            return roles;
        }

        private ApplicationUser GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userManager.FindByIdAsync(userId).Result;
        }

        private bool AccountIsActive(string email)
        {
            Person person = _personRepository.GetByEmail(email);

            return (person.Librarian?.Status == LibrarianStatus.Active ||
                person.Member?.Status == MemberStatus.Active ||
                person.Member?.Status == MemberStatus.Blacklisted);
        }
    }
}
