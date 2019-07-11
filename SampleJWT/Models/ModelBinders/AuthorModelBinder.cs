using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace SampleJWT.Models.ModelBinders {
    public class AuthorModelBinder : IModelBinder {

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext) {
            if (bindingContext.ModelType != typeof(Author)) {
                return false;
            }
            ValueProviderResult val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (val == null) {
                return false;
            }
            string key = val.RawValue as string;
            if (key == null) {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Wrong value type");
                return false;
            }

            Author result;
            if (Author.TryParse(key, out result)) {
                bindingContext.Model = result;
                return true;
            }
            bindingContext.ModelState.AddModelError(
            bindingContext.ModelName, "Cannot convert value to Author");
            return false;
        }
    }
}