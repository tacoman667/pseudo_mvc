using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using PseudoMvc.Factories;
using PseudoMvc.Tests.Fakes;
using System.Reflection;


namespace PseudoMvc.Tests.Controllers {
    /// <summary>
    /// Summary description for ControllerTests
    /// </summary>
    [TestClass]
    public class ControllerUnitTests {

        public RequestContext context { get; set; }


        [TestInitialize]
        public void Initialize() {
            context = new RequestContext() { RouteData = new RouteData() };
            context.RouteData.Values.Add("controller", "");
            context.RouteData.Values.Add("action", "");
        }

        private IController CreateFakeHomeController() {
            context.RouteData.Values["controller"] = "fakehome";
            var controller = ControllerFactory.Current.CreateController(Assembly.GetCallingAssembly(), "fakehome");
            return controller;
        }


        [TestMethod]
        public void Controller_Is_TypeOf_IController() {
            var controller = CreateFakeHomeController();
            Assert.IsInstanceOfType(controller, typeof(IController));
        }

        [TestMethod]
        public void ViewData_Initialized_In_Contructor() {
            var controller = CreateFakeHomeController();
            Assert.IsNotNull(controller.ViewData);
        }

        [TestMethod]
        public void ModelState_Initialized_In_Constructor() {
            var controller = CreateFakeHomeController();
            Assert.IsNotNull(controller.ModelState);
        }

        [TestMethod]
        public void View_Method_Returns_ActionResult() {
            var controller = CreateFakeHomeController();
            Assert.IsInstanceOfType(controller.View(), typeof(ActionResult));
            Assert.IsInstanceOfType(controller.View(new object()), typeof(ActionResult));
        }

        [TestMethod]
        public void Model_Property_Equals_ViewData_Model() {
            var controller = CreateFakeHomeController();
            Assert.AreEqual(controller.ViewData.Model, controller.Model);
        }

    }
}
