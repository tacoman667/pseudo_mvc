using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;

namespace PseudoMvc.Tests {
    /// <summary>
    /// Summary description for ViewPageTests
    /// </summary>
    [TestClass]
    public class ViewPageTests {

        public ViewPage<Fakes.FakeHomeViewModel> Page { get; set; }


        [TestInitialize]
        public void Initializer() {
            Page = new Fakes.FakePage();
        }

        [TestMethod]
        public void Is_TypeOf_Page() {
            Assert.IsInstanceOfType(Page, typeof(System.Web.UI.Page));
            Assert.IsInstanceOfType(Page, typeof(IRoutablePage));
        }

        [TestMethod]
        public void ViewData_Initialized_In_Constructor() {
            Assert.IsNotNull(Page.ViewData);
        }

        [TestMethod]
        public void Html_Helper_Property_Initialized_In_Constructor() {
            Assert.IsNotNull(Page.Html);
        }

        [TestMethod]
        public void Init_Called_Creates_Html_Object() {
            var page = new Fakes.FakePage() { Html = null };
            Assert.IsNull(page.Html);

            page.Init();
            Assert.IsNotNull(page.Html);
        }

        [TestMethod]
        public void Model_Property_Equals_ViewData_Model() {
            Assert.AreEqual(Page.ViewData.Model, Page.Model);
        }

        [TestMethod]
        public void Page_Created_With_A_ViewModel_Initializes_Properly() {
            var page = new Fakes.FakePage();
            Assert.IsInstanceOfType(page.Model, typeof(Fakes.FakeHomeViewModel));
        }

    }
}
