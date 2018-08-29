using System;
using System.Web.Http.Controllers;


namespace TomNet.Web.WebApi.ModelBinders
{
    public class MvcStyleBindingAttribute : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            controllerSettings.Services.Replace(typeof(IActionValueBinder), new MvcActionValueBinder());
        }
    }
}
