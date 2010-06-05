using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.Compilation;

namespace PseudoMvc {
    public class ViewResult : ActionResult {

        public object CreateView(string virtualPath) {

            var page = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(Page));
            return page;

        }

    }
}
