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
    public partial class MembersController
    {
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected MembersController(Dummy d)
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
        public virtual IActionResult Details()
        {
            return new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Details);
        }

        [NonAction]
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public virtual IActionResult AccountStatusChange()
        {
            return new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.AccountStatusChange);
        }

        [NonAction]
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public virtual IActionResult BorrowedBookItems()
        {
            return new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.BorrowedBookItems);
        }

        [NonAction]
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public virtual IActionResult ReservedBookItems()
        {
            return new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.ReservedBookItems);
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public MembersController Actions => MVC.Members;
        [GeneratedCode("R4Mvc", "1.0")]
        public readonly string Area = "";
        [GeneratedCode("R4Mvc", "1.0")]
        public readonly string Name = "Members";
        [GeneratedCode("R4Mvc", "1.0")]
        public const string NameConst = "Members";
        [GeneratedCode("R4Mvc", "1.0")]
        static readonly ActionNamesClass s_ActionNames = new ActionNamesClass();
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames => s_ActionNames;
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Details = "Details";
            public readonly string AccountStatusChange = "AccountStatusChange";
            public readonly string BorrowedBookItems = "BorrowedBookItems";
            public readonly string ReservedBookItems = "ReservedBookItems";
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Details = "Details";
            public const string AccountStatusChange = "AccountStatusChange";
            public const string BorrowedBookItems = "BorrowedBookItems";
            public const string ReservedBookItems = "ReservedBookItems";
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames => s_ViewNames;
            public class _ViewNamesClass
            {
                public readonly string BorrowedBookItems = "BorrowedBookItems";
                public readonly string Details = "Details";
                public readonly string MemberStatusChange = "MemberStatusChange";
                public readonly string ReservedBookItems = "ReservedBookItems";
            }

            public readonly string BorrowedBookItems = "~/Views/Members/BorrowedBookItems.cshtml";
            public readonly string Details = "~/Views/Members/Details.cshtml";
            public readonly string MemberStatusChange = "~/Views/Members/MemberStatusChange.cshtml";
            public readonly string ReservedBookItems = "~/Views/Members/ReservedBookItems.cshtml";
        }

        [GeneratedCode("R4Mvc", "1.0")]
        static readonly ViewsClass s_Views = new ViewsClass();
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public ViewsClass Views => s_Views;
    }

    [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
    public partial class R4MVC_MembersController : Web.Controllers.MembersController
    {
        public R4MVC_MembersController(): base(Dummy.Instance)
        {
        }

        [NonAction]
        partial void DetailsOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, System.Guid id);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult Details(System.Guid id)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Details);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DetailsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void AccountStatusChangeOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, System.Guid id);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult AccountStatusChange(System.Guid id)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.AccountStatusChange);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            AccountStatusChangeOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void AccountStatusChangeOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, Web.ViewModels.Members.MemberStatusChangeViewModel model);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult AccountStatusChange(Web.ViewModels.Members.MemberStatusChangeViewModel model)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.AccountStatusChange);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AccountStatusChangeOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void BorrowedBookItemsOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, System.Guid id);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult BorrowedBookItems(System.Guid id)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.BorrowedBookItems);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            BorrowedBookItemsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void ReservedBookItemsOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, System.Guid id);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult ReservedBookItems(System.Guid id)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.ReservedBookItems);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ReservedBookItemsOverride(callInfo, id);
            return callInfo;
        }
    }
}
#pragma warning restore 1591, 3008, 3009, 0108
