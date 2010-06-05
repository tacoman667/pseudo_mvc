using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;


namespace PseudoMvc {
    public abstract class ActionResult {

        public RequestContext requestContext { get; set; }

    }
}
