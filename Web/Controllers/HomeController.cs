using AutoMapper;
using Business.Identity;
using Common.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.ViewModels;
using Web.ViewModels.Identity;

namespace Web.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public HomeController(
            IIdentityService identityService,
            IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public virtual IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public virtual IActionResult Privacy()
        {
            return View();
        }

        public virtual IActionResult Profile()
        {
            ProfileNavigationViewModel model = _mapper.Map<ProfileNavigationViewModel>(
                _identityService.GetProfileNavigationDTO());

            return View(model);
        }

        [AllowAnonymous]
        public virtual IActionResult LendingMenu()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public virtual IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
