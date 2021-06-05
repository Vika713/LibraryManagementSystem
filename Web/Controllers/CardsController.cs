using AutoMapper;
using Business.Authorization;
using Business.Cards;
using Business.Cards.DTOs;
using Common.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Web.ViewModels.Cards;

namespace Web.Controllers
{
    public partial class CardsController : Controller
    {
        private readonly ICardQueriesService _cardQueriesService;
        private readonly ICardUpdateService _cardUpdateService;
        private readonly ICustomAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public CardsController(
            ICardQueriesService cardQueriesService,
            ICardUpdateService cardUpdateService,
            ICustomAuthorizationService authorizationService,
            IMapper mapper)
        {
            _cardQueriesService = cardQueriesService;
            _cardUpdateService = cardUpdateService;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult Create(Guid id)
        {
            CardCreateDTO createDTO = _cardQueriesService.GetCreateDTO(id);
            CardCreateViewModel model = _mapper.Map<CardCreateViewModel>(createDTO);
            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(CardCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _cardUpdateService.MakeAllCardsInactive(model.MemberId);
                CardCreateDTO createDTO = _mapper.Map<CardCreateDTO>(model);
                _cardUpdateService.Create(createDTO);

                return RedirectToAction(
                    nameof(MVC.Members.Details), nameof(MVC.Members), new { id = model.MemberId });
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian + "," + Roles.Member)]
        public virtual ActionResult Block(Guid id)
        {
            var isAuthorized = _authorizationService.AuthorizeAsync(
                User, id, OperationAuthorizationRequirements.CardBlock)
                .Result;

            if (isAuthorized.Succeeded)
            {
                CardBlockDTO blockDTO = _cardQueriesService.GetBlockDTO(id);
                CardBlockViewModel model = _mapper.Map<CardBlockViewModel>(blockDTO);

                return View(model);
            }
            return Forbid();
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian + "," + Roles.Member)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Block(CardBlockViewModel model)
        {
            _cardUpdateService.MakeAllCardsInactive(model.MemberId);

            return RedirectToAction(
                    nameof(MVC.Members.Details), nameof(MVC.Members), new { id = model.MemberId });
        }
    }
}
