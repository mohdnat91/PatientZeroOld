using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            string type = controllerContext.RequestContext.HttpContext.Request["section-type"];
            Section section = EntityManager.Instance.InstantiateSection(type);
            foreach (PropertyInfo property in section.GetType().GetProperties())
            {
                object value = Convert(controllerContext.RequestContext.HttpContext.Request[property.Name], property.PropertyType);
                property.SetValue(section, value);
            }

            return section;
        }

        private object Convert(string value, Type dest)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(dest);
            return converter.ConvertFromString(value);
        }
    }
}