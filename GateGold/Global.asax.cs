using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Nexus
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FluentValidationModelValidatorProvider.Configure();
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            if (ticket == null) return;

            var identity = new GenericIdentity(ticket.Name);
            var roles = (ticket.UserData ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            HttpContext.Current.User = new GenericPrincipal(identity, roles);
            System.Threading.Thread.CurrentPrincipal = HttpContext.Current.User;



        }

        protected void Application_Error(object sender, EventArgs e) { var exception = Server.GetLastError(); if (exception == null) return; Nexus.Helpers.Logger.LogError(exception, Request?.Url?.ToString()); }


    }


}
