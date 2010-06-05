using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PseudoMvc {
    public class JsonResponseHandler : IHttpHandler {

        public string Json { get; set; }

        public JsonResponseHandler(string json) {
            this.Json = json;
        }

        public bool IsReusable {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context) {
            context.Response.Write(this.Json);
        }
    }
}
