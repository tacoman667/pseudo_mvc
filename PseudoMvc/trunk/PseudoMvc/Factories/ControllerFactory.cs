using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Reflection;

namespace PseudoMvc.Factories {
    /// <summary>
    /// Factory for creating Controllers based on Route values.
    /// </summary>
    public class ControllerFactory {

        private static ControllerFactory _current;
        public static ControllerFactory Current {
            get {
                if (_current == null)
                    _current = new ControllerFactory();
                return _current;
            }
        }


        static ControllerFactory() {

        }

        /// <summary>
        /// Creates a Controller based on the route values from the URL.
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        public Controller CreateController(Assembly assembly, string controllerName) {

            controllerName = controllerName.ToLower() + "controller";
            var nameSpace = assembly.GetName().Name;

            var type = assembly.GetTypes().Where(t => t.FullName.ToLower().Contains(controllerName)).FirstOrDefault();
            
            Controller controller = (Controller)assembly.CreateInstance(type.FullName);

            return controller;

        }

    }
}
