using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PseudoMvc.Tests {

    internal interface IFoo { }
    internal class Foo : IFoo { }

    [TestClass]
    public class IoC_Tests {

        [TestMethod]
        public void IoC_Registers_And_Resolves_Type_With_One_Type() {
            IoC.Register<Foo>();
            var newObject = IoC.Resolve<Foo>();
            Assert.IsNotNull(newObject);
            Assert.IsInstanceOfType(newObject, typeof(Foo));
            IoC.CleanupTypes();
        }

        [TestMethod]
        public void IoC_Registers_And_Resolves_Type() {
            IoC.Register<IFoo, Foo>();
            var newObject = IoC.Resolve<IFoo>();
            Assert.IsNotNull(newObject);
            Assert.IsInstanceOfType(newObject, typeof(Foo));
            IoC.CleanupTypes();
        }

        [TestMethod]
        public void IoC_Resolves_Type_From_String_Argument() {
            IoC.Register<Foo, Foo>();
            var newObject = IoC.ResolveFromName("Foo") as IFoo;
            Assert.IsNotNull(newObject);
            Assert.IsInstanceOfType(newObject, typeof(Foo));
            IoC.CleanupTypes();
        }
    }
}
