using System;
namespace PseudoMvc {
    interface IViewData<TModel> where TModel : class, new() {
        
        TModel Model { get; set; }

    }
}
