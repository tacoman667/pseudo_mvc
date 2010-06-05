using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using PseudoMvc.Factories;
using PseudoMvc.Tests.Fakes;
using System.Web;

namespace PseudoMvc.Tests {
    /// <summary>
    /// Summary description for WebFormRouteHandlerTests
    /// </summary>
    [TestClass]
    public class WebFormRouteHandlerTests {

        [TestMethod]
        public void Controller_Has_Index_Action_Called_Based_On_Route_Data() {

            var context = new RequestContext() { RouteData = new RouteData() };
            context.RouteData.Values.Add("controller", "fakehome");
            context.RouteData.Values.Add("action", "index");

            var controller = ControllerFactory.Current.CreateController(System.Reflection.Assembly.GetExecutingAssembly(), "fakeHome");

            WebFormRouteHandler_Accessor.CallActionOnControllerAndReturnActionName(context, controller);

        }

        [TestMethod]
        public void SetupViewPage_Successful() {
            var controller = ControllerFactory.Current.CreateController(System.Reflection.Assembly.GetExecutingAssembly(), "fakehome");
            var homeViewModel = new FakeHomeViewModel() { HelloWorld = "Hello Mvc!" };
            controller.ViewData.Model = homeViewModel;
            controller.ViewData["action"] = "foo";

            var page = new Fakes.FakePage();
            page.ViewData.Model = new FakeHomeViewModel();
            PseudoMvc.WebFormRouteHandler_Accessor.SetupViewPage(controller, page, page.ViewData);

            Assert.AreEqual(controller.ViewData.Model, page.ViewData.Model, "Page did not get the controller's Model bound to it.");
        }

    }
}
