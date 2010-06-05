using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoMvc {
    public class XmlResult : ActionResult {

        public string Xml { get; set; }


        public XmlResult(string xml) {
            this.Xml = xml;
        }
    }
}
