using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;


namespace PseudoMvc {
    public class DefaultModelBinder {
        private object arg;
        private NameValueCollection nameValueCollection;
        private ModelValidator validator;

        public ModelState ModelState { get; set; }


        public DefaultModelBinder(object arg, NameValueCollection nameValueCollection) {
            // TODO: Complete member initialization
            this.arg = arg;
            this.nameValueCollection = nameValueCollection;
            this.ModelState = new ModelState();
            this.validator = new ModelValidator(this.ModelState);
        }

        public void Bind() {

            foreach (var p in arg.GetType().GetProperties()) {

                var value = nameValueCollection[p.Name];
                if (value == null) continue;

                p.SetValue(arg, value.ChangeTypeTo(p.PropertyType), null);
            }

            validator.ValidateModel(arg);
            this.ModelState = validator.ModelState;

        }
    }
}
