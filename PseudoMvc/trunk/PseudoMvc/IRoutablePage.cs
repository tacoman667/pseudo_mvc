using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace PseudoMvc {
    public interface IRoutablePage {

        RequestContext RequestContext {get;set;}

    }
}
