using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoMvc {

    public class ViewData : ViewData<object> {

    }

    public class ViewData<TModel> : Dictionary<string, object> where TModel : class, new() {

        public TModel Model { get; set; }

        public ViewData() {
            this.Model = new TModel();
        }

    }
}
