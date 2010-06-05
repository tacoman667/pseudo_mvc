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
    [TestClass]
    public class ControllerIntegrationTests {

        public RequestContext context { get; set; }


        [TestInitialize]
        public void Initialize() {
        }


        [TestMethod]
        public void Index_Returns_New_ActionResult_In_The_Model() {
            var controller = ControllerFactory.Current.CreateController(Assembly.GetExecutingAssembly(), "fakehome");
            Assert.IsNotNull(controller, "Controller was not created.");

            var result = controller.GetType().GetMethod("Index").Invoke(controller, null);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Foo_Action_Sets_Model_Correctly() {
            var controller = ControllerFactory.Current.CreateController(Assembly.GetExecutingAssembly(), "fakehome");
            Assert.IsNotNull(controller, "Controller was not created.");
            var model = new Fakes.FakeHomeViewModel();
            var result = controller.GetType().GetMethod("Foo").Invoke(controller, new List<object>() { model }.ToArray());
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(model, controller.Model);
        }

        [TestMethod]
        public void Bar_Action_Sets_Model_Correctly() {
            var controller = ControllerFactory.Current.CreateController(Assembly.GetExecutingAssembly(), "fakehome");
            Assert.IsNotNull(controller, "Controller was not created.");

            var result = controller.GetType().GetMethod("Bar").Invoke(controller, new List<object>() { "Hello Mvc!", 1 }.ToArray());


            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual("Hello Mvc!", (controller.Model as FooBar).HelloMvc);
            Assert.AreEqual(1, (controller.Model as FooBar).Id);
        }

    }
}
