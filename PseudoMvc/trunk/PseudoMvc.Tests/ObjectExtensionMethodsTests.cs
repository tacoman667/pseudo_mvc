using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PseudoMvc.Tests {
    /// <summary>
    /// Summary description for ObjectExtensionMethodsTests
    /// </summary>
    [TestClass]
    public class ObjectExtensionMethodsTests {

        internal class Foo {

            public string StringProperty { get; set; }
            public bool BooleanProperty { get; set; }
            public System.Nullable<Int32> NullableInt32Property { get; set; }

        }

        [TestMethod]
        public void SetPropertyValue_Sets_String_Property() {

            var foo = new Foo();
            foo.SetPropertyValue("StringProperty", "Hello Mvc!");

            Assert.IsNotNull(foo.StringProperty);

        }

        [TestMethod]
        public void SetPropertyValue_Sets_Boolean_Property() {

            var foo = new Foo();
            foo.SetPropertyValue("BooleanProperty", true);

            Assert.IsTrue(foo.BooleanProperty);
            
        }

        [TestMethod]
        public void SetPropertyValue_Sets_Nullable_Property() {

            var foo = new Foo() { NullableInt32Property = 32 };
            foo.SetPropertyValue("NullableInt32Property", null);

            Assert.IsNull(foo.NullableInt32Property);
        }

        [TestMethod]
        public void SetPropertyValue_Assigns_Nullable_Value_To_Property() {

            var foo = new Foo();
            System.Nullable<Boolean> val = true;
            foo.SetPropertyValue("BooleanProperty", val);

            Assert.IsTrue(foo.BooleanProperty);
        }

        [TestMethod]
        public void SetPropertyValue_Converts_String_To_Boolean_And_Sets_Value() {

            var foo = new Foo();
            foo.SetPropertyValue("BooleanProperty", "true");

            Assert.IsTrue(foo.BooleanProperty);

        }


    }
}
