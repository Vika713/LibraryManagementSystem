using AutoMapper;
using Business.Authorization;
using Business.Identity;
using Business.People;
using Business.People.DTOs;
using Common.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web.ViewModels.People;

namespace Web.Controllers
{
    public partial class PeopleController : Controller
    {
        private readonly IPersonQueriesService _personQueriesService;
        private readonly IPersonUpdateService _personUpdateService;
        private readonly IIdentityService _identityService;
        private readonly ICustomAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public PeopleController(
            IPersonQueriesService personQueriesService,
            IPersonUpdateService personUpdateService,
            IIdentityService identityService,
            ICustomAuthorizationService authorizationService,
            IMapper mapper)
        {
            _personQueriesService = personQueriesService;
            _personUpdateService = personUpdateService;
            _identityService = identityService;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual IActionResult Index(PeopleIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                PeopleFilterDTO filter = new PeopleFilterDTO()
                {
                    PersonalCode = model.PersonalCodeFilter,
                    Email = model.EmailFilter,
                    MemberCode = model.MemberCodeFilter,
                    CardNumber = model.CardNumberFilter,
                    LibrarianCode = model.LibrarianCodeFilter
                };

                List<PeopleIndexItemDTO> indexItems = _personQueriesService
                    .GetIndexItems(filter, model.PageNumber, model.PageSize);

                model.Index = _mapper.Map<List<PersonItemViewModel>>(indexItems);

                model.SetPaginationParameters(
                    _personQueriesService.GetItemsCount(filter),
                     model.PageNumber, model.PageSize);
            }

            return View(model);
        }

        public virtual ActionResult Details(Guid id)
        {
            var isAuthorized = _authorizationService.AuthorizeAsync(
                User, id, OperationAuthorizationRequirements.PersonDetails)
                .Result;

            if (isAuthorized.Succeeded)
            {
                PersonDetailsDTO detailsDTO = _personQueriesService.GetDetailsDTO(id);
                PersonDetailsViewModel model = _mapper.Map<PersonDetailsViewModel>(detailsDTO);

                if (model == null)
                {
                    return NotFound();
                }

                return View(model);
            }
            return Forbid();
        }


        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(PersonCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                PersonCreateDTO createDTO = _mapper.Map<PersonCreateDTO>(model);
                Guid id = _personUpdateService.Create(createDTO);

                if (model.Member)
                    _personUpdateService.AddAsMember(id);

                if (model.Librarian)
                    _personUpdateService.AddAsLibrarian(id);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public virtual ActionResult Edit(Guid id)
        {
            var isAuthorized = _authorizationService.AuthorizeAsync(
                User, id, OperationAuthorizationRequirements.PersonEdit)
                .Result;

            if (isAuthorized.Succeeded)
            {
                PersonEditDTO editDTO = _personQueriesService.GetEditDTO(id);
                PersonEditViewModel model = _mapper.Map<PersonEditViewModel>(editDTO);

                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
            return Forbid();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(PersonEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                PersonEditDTO editDTO = _mapper.Map<PersonEditDTO>(model);
                _personUpdateService.Edit(editDTO);

                if (model.Member && (
                    _identityService.CurrentUserIsInRole(Roles.Admin) ||
                    _identityService.CurrentUserIsInRole(Roles.Librarian)))
                {
                    _personUpdateService.AddAsMember(editDTO.Id);
                }

                if (model.Librarian &&
                    _identityService.CurrentUserIsInRole(Roles.Admin))
                {
                    _personUpdateService.AddAsLibrarian(editDTO.Id);
                }

                if (_identityService.UserExists(editDTO.Email))
                {
                    _identityService.UpdateRoles(editDTO.Email);
                }

                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            return View(model);
        }
    }
}
