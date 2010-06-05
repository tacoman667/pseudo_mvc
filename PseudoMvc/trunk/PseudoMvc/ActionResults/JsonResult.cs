using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoMvc {
    public class JsonResult :ActionResult {

        public string Json { get; set; }

        public JsonResult(string json) {
            this.Json = json;
        }
    }
}
