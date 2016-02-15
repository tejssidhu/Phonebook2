using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phonebook.UI.Controllers
{
    public class HomeController : Controller
    {
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
			//if (Request != null && Request.Path != null && Request.Path.EndsWith("/"))
			//	if (Request.Url != null) return RedirectPermanent(Request.Url.ToString().TrimEnd('/'));

            return View();
        }

    }
}
