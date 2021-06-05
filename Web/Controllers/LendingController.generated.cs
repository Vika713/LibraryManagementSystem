// <auto-generated />
// This file was generated by R4Mvc.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the r4mvc.json file (i.e. the settings file), save it and run the generator tool again.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo.Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
#pragma warning disable 1591, 3008, 3009, 0108
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using R4Mvc;

namespace Web.Controllers
{
    public partial class LendingController
    {
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected LendingController(Dummy d)
        {
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(IActionResult result)
        {
            var callInfo = result.GetR4ActionResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<IActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(IActionResult result)
        {
            var callInfo = result.GetR4ActionResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<IActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToPage(IActionResult result)
        {
            var callInfo = result.GetR4ActionResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToPage(Task<IActionResult> taskResult)
        {
            return RedirectToPage(taskResult.Result);
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToPagePermanent(IActionResult result)
        {
            var callInfo = result.GetR4ActionResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToPagePermanent(Task<IActionResult> taskResult)
        {
            return RedirectToPagePermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public virtual IActionResult ReturnFine()
        {
            return new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.ReturnFine);
        }

        [NonAction]
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public virtual IActionResult RenewFine()
        {
            return new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.RenewFine);
        }

        [NonAction]
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public virtual IActionResult ManageRenew()
        {
            return new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.ManageRenew);
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public LendingController Actions => MVC.Lending;
        [GeneratedCode("R4Mvc", "1.0")]
        public readonly string Area = "";
        [GeneratedCode("R4Mvc", "1.0")]
        public readonly string Name = "Lending";
        [GeneratedCode("R4Mvc", "1.0")]
        public const string NameConst = "Lending";
        [GeneratedCode("R4Mvc", "1.0")]
        static readonly ActionNamesClass s_ActionNames = new ActionNamesClass();
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames => s_ActionNames;
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string CheckOut = "CheckOut";
            public readonly string Return = "Return";
            public readonly string ReturnFine = "ReturnFine";
            public readonly string Renew = "Renew";
            public readonly string RenewFine = "RenewFine";
            public readonly string ManageRenew = "ManageRenew";
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string CheckOut = "CheckOut";
            public const string Return = "Return";
            public const string ReturnFine = "ReturnFine";
            public const string Renew = "Renew";
            public const string RenewFine = "RenewFine";
            public const string ManageRenew = "ManageRenew";
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames => s_ViewNames;
            public class _ViewNamesClass
            {
                public readonly string CheckOut = "CheckOut";
                public readonly string Loaned = "Loaned";
                public readonly string Renew = "Renew";
                public readonly string RenewFine = "RenewFine";
                public readonly string Return = "Return";
                public readonly string Returned = "Returned";
                public readonly string ReturnFine = "ReturnFine";
            }

            public readonly string CheckOut = "~/Views/Lending/CheckOut.cshtml";
            public readonly string Loaned = "~/Views/Lending/Loaned.cshtml";
            public readonly string Renew = "~/Views/Lending/Renew.cshtml";
            public readonly string RenewFine = "~/Views/Lending/RenewFine.cshtml";
            public readonly string Return = "~/Views/Lending/Return.cshtml";
            public readonly string Returned = "~/Views/Lending/Returned.cshtml";
            public readonly string ReturnFine = "~/Views/Lending/ReturnFine.cshtml";
        }

        [GeneratedCode("R4Mvc", "1.0")]
        static readonly ViewsClass s_Views = new ViewsClass();
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public ViewsClass Views => s_Views;
    }

    [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
    public partial class R4MVC_LendingController : Web.Controllers.LendingController
    {
        public R4MVC_LendingController(): base(Dummy.Instance)
        {
        }

        [NonAction]
        partial void CheckOutOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult CheckOut()
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.CheckOut);
            CheckOutOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CheckOutOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, Web.ViewModels.Lending.CheckOutViewModel model);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult CheckOut(Web.ViewModels.Lending.CheckOutViewModel model)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.CheckOut);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CheckOutOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void ReturnOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult Return()
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Return);
            ReturnOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ReturnOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, Web.ViewModels.Lending.ReturnViewModel model);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult Return(Web.ViewModels.Lending.ReturnViewModel model)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Return);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ReturnOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void ReturnFineOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, Web.ViewModels.Lending.LendingFineViewModel model);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult ReturnFine(Web.ViewModels.Lending.LendingFineViewModel model)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.ReturnFine);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ReturnFineOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void RenewOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult Renew()
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Renew);
            RenewOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RenewOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, Web.ViewModels.Lending.RenewViewModel model);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult Renew(Web.ViewModels.Lending.RenewViewModel model)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Renew);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            RenewOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void RenewFineOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, Web.ViewModels.Lending.LendingFineViewModel model);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult RenewFine(Web.ViewModels.Lending.LendingFineViewModel model)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.RenewFine);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            RenewFineOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void ManageRenewOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, string bookBarcode);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult ManageRenew(string bookBarcode)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.ManageRenew);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "bookBarcode", bookBarcode);
            ManageRenewOverride(callInfo, bookBarcode);
            return callInfo;
        }
    }
}
#pragma warning restore 1591, 3008, 3009, 0108
