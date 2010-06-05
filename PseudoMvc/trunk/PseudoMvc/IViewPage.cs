using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoMvc {

    public interface IViewPage<TModel> where TModel : class, new() {

        ViewData<TModel> ViewData { get; set; }

    }
}
