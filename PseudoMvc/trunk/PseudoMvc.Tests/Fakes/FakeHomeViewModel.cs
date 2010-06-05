using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace PseudoMvc.Tests.Fakes {
    public class FakeHomeViewModel {

        [Required(ErrorMessage="HelloWorld is a required field.")]
        public string HelloWorld { get; set; }

        [RegularExpression(@"([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d", ErrorMessage = "MM/DD/YYYY")]
        public string CreatedDate { get; set; }

        [Range(0, 5)]
        public int SomeInt { get; set; }
        
        public FakeHomeViewModel() {
        }
    }
}
