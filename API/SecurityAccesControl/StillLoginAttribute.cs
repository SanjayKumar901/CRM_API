using API.BAL;
using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SecurityAccesControl
{
    public class StillLoginAttribute : ActionFilterAttribute
    {
        private IBusinessLayer Business;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Count > 0)
            {
                try
                {
                    Business = new BusinessLayer();
                    var contextModel = context.ActionArguments.SingleOrDefault(p => p.Value != null);
                    dynamic ss = contextModel.Value;
                    var test = ss.Token;
                    if (Business.checkToken(test) == "Not")
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                        new { Controller = "Account", Action = "SessionOut" }
                        ));
                    }
                    Business.Dispose();
                }
                catch(Exception ex)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                        new { Controller = "Account", Action = "SessionOut" }
                        ));
                }
                //var model = contextModel.Values[0].Token;
            }
            //var ss1 = context.ModelState;
            // ss1= context.HttpContext.Request.Body;
            //var ss = context.HttpContext.Request.Headers["Bearer"];
            //ss = context.HttpContext.Request.Headers["Authorization"];
            //context.Result = new RedirectToRouteResult(new
            //   RouteValueDictionary(new { Controller = "Account", Action = "SessionOut" }));
            base.OnActionExecuting(context);
        }
    }
}
