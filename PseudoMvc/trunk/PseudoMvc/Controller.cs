using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PseudoMvc {
    public class Controller : IController {

        public ViewData<object> ViewData {
            get;
            set;
        }

        public object Model {
            get { return this.ViewData.Model; }
            set { this.ViewData.Model = value; }
        }

        public ModelState ModelState {
            get;
            set;
        }

        public Controller() {
            this.ViewData = new ViewData();
            this.ModelState = new ModelState();
        }

        public virtual void Initialize(System.Web.Routing.RequestContext requestContext) {

            foreach (var param in requestContext.RouteData.Values) {
                this.ViewData.Add(param.Key, param.Value as string);
            }

        }

        /// <summary>
        /// Returns a ViewResult object.  ViewData.Model is set by Controller.
        /// </summary>
        /// <returns>ViewResult</returns>
        public ViewResult View() {
            return new ViewResult();
        }

        /// <summary>
        /// Returns a ViewResult object. Model passed in and set to the ViewData.Model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ViewResult</returns>
        public ViewResult View(object model) {
            this.ViewData.Model = model;
            return new ViewResult();
        }

        /// <summary>
        /// Returns XmlResult object.  Uses the controller's ViewData.Model as the Xml string.
        /// Usually used for AJAX operations.
        /// </summary>
        /// <returns>XmlResult</returns>
        public XmlResult Xml() {
            return new XmlResult(Model as string);
        }

        /// <summary>
        /// Returns XmlResult object.  Uses supplied Xml string in the Response.
        /// Usually used for AJAX operations.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>XmlResult</returns>
        public XmlResult Xml(string xml) {
            return new XmlResult(xml);
        }

        /// <summary>
        /// Returns JsonResult object.  Uses the controller's ViewData.model to serialize into a Json string for the Response.
        /// Usually used for AJAX operations.
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult Json() {
            var result = PseudoMvc.Json.JsonConvert.SerializeObject(Model);
            return new JsonResult(result);
        }

        /// <summary>
        /// Returns JsonResult object.  Uses supplied model object to serialize into Json string for the Response.
        /// Usually used for AJAX operations.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Json(object model) {
            var result = PseudoMvc.Json.JsonConvert.SerializeObject(model);
            return new JsonResult(result);
        }

    }
}
