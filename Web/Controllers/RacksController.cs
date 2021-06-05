using AutoMapper;
using Business.Racks;
using Business.Racks.DTOs;
using Common.Constants;
using Common.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web.ViewModels.Racks;

namespace Web.Controllers
{
    public partial class RacksController : Controller
    {
        private readonly IRackQueriesService _rackQueriesService;
        private readonly IRackUpdateService _rackUpdateService;
        private readonly IMapper _mapper;

        public RacksController(
            IRackQueriesService rackQueriesService,
            IRackUpdateService rackUpdateService,
            IMapper mapper)
        {
            _rackQueriesService = rackQueriesService;
            _rackUpdateService = rackUpdateService;
            _mapper = mapper;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual IActionResult Index(RacksIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                RacksFilterDTO filter = new RacksFilterDTO()
                {
                    Number = model.NumberFilter,
                    LocationId = model.LocationIdFilter
                };

                List<RacksIndexItemDTO> indexItems = _rackQueriesService
                    .GetIndexItems(filter, model.PageNumber, model.PageSize);

                model.Index = _mapper.Map<List<RackItemViewModel>>(indexItems);

                model.SetPaginationParameters(
                    _rackQueriesService.GetItemsCount(filter),
                    model.PageNumber,
                    model.PageSize);
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        public virtual ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(RackCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                RackCreateDTO createDTO = _mapper.Map<RackCreateDTO>(model);
                _rackUpdateService.Create(createDTO);

                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public virtual ActionResult Edit(Guid id, int? pageNumber, int? pageSize)
        {
            int pageIndex = pageNumber ?? 1;
            int itemsCount = pageSize ?? Consts.DefaultPageSize;

            RackEditDTO editDTO = _rackQueriesService.GetEditDTO(id, pageIndex, itemsCount);
            RackEditViewModel model = _mapper.Map<RackEditViewModel>(editDTO);

            model.SetPaginationParameters(
                _rackQueriesService.GetBookItemsCountByRack(id), pageIndex, itemsCount);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(RackEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                RackEditDTO editDTO = _mapper.Map<RackEditDTO>(model);
                _rackUpdateService.Edit(editDTO);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Admin)]
        public virtual ActionResult Delete(Guid id)
        {
            RackDeleteDTO deleteDTO = _rackQueriesService.GetDeleteDTO(id);
            RackDeleteViewModel model = _mapper.Map<RackDeleteViewModel>(deleteDTO);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(Guid id)
        {
            _rackUpdateService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
