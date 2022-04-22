using GiellyGreen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GiellyGreen.Controllers
{
    public class LoginController : ApiController
    {
        // POST /login
        [Route("login")]
        public JsonResponse Post(LoginViewModel model)
        {
            if (model.Username == WebConfigurationManager.AppSettings["Username"] && model.Password == WebConfigurationManager.AppSettings["LoginPassword"])
            {
                return new JsonResponse()
                {
                    ResponseStatus = 1,
                    Result = "OK",
                    Message = "Login Successfull!"
                };
            }
            else
            {
                return new JsonResponse()
                {
                    ResponseStatus = 2,
                    Result = "Fail",
                    Message = "Invalid user credentials!"
                };
            }    
        }
    }
}
