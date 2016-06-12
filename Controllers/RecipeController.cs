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

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult OpenRecipe(Recipe recipe)
        {
            return View("~/Views/Recipe/Index.cshtml", recipe);
        }

    }
}
