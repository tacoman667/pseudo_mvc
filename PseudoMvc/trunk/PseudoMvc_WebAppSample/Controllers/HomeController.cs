using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PseudoMvc_WebAppSample.Models;
using PseudoMvc;

namespace PseudoMvc_WebAppSample.Controllers {

    public class HomeController : PseudoMvc.Controller {


        public JsonResult Index() {
            Models.HomeViewModel model = new Models.HomeViewModel();

            for (int i = 0; i < 5; i++) {
                this.ViewData.Add(i.ToString(), i);
            }

            List<Object> list = new List<object>();

            list.Add(new { Name = "Jeremy Hardin", Email = "Jeremy.Hardin@coair.com" });
            list.Add(new { Name = "Terry Tsay", Email = "Terry.Tsay@coair.com" });


            return Json(list);
        }

        [HttpPost]
        public ViewResult Index(HomeViewModel model) {

            model.SubmittedValue = model.HelloWorld;

            if (ModelState.HasErrors)
                throw new Exception(ModelState.Errors.Aggregate((a, b) => { return a + ", " + b; }));

            return View(model);

        }

        [HttpGet]
        public ViewResult Foo() {
            return View();
        }

        [HttpPost]
        public ViewResult Foo(HomeViewModel model) {
            return View();
        }

        [HttpGet]
        public ViewResult Bar() {
            var model = new HomeViewModel() {
                HelloWorld = "Hello Mvc!"
            };
            return View(model);
        }

        [HttpPost]
        public ViewResult Bar(string HelloMvc, int Id) {
            var model = new HomeViewModel();
            model.SubmittedValue = "I Was Submitted!";
            return View(model);
        }

        [HttpGet]
        [FSGAuthorize("FSGMaintenance", "bull", "bulledit")]
        public ViewResult Baz() {

            var model = new List<int>();
            5.Times( (x) => { model.Add(x); } );
            return View(model);

        }

    }
}