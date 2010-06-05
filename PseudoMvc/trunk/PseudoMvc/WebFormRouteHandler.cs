using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Security;
using System.Web.Compilation;
using System.Web;
using System.Web.UI;
using System.Web.Routing;
using System.Reflection;
using System.Globalization;
using PseudoMvc.Factories;



namespace PseudoMvc {
    public class WebFormRouteHandler : IRouteHandler {

        public WebFormRouteHandler() { }

        public Assembly ApplicationAssembly { get; set; }

        public IHttpHandler GetHttpHandler(RequestContext requestContext) {

            requestContext.RouteData.DataTokens.Add("appAssembly", ApplicationAssembly);

            foreach (var param in requestContext.RouteData.Values) {
                requestContext.HttpContext.Items[param.Key] = param.Value;
            }



            // Create Controller Instance
            IController controller = GetControllerFromRouteValues(requestContext);

            if (controller == null) return null;

            // Call Initialize on the Controller
            controller.Initialize(requestContext);


            // Call action on the controller
            var actionResult = CallActionOnControllerAndReturnActionName(requestContext, controller) as ActionResult;
            actionResult.requestContext = requestContext;


            // Create page from BuildManager using VirtualPath of the physical aspx file
            // /Views/{controller}/{action}.aspx
            string virtualPath = String.Format("~/Views/{0}/{1}.aspx", requestContext.RouteData.Values["controller"], requestContext.RouteData.Values["action"]);

            IHttpHandler handler = System.Web.HttpContext.Current.CurrentHandler;
            switch (actionResult.GetType().Name) {

                case "ViewResult": {
                    var page = ((ViewResult)actionResult).CreateView(virtualPath);
                    var viewData = page.GetPropertyValue("ViewData");
                    SetupViewPage(controller, page, viewData);
                    handler = page as IHttpHandler;
                    break;
                }

                case "XmlResult": {
                    handler = new XMLResponseHandler((actionResult as XmlResult).Xml);
                    break;
                }

                case "JsonResult": {
                    handler = new JsonResponseHandler((actionResult as JsonResult).Json);
                    break;
                }

                default:
                    break;

            }


            return handler as IHttpHandler;

        }

        private static void SetupViewPage(IController controller, object viewPage, object viewData) {
            // Set the page's ViewData to the ViewData of the controller
            foreach (var item in controller.ViewData) {
                ((Dictionary<string, object>)viewData).Add(item.Key, item.Value);
            }
            viewData.SetPropertyValue("Model", controller.ViewData.Model);


            // Sets the page's FSG Identification if the FSGAuthorizeAttribute is used.
            // Controller setting rulez all.
            var att = controller.GetType().GetCustomAttributes(typeof(FSGAuthorizeAttribute), false).FirstOrDefault();
            if (att != null) {
                ((FSGAuthorizeAttribute)att).setPageIdentification((FrameworkPage)viewPage);
            }
            else {
                var method = controller.GetAllMethods().Where(m => m.Name.ToLower() == controller.ViewData["action"].ToString().ToLower()).FirstOrDefault();
                att = method.GetCustomAttributes(typeof(FSGAuthorizeAttribute), false).FirstOrDefault();
                if (att != null) {
                    ((FSGAuthorizeAttribute)att).setPageIdentification((FrameworkPage)viewPage);
                }
            }
        }

        private static MethodInfo GetControllerMethodToCallFromRequestType(RequestContext requestContext, IController controller, string actionName) {
            
            var methods = controller.GetAllMethods().Where(m => m.Name.ToLower() == actionName.ToLower() && m.IsPublic && m.ReturnType.BaseType == typeof(ActionResult));
            

            foreach (var method in methods) {

                var atts = method.GetCustomAttributes(false);

                foreach (var a in atts) {
                    if (a is HttpGet && requestContext.HttpContext.Request.HttpMethod == "GET")
                        return method;
                    else if (a is HttpPost && requestContext.HttpContext.Request.HttpMethod == "POST")
                        return method;
                    else if (a is HttpDelete && requestContext.HttpContext.Request.HttpMethod == "DELETE")
                        return method;
                }
            }

            if (methods.FirstOrDefault() == null)
                throw new Exception(String.Format("Action: {0} is not defined in the controller.", actionName));

            return methods.First();
        }

        /// <summary>
        /// Figures out the Action to call on the controller based on the REST Url. Then is gets any arguements the action
        /// has and tries to bind the Request.Form values to them. Then the action is called injected with the arguments.
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        private static object CallActionOnControllerAndReturnActionName(RequestContext requestContext, IController controller) {
            var actionName = requestContext.RouteData.Values["action"].ToString();

            var func = GetControllerMethodToCallFromRequestType(requestContext, controller, actionName);

            List<object> parameters = BindArgumentsFromControllerAction(requestContext, controller, func);

            var result = func.Invoke(controller, parameters.ToArray());

            return result;
        }

        /// <summary>
        /// Binds all arguments from the Action method to objects and returns the list.
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controller"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static List<object> BindArgumentsFromControllerAction(RequestContext requestContext, IController controller, MethodInfo func) {
            // Iterate through all arguments of the action method to bind data to them from Request.Form
            List<object> parameters = new List<object>();
            foreach (var p in func.GetParameters()) {
                object arg;
                if (p.ParameterType.Name == "String")
                    arg = String.Empty;
                else
                    arg = Activator.CreateInstance(p.ParameterType);

                // If primitive type or String then just bind the data otherwise bind request data to the class.
                if (arg.GetType().IsPrimitive || arg.GetType().Name == "String") {
                    if (requestContext.HttpContext.Request.Form[p.Name] != null)
                        arg = Convert.ChangeType(requestContext.HttpContext.Request.Form[p.Name], p.ParameterType);
                }
                else {
                    var modelBinder = new DefaultModelBinder(arg, requestContext.HttpContext.Request.Form);
                    modelBinder.Bind();
                    controller.ModelState = modelBinder.ModelState;
                }
                parameters.Add(arg);

            }
            return parameters;
        }

        private static IController GetControllerFromRouteValues(RequestContext requestContext) {
            return ControllerFactory.Current.CreateController(requestContext.RouteData.DataTokens["appAssembly"] as Assembly, requestContext.RouteData.Values["controller"] as string);
        }

        private static string ToProperCase(string text) {

            var textInfo = new CultureInfo("en-US", false).TextInfo;

            return textInfo.ToTitleCase(text);

        }

    }
}