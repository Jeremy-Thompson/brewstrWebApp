using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace brewstrWebApp.Controllers
{
    public class RecipeListController : Controller
    {
        //
        // GET: /RecipeList/

        public ActionResult Index()
        {
            return View();
        }

    }
}
