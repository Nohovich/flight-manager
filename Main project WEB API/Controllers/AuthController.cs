using Main_Project;
using Main_Project.Facade;
using Main_Project.Login;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Main_project_WEB_API.Controllers
{
    public class AuthController : ApiController
    {
        [Route("api/auth")]
        [HttpPost]
        public IHttpActionResult GetToken([FromBody] JObject jsonDataForToken)
        {
            string userName = jsonDataForToken["userName"].ToString();
            string password = jsonDataForToken["password"].ToString();
            FlyingCenterSystem.GetInstance().TryLogin(userName, password, out ILogin token, out FacadeBase facade);
            return Ok();

        }
    }
}
