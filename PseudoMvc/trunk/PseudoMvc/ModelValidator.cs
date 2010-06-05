using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PseudoMvc {
    public class ModelValidator {

        public ModelState ModelState { get; set; }


        public ModelValidator(ModelState modelState) {
            this.ModelState = new PseudoMvc.ModelState();
            
        }


        public void ValidateModel(object model) {
            foreach (var p in model.GetType().GetProperties()) {
                // Check if member needs regex validation
                var attribute = (ValidationAttribute)p.GetCustomAttributes(typeof(ValidationAttribute), false).FirstOrDefault();
                
                if (attribute != null) {
                    var value = p.GetValue(model, null);
                    var result = attribute.IsValid(value);
                    if (!result) {
                        this.ModelState.Errors.Add(attribute.ErrorMessage);
                        this.ModelState.HasErrors = true;
                    }
                }
            }

        }
    }
}
