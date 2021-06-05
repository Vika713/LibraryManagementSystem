using AutoMapper;
using Business.Authorization;
using Business.Identity;
using Business.Librarians;
using Business.Librarians.DTOs;
using Common.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Web.ViewModels.Librarians;

namespace Web.Controllers
{
    public partial class LibrariansController : Controller
    {
        private readonly ILibrarianQueriesService _librarianQueriesService;
        private readonly ILibrarianUpdateService _librarianUpdateService;
        private readonly IIdentityService _identityService;
        private readonly ICustomAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public LibrariansController(
            ILibrarianQueriesService librarianQueriesService,
            ILibrarianUpdateService librarianUpdateService,
            IIdentityService identityService,
            ICustomAuthorizationService authorizationService,
            IMapper mapper)
        {
            _librarianQueriesService = librarianQueriesService;
            _librarianUpdateService = librarianUpdateService;
            _identityService = identityService;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult Details(Guid id)
        {
            var isAuthorized = _authorizationService.AuthorizeAsync(
                User, id, OperationAuthorizationRequirements.LibrarianDetails)
                .Result;

            if (isAuthorized.Succeeded)
            {
                LibrarianDetailsDTO detailsDTO = _librarianQueriesService.GetDetailsDTO(id);
                LibrarianDetailsViewModel model = _mapper.Map<LibrarianDetailsViewModel>(detailsDTO);

                if (model == null)
                {
                    return NotFound();
                }

                return View(model);
            }
            return Forbid();
        }

        [Authorize(Roles = Roles.Admin)]
        public virtual ActionResult AccountStatusChange(Guid id)
        {
            LibrarianStatusChangeDTO statusChangeDTO = _librarianQueriesService.GetStatusChangeDTO(id);
            LibrarianStatusChangeViewModel model = _mapper.Map<LibrarianStatusChangeViewModel>(statusChangeDTO);

            return View(MVC.Librarians.Views.LibrarianStatusChange, model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult AccountStatusChange(LibrarianStatusChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _librarianUpdateService.ChangeStatus(model.Id, model.AccountStatus);

                string email = _librarianQueriesService.GetPersonEmail(model.Id);

                if (_identityService.UserExists(email))
                {
                    _identityService.UpdateRoles(email);
                    _identityService.UpdateUserAccount(email);
                }

                return View(MVC.Account.Views.AccountStatusChangeMessage);
            }
            else
                return View(MVC.Librarians.Views.LibrarianStatusChange, model);
        }
    }
}
