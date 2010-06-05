using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using PseudoMvc;
using PseudoMvc_WebAppSample.Controllers;

namespace PseudoMvc_WebAppSample {
    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            RegisterRoutes(RouteTable.Routes);

        }

        private void RegisterRoutes(RouteCollection routes) {
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.Map("home",
                       "{controller}/{action}/{id}",
                       new { controller = "home", action = "index", id = "" });

        }

        protected void Session_Start(object sender, EventArgs e) {

        }

        protected void Application_BeginRequest(object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

        }

        protected void Application_Error(object sender, EventArgs e) {

        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

        }
    }
}