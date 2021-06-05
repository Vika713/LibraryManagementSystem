using Business.Identity.DTOs;
using Domain.Areas.Identity;
using System;
using System.Collections.Generic;

namespace Business.Identity
{
    public interface IIdentityService
    {
        ProfileNavigationDTO GetProfileNavigationDTO();
        List<string> GetRoles(string email);
        Guid GetPersonId(ApplicationUser user);
        string GetCurrentUserEmail();
        bool UserExists(string email);
        bool CurrentUserIsInRole(string role);
        void UpdateRoles(string email);
        void UpdateUserAccount(string email);
        void ChangePersonEmail(Guid personId, string email);
    }
}
