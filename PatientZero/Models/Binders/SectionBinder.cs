using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PatientZero.Models.Binders
{
    public class SectionBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string type = controllerContext.RequestContext.HttpContext.Request["type"];
            Section section = EntityManager.Instance.InstantiateSection(type);
            foreach (PropertyInfo property in section.GetType().GetProperties())
            {
                property.SetValue(section, controllerContext.RequestContext.HttpContext.Request[property.Name]);
            }

            return section;
        }
    }
}