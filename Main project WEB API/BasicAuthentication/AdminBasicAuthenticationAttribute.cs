using Main_Project;
using Main_Project.Exceptions;
using Main_Project.Facade;
using Main_Project.Login;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Main_project_WEB_API.BasicAuthentication
{
    public class AdminBasicAuthenticationAttribute : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
            if (authenticationToken == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            string decodedAuthenticationToken = Encoding.UTF8.GetString(
                Convert.FromBase64String(authenticationToken));

            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            string userName = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];
            try
            {
                FlyingCenterSystem.GetInstance().TryLogin(userName, password, out ILogin token, out FacadeBase facade);
                

                if (token is LoginToken<Admin> == false)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    return;
                }
                
                LoginToken<Admin> adminLoginToken = token as LoginToken<Admin>;
                LoggedInAdministratorFacade adminFacade = facade as LoggedInAdministratorFacade;

                actionContext.Request.Properties["adminLoginToken"] = adminLoginToken;
                actionContext.Request.Properties["adminFacade"] = adminFacade;
            }
            catch (DataNotFoundException)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }
            catch (FlightSystemUnexpectedError)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }
            catch (WrongPasswordException)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }
            catch (Exception ex)
            {
                // TODO:
                // write ex to log.NET (or db file)
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, "Unexpected error. please contact site administrator");
                return;
            }
        }
    }
}