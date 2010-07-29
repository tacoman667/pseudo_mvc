using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoMvc.Tests.Fakes {
    class FakeHomeController : Controller {

        public ViewResult Index() {
            return View();
        }

        public ViewResult Foo(FakeHomeViewModel bar) {
            return View(bar);
        }

        public ViewResult Bar(string HelloMvc, int Id) {
            var model = new FooBar() { HelloMvc = HelloMvc, Id = Id };
            return View(model);
        }

    }

    public class FooBar {
        public string HelloMvc { get; set; }
        public int Id { get; set; }
    }
}
