using Main_Project.Facade;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main_project_WEB_API.Controllers
{
    public class RazorPageController : Controller
    {
        AnonymousUserFacade anonymousUserFacade = new AnonymousUserFacade();

        public ActionResult Landing()
        {
            List<FlightRazor> flightsInfo = anonymousUserFacade.LandingFlights().ToList();
            ViewBag.Flights = flightsInfo;

            return View();
        }
        public ActionResult departing()
        {
            List<FlightRazor> flightsInfo = anonymousUserFacade.DeparturesFlights().ToList();
            ViewBag.Flights = flightsInfo;

            return View();
        }
    }
}