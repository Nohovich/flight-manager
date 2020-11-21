using Main_Project.Facade;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main_project_WEB_API.Controllers
{
    public class PageController : Controller
    {
        AnonymousUserFacade anonymousUserFacade = new AnonymousUserFacade();

    }
}