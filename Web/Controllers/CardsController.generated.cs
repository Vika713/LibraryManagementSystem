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
    public partial class CardsController
    {
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        protected CardsController(Dummy d)
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
        public virtual IActionResult Create()
        {
            return new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Create);
        }

        [NonAction]
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public virtual IActionResult Block()
        {
            return new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Block);
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public CardsController Actions => MVC.Cards;
        [GeneratedCode("R4Mvc", "1.0")]
        public readonly string Area = "";
        [GeneratedCode("R4Mvc", "1.0")]
        public readonly string Name = "Cards";
        [GeneratedCode("R4Mvc", "1.0")]
        public const string NameConst = "Cards";
        [GeneratedCode("R4Mvc", "1.0")]
        static readonly ActionNamesClass s_ActionNames = new ActionNamesClass();
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames => s_ActionNames;
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Create = "Create";
            public readonly string Block = "Block";
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Create = "Create";
            public const string Block = "Block";
        }

        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames => s_ViewNames;
            public class _ViewNamesClass
            {
                public readonly string Block = "Block";
                public readonly string Create = "Create";
            }

            public readonly string Block = "~/Views/Cards/Block.cshtml";
            public readonly string Create = "~/Views/Cards/Create.cshtml";
        }

        [GeneratedCode("R4Mvc", "1.0")]
        static readonly ViewsClass s_Views = new ViewsClass();
        [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
        public ViewsClass Views => s_Views;
    }

    [GeneratedCode("R4Mvc", "1.0"), DebuggerNonUserCode]
    public partial class R4MVC_CardsController : Web.Controllers.CardsController
    {
        public R4MVC_CardsController(): base(Dummy.Instance)
        {
        }

        [NonAction]
        partial void CreateOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, System.Guid id);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult Create(System.Guid id)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            CreateOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, Web.ViewModels.Cards.CardCreateViewModel model);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult Create(Web.ViewModels.Cards.CardCreateViewModel model)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CreateOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void BlockOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, System.Guid id);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult Block(System.Guid id)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Block);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            BlockOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void BlockOverride(R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult callInfo, Web.ViewModels.Cards.CardBlockViewModel model);
        [NonAction]
        public override Microsoft.AspNetCore.Mvc.ActionResult Block(Web.ViewModels.Cards.CardBlockViewModel model)
        {
            var callInfo = new R4Mvc_Microsoft_AspNetCore_Mvc_ActionResult(Area, Name, ActionNames.Block);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            BlockOverride(callInfo, model);
            return callInfo;
        }
    }
}
#pragma warning restore 1591, 3008, 3009, 0108
