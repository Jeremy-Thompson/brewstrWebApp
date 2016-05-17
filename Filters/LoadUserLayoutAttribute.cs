﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace brewstrWebApp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LoadUserLayoutAttribute : ActionFilterAttribute
    {
        private string _UserLayout;
        public LoadUserLayoutAttribute()
        {
            _UserLayout = "~/Views/Shared/_UserLayout.cshtml";
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                result.MasterName = _UserLayout;
            }
        }
    }
}
