using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace PseudoMvc {

    public interface IController : IController<object> {

    }

    public interface IController<TModel> where TModel : class, new() {

        ViewData<TModel> ViewData { get; set; }
        ModelState ModelState { get; set; }

        object Model { get; set; }

        void Initialize(System.Web.Routing.RequestContext requestContext);

        ViewResult View();
        ViewResult View(TModel model);

    }
}
