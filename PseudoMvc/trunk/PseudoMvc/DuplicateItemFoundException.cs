using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoMvc {
    public class DuplicateItemFoundException : Exception {

        public DuplicateItemFoundException(string message) : base(message) {

        }
    }
}
