using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using System.Reflection;


namespace PseudoMvc.Factories.Tests {
    /// <summary>
    /// Summary description for ControllerFactoryTests
    /// </summary>
    [TestClass]
    public class ControllerFactoryTests {

        [TestMethod]
        public void ControllerFactory_Current_Is_Not_Null() {
            Assert.IsNotNull(ControllerFactory.Current, "ControllerFactory did not initialize a new Current factory singleton.");
        }

        [TestMethod]
        public void HomeController_Created_With_LowerCase_ControllerName() {
            var controller = ControllerFactory.Current.CreateController(Assembly.GetExecutingAssembly(), "fakehome");
            Assert.IsNotNull(controller, "Controller result is null");
        }
        
        [TestMethod]
        public void HomeController_Created_With_UpperCase_ControllerName() {
            var controller = ControllerFactory.Current.CreateController(Assembly.GetExecutingAssembly(), "fakeHome");
            Assert.IsNotNull(controller, "Controller result is null");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Non_Existant_Controller_Returns_Null() {
            var controller = ControllerFactory.Current.CreateController(Assembly.GetExecutingAssembly(), "blahblah");
            Assert.IsNull(controller, "Controller result is not null");
        }

    }
}
