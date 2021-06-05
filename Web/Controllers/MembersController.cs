using AutoMapper;
using Business.Authorization;
using Business.Identity;
using Business.Members;
using Business.Members.DTOs;
using Common.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Web.ViewModels.Members;

namespace Web.Controllers
{
    public partial class MembersController : Controller
    {
        private readonly IMemberQueriesService _memberQueriesService;
        private readonly IMemberUpdateService _memberUpdateService;
        private readonly IIdentityService _identityService;
        private readonly ICustomAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public MembersController(
            IMemberQueriesService memberQueriesService,
            IMemberUpdateService memberUpdateService,
            IIdentityService identityService,
            ICustomAuthorizationService authorizationService,
            IMapper mapper)
        {
            _memberQueriesService = memberQueriesService;
            _memberUpdateService = memberUpdateService;
            _identityService = identityService;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        public virtual ActionResult Details(Guid id)
        {
            var isAuthorized = _authorizationService.AuthorizeAsync(
                User, id, OperationAuthorizationRequirements.MemberDetails)
                .Result;

            if (isAuthorized.Succeeded)
            {
                MemberDetailsDTO detailsDTO = _memberQueriesService.GetDetailsDTO(id);
                MemberDetailsViewModel model = _mapper.Map<MemberDetailsViewModel>(detailsDTO);

                if (model == null)
                {
                    return NotFound();
                }

                return View(model);
            }

            return Forbid();
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult AccountStatusChange(Guid id)
        {
            MemberStatusChangeDTO statusChangeDTO = _memberQueriesService.GetStatusChangeDTO(id);
            MemberStatusChangeViewModel model = _mapper.Map<MemberStatusChangeViewModel>(statusChangeDTO);

            return View(MVC.Members.Views.MemberStatusChange, model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult AccountStatusChange(MemberStatusChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _memberUpdateService.ChangeStatus(model.Id, model.AccountStatus);

                string email = _memberQueriesService.GetPersonEmail(model.Id);

                if (_identityService.UserExists(email))
                {
                    _identityService.UpdateRoles(email);
                    _identityService.UpdateUserAccount(email);
                }

                return View(MVC.Account.Views.AccountStatusChangeMessage);
            }
            else
                return View(MVC.Members.Views.MemberStatusChange, model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian + "," + Roles.Member)]
        public virtual ActionResult BorrowedBookItems(Guid id)
        {
            var isAuthorized = _authorizationService.AuthorizeAsync(
                User, id, OperationAuthorizationRequirements.MemberBookItems)
                .Result;

            if (isAuthorized.Succeeded)
            {
                MembersBorrowDTO borrowDTO = _memberQueriesService.GetBorrowedBookItemsDTO(id);
                MembersBorrowViewModel model = _mapper.Map<MembersBorrowViewModel>(borrowDTO);

                return View(model);
            }
            return Forbid();
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian + "," + Roles.Member)]
        public virtual ActionResult ReservedBookItems(Guid id)
        {
            var isAuthorized = _authorizationService.AuthorizeAsync(
                User, id, OperationAuthorizationRequirements.MemberBookItems)
                .Result;

            if (isAuthorized.Succeeded)
            {
                MembersReserveDTO reserveDTO = _memberQueriesService.GetReservedBookItemsDTO(id);
                MembersReserveViewModel model = _mapper.Map<MembersReserveViewModel>(reserveDTO);

                return View(model);
            }

            return Forbid();
        }
    }
}
