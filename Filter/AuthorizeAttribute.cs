using JWTEXAMPLE.Helpers;
using JWTEXAMPLE.Model;
using JWTEXAMPLE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTEXAMPLE.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationkey = context.HttpContext.Request.Headers["Authorization"].ToString().Split(' ');
            var user = (User)context.HttpContext.Items["User"];
            string token = string.Empty;
            if (context.HttpContext.Session.Get("token") != null)
            {
                token = Encoding.UTF8.GetString(context.HttpContext.Session.Get("token"));

            }

            if (authorizationkey.Count() <= 1)
            {
                if (authorizationkey[0] == token)
                {
                    context.Result = new JsonResult(new { message = "unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }

            if (authorizationkey.Count() > 1)
            {
                if (token != authorizationkey[1])
                {
                    context.Result = new JsonResult(new { message = "unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
                
            
        }
    }
}
