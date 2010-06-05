using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PseudoMvc;
using System.Collections.Specialized;


namespace PseudoMvc.Tests {
    /// <summary>
    /// Summary description for DefaultModelBinderTests
    /// </summary>
    [TestClass]
    public class DefaultModelBinderTests {

        [TestMethod]
        public void Bind_Model_From_Request_Form_Values_Success() {

            var model = new Fakes.FakeHomeViewModel();
            var values = new NameValueCollection();
            values.Add("HelloWorld", "Hello Mvc!");

            var binder = new DefaultModelBinder(model, values);

            binder.Bind();

            Assert.IsFalse(binder.ModelState.HasErrors);
            Assert.AreEqual("Hello Mvc!", model.HelloWorld);
        }

        [TestMethod]
        public void Model_Validation_For_Required_Field_Missing_Fails() {
            var model = new Fakes.FakeHomeViewModel();
            var values = new NameValueCollection();
            var binder = new DefaultModelBinder(model, values);
            binder.Bind();
            Assert.IsTrue(binder.ModelState.HasErrors);
        }

        [TestMethod]
        public void Model_Validation_For_Regular_Expression_Check_Fails() {
            var model = new Fakes.FakeHomeViewModel() { HelloWorld = "Hello Mvc!", CreatedDate = "11/1/2" };
            var values = new NameValueCollection();
            var binder = new DefaultModelBinder(model, values);
            binder.Bind();
            Assert.IsTrue(binder.ModelState.HasErrors);
        }

        [TestMethod]
        public void Model_Validation_For_Regular_Expression_Check_Passes() {
            var model = new Fakes.FakeHomeViewModel() { HelloWorld = "Hello Mvc!", CreatedDate = "11/1/2010" };
            var values = new NameValueCollection();
            var binder = new DefaultModelBinder(model, values);
            binder.Bind();
            Assert.IsFalse(binder.ModelState.HasErrors);
        }

        [TestMethod]
        public void Model_Validation_For_Range_Check_Fails() {
            var model = new Fakes.FakeHomeViewModel() { HelloWorld = "Hello Mvc!", CreatedDate = "11/1/2010", SomeInt = 10 };
            var values = new NameValueCollection();
            var binder = new DefaultModelBinder(model, values);
            binder.Bind();
            Assert.IsTrue(binder.ModelState.HasErrors);
        }

        [TestMethod]
        public void Model_Validation_For_Range_Check_Passes() {
            var model = new Fakes.FakeHomeViewModel() { HelloWorld = "Hello Mvc!", CreatedDate = "11/1/2010", SomeInt = 1 };
            var values = new NameValueCollection();
            var binder = new DefaultModelBinder(model, values);
            binder.Bind();
            Assert.IsFalse(binder.ModelState.HasErrors);
        }

    }
}
