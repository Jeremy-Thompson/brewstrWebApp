using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using brewstrWebApp.Models;

namespace brewstrWebApp.Controllers
{
    public class RecipeController : Controller
    {
        //
        // GET: /Recipe/

        public ActionResult Index(Recipe recipe)
        {
            TempData["MyRecipe"] = recipe;
            return View();
        }

    }
}
