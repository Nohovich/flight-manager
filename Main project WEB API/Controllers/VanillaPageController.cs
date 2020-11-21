using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main_project_WEB_API.Controllers
{
    public class VanillaPageController : Controller
    {
        // GET: VanillaPage
        public ActionResult Index()
        {
            // return file path result
            return new FilePathResult(@"Views\VanillaPage\flightsSearch.html", "text/html");
        }
    }
}