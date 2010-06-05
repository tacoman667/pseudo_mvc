using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PseudoMvc {
    public class XMLResponseHandler : IHttpHandler {

        public string Xml { get; set; }

        public XMLResponseHandler(string xml) {
            this.Xml = xml;
        }

        public bool IsReusable {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context) {
            context.Response.Write(this.Xml);
        }
    }
}
