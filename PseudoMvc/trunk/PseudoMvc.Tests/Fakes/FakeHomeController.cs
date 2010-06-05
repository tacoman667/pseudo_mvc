using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoMvc.Tests.Fakes {
    class FakeHomeController : Controller {

        public ActionResult Index() {
            return View();
        }

        public ActionResult Foo(FakeHomeViewModel bar) {
            return View(bar);
        }

        public ActionResult Bar(string HelloMvc, int Id) {
            var model = new FooBar() { HelloMvc = HelloMvc, Id = Id };
            return View(model);
        }

    }

    public class FooBar {
        public string HelloMvc { get; set; }
        public int Id { get; set; }
    }
}
