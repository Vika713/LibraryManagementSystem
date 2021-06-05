using AutoMapper;
using Business.Lending;
using Business.Lending.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Lending;

namespace Web.Controllers
{
    [AllowAnonymous]
    public partial class LendingController : Controller
    {
        private readonly ILendingQueriesService _lendingQueriesService;
        private readonly ILendingUpdateService _lendingService;
        private readonly IMapper _mapper;

        public LendingController(
            ILendingQueriesService lendingQueriesService,
            ILendingUpdateService lendingService,
            IMapper mapper)
        {
            _lendingQueriesService = lendingQueriesService;
            _lendingService = lendingService;
            _mapper = mapper;
        }

        public virtual ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CheckOut(CheckOutViewModel model)
        {
            if (ModelState.IsValid)
            {
                ScanDTO scanDTO = _mapper.Map<ScanDTO>(model);
                _lendingService.Lend(scanDTO);

                return View(Views.Loaned, new DueDateViewModel(_lendingQueriesService.GetDueDate(scanDTO.BookBarcode)));
            }
            else
            {
                return View(model);
            }
        }

        public virtual ActionResult Return()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Return(ReturnViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_lendingQueriesService.IsOverdue(model.BookBarcode))
                {
                    LendingFineDTO fineDTO = _lendingQueriesService.GetFineDTO(model.BookBarcode);
                    LendingFineViewModel fineModel = _mapper.Map<LendingFineViewModel>(fineDTO);

                    return View(Views.ReturnFine, fineModel);
                }
                else
                {
                    _lendingService.Return(model.BookBarcode);

                    return View(Views.Returned);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ReturnFine(LendingFineViewModel model)
        {
            if (ModelState.IsValid)
            {
                _lendingService.Return(model.BookBarcode);

                return View(Views.Returned);
            }
            else
            {
                return View(model);
            }
        }

        public virtual ActionResult Renew()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Renew(RenewViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_lendingQueriesService.IsOverdue(model.BookBarcode))
                {
                    LendingFineDTO fineDTO = _lendingQueriesService.GetFineDTO(model.BookBarcode);
                    LendingFineViewModel fineModel = _mapper.Map<LendingFineViewModel>(fineDTO);
                    return View(Views.RenewFine, fineModel);
                }
                else
                {
                    return ManageRenew(model.BookBarcode);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult RenewFine(LendingFineViewModel model)
        {
            if (ModelState.IsValid)
            {
                return ManageRenew(model.BookBarcode);
            }
            else
            {
                return View(model);
            }
        }

        public virtual ActionResult ManageRenew(string bookBarcode)
        {
            if (_lendingQueriesService.BookIsReserved(bookBarcode))
            {
                _lendingService.Return(bookBarcode);

                return View(Views.Returned);
            }
            else
            {
                _lendingService.Renew(bookBarcode);

                return View(Views.Loaned, new DueDateViewModel(_lendingQueriesService.GetDueDate(bookBarcode)));
            }
        }
    }
}
