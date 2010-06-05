using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoMvc {
    public class ModelState {

        public List<String> Errors { get; set; }
        public bool HasErrors { get; set; }


        public ModelState() {
            this.Errors = new List<string>();
            this.HasErrors = false;
        }
    }
}
