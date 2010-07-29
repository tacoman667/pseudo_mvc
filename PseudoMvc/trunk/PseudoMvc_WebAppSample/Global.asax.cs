using System;
using System.Linq;
using System.Web.Routing;
using PseudoMvc;

namespace PseudoMvc_WebAppSample {
    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            RegisterRoutes(RouteTable.Routes);
            RegisterTypes();

        }

        private void RegisterTypes() {

            RegisterControllers();

        }

        private void RegisterRoutes(RouteCollection routes) {
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.Map("home",
                       "{controller}/{action}/{id}",
                       new { controller = "home", action = "index", id = "" });

        }

        private void RegisterControllers() {
            var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType == typeof(Controller)).ToList();
            foreach (var type in types) {
                IoC.Register(type, type);
            }
        }

    }
}