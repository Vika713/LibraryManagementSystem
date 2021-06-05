using AutoMapper;
using Business.Authorization;
using Business.BookItems;
using Business.BookItems.DTOs;
using Business.Identity;
using Common.Constants;
using Common.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.ViewModels.BookItems;

namespace Web.Controllers
{
    public partial class BookItemsController : Controller
    {
        private readonly IBookItemQueriesService _bookItemQueriesService;
        private readonly IBookItemUpdateService _bookItemUpdateService;
        private readonly IIdentityService _identityService;
        private readonly ICustomAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public BookItemsController(
            IBookItemQueriesService bookItemQueriesService,
            IBookItemUpdateService bookItemUpdateService,
            IIdentityService identityService,
            ICustomAuthorizationService authorizationService,
            IMapper mapper)
        {
            _bookItemQueriesService = bookItemQueriesService;
            _bookItemUpdateService = bookItemUpdateService;
            _identityService = identityService;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual IActionResult Index(BookItemsIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                BookItemsFilterDTO filter = new BookItemsFilterDTO()
                {
                    ISBN = model.ISBNFilter,
                    Barcode = model.BarcodeFilter
                };

                List<BookItemsIndexItemDTO> indexItems = _bookItemQueriesService
                    .GetIndexItems(filter, model.PageNumber, model.PageSize);

                model.Index = _mapper.Map<List<BookItemItemViewModel>>(indexItems);

                model.SetPaginationParameters(
                    _bookItemQueriesService.GetItemsCount(filter),
                    model.PageNumber, model.PageSize);
            }

            return View(model);
        }

        [AllowAnonymous]
        public virtual ActionResult IndexByBook(Guid id, int? pageNumber, int? pageSize)
        {
            int pageIndex = pageNumber ?? 1;
            int itemsCount = pageSize ?? Consts.DefaultPageSize;

            List<BookItemsIndexItemDTO> intexItemsDTO = _bookItemQueriesService
                .GetIndexItemsByBook(id, pageIndex, itemsCount);
            List<BookItemItemViewModel> indexItemsModel = _mapper.Map<List<BookItemItemViewModel>>(intexItemsDTO);

            if (indexItemsModel == null)
            {
                return NotFound();
            }

            BookItemsIndexViewModel model = new BookItemsIndexViewModel()
            {
                Index = indexItemsModel
            };

            model.SetPaginationParameters(_bookItemQueriesService.GetItemsCountByBook(id), pageIndex, itemsCount);

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult IndexByRack(Guid id, int? pageNumber, int? pageSize)
        {
            int pageIndex = pageNumber ?? 1;
            int itemsCount = pageSize ?? Consts.DefaultPageSize;

            List<BookItemsIndexItemDTO> indexItemsDTO = _bookItemQueriesService
                .GetIndexItemsByRack(id, pageIndex, itemsCount);
            List<BookItemItemViewModel> indexItemsModel = _mapper.Map<List<BookItemItemViewModel>>(indexItemsDTO);

            if (indexItemsModel == null)
            {
                return NotFound();
            }

            BookItemsIndexViewModel model = new BookItemsIndexViewModel()
            {
                Index = indexItemsModel
            };

            model.SetPaginationParameters(_bookItemQueriesService.GetItemsCountByRack(id), pageIndex, itemsCount);

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult Details(Guid id)
        {
            BookItemDetailsDTO detailsDTO = _bookItemQueriesService.GetDetailsDTO(id);
            BookItemDetailsViewModel model = _mapper.Map<BookItemDetailsViewModel>(detailsDTO);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual IActionResult Create(Guid id)
        {
            BookItemCreateDTO createDTO = _bookItemQueriesService.GetCreateDTO(id);
            BookItemCreateViewModel model = _mapper.Map<BookItemCreateViewModel>(createDTO);

            return View(Views.CreateBookItem, model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateBookItem(BookItemCreateViewModel model)
        {
            BookItemCreateDTO createDTO = _mapper.Map<BookItemCreateDTO>(model);

            if (ModelState.IsValid)
            {
                _bookItemUpdateService.Create(createDTO);

                return RedirectToAction(nameof(Index));
            }

            model = _mapper.Map<BookItemCreateViewModel>(
                _bookItemQueriesService.UpdateRacksSelectList(createDTO));

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult Edit(Guid id)
        {
            BookItemEditDTO editDTO = _bookItemQueriesService.GetEditDTO(id);
            BookItemEditViewModel model = _mapper.Map<BookItemEditViewModel>(editDTO);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(BookItemEditViewModel model)
        {
            BookItemEditDTO editDTO = _mapper.Map<BookItemEditDTO>(model);

            if (ModelState.IsValid)
            {
                _bookItemUpdateService.Edit(editDTO);
                return RedirectToAction(nameof(Index));
            }

            model = _mapper.Map<BookItemEditViewModel>(
                _bookItemQueriesService.UpdateRacksSelectList(editDTO));

            return View(model);
        }

        public virtual IActionResult GetRackNumberSuggestions(DropDownListQuery queryParameters)
        {
            Dictionary<string, string> suggestions = _bookItemQueriesService
                .GetRackNumberList(queryParameters.Query);

            return Json(suggestions.Select(c => new { ID = c.Key, Name = c.Value }));
        }

        public virtual IActionResult GetLocationIdentifierSuggestions(DropDownListQuery queryParameters)
        {
            Dictionary<string, string> suggestions = _bookItemQueriesService
                .GetLocationIdentifierList(queryParameters.Query);

            return Json(suggestions.Select(c => new { ID = c.Key, Name = c.Value }));
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult Delete(Guid id)
        {
            BookItemDeleteDTO deleteDTO = _bookItemQueriesService.GetDeleteDTO(id);
            BookItemDeleteViewModel model = _mapper.Map<BookItemDeleteViewModel>(deleteDTO);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(BookItemDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                _bookItemUpdateService.Delete(model.BookItemId);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Member)]
        public virtual ActionResult Reserve(Guid id)
        {
            BookItemReserveDTO reserveDTO = _bookItemQueriesService.GetReserveDTO(id, _identityService.GetCurrentUserEmail());
            BookItemReserveViewModel model = _mapper.Map<BookItemReserveViewModel>(reserveDTO);

            return View(model);
        }

        [Authorize(Roles = Roles.Member)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Reserve(BookItemReserveViewModel model)
        {
            if (ModelState.IsValid)
            {
                BookItemReserveDTO reserveDTO = _mapper.Map<BookItemReserveDTO>(model);

                _bookItemUpdateService.Reserve(reserveDTO);

                return RedirectToAction(nameof(IndexByBook), new { id = model.BookId });
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian + "," + Roles.Member)]
        public virtual ActionResult CancelReservation(Guid id)
        {
            var isAuthorized = _authorizationService.AuthorizeAsync(
                User, id, OperationAuthorizationRequirements.CancelReservation)
                .Result;

            if (isAuthorized.Succeeded)
            {
                BookItemReserveDTO reserveDTO = _bookItemQueriesService.GetReserveDTO(id);
                BookItemReservationCancelViewModel model = _mapper.Map<BookItemReservationCancelViewModel>(reserveDTO);

                return View(model);
            }

            return Forbid();
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian + "," + Roles.Member)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CancelReservation(BookItemReservationCancelViewModel model)
        {
            _bookItemUpdateService.CancelReservation(model.BookItemId);

            return RedirectToAction(nameof(IndexByBook), new { id = model.BookId });
        }
    }
}
