using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PseudoMvc_WebAppSample.Models {
    public class HomeViewModel {

        [Required(ErrorMessage="HelloWorld is required.")]
        public string HelloWorld { get; set; }

        public string SubmittedValue { get; set; }

        public HomeViewModel() {
        }

    }
}