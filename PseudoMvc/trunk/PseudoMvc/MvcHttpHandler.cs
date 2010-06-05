using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;

namespace PseudoMvc {
    public class MvcHttpHandler : UrlRoutingHandler {
        protected override void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext) {
            if (httpHandler == null) {
                throw new ArgumentNullException("httpHandler");
            }

            httpHandler.ProcessRequest(HttpContext.Current);
        }
    }
}
