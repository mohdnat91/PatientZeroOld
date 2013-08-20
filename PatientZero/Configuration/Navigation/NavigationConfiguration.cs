using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace PatientZero.Configuration.Navigation
{
    public class NavigationSection : ConfigurationSection
    {
        [ConfigurationProperty("links", Options = ConfigurationPropertyOptions.IsRequired)]
        public NavigationLinkCollection Links { get { return (NavigationLinkCollection) this["links"]; } }
    }

    [ConfigurationCollection(typeof(NavigationLinkElement), AddItemName = "link", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class NavigationLinkCollection : BaseConfigurationElementCollection<NavigationLinkElement>
    {
        protected override ConfigurationElement CreateNewElement() 
        {
            return new NavigationLinkElement();
        }

        protected override object GetElementKey(ConfigurationElement element) 
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((NavigationLinkElement)element).DisplayName;
        }

    }

    public class NavigationLinkElement : ConfigurationElement
    {
        [ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired)]
        public string DisplayName { get { return (string)base["name"]; } }

        [ConfigurationProperty("action", Options = ConfigurationPropertyOptions.IsRequired)]
        public ConfigurationTextElement Action { get { return (ConfigurationTextElement)base["action"]; } }

        [ConfigurationProperty("controller", Options = ConfigurationPropertyOptions.IsRequired)]
        public ConfigurationTextElement Controller { get { return (ConfigurationTextElement)base["controller"]; } }

        [ConfigurationProperty("params")]
        public ParamsElementCollection Params { get { return (ParamsElementCollection)this["params"]; } }

        [ConfigurationProperty("icon", Options = ConfigurationPropertyOptions.IsRequired)]
        public ConfigurationTextElement Icon { get { return (ConfigurationTextElement)base["icon"]; } }

        public NavigationLink ToNavigationLink() {
            NavigationLink link = new NavigationLink { DisplayName = DisplayName, Controller = Controller.Value, Action = Action.Value };
            link.Params = new RouteValueDictionary();
            foreach (ParamElement param in Params) {
                link.Params.Add(param.Key, param.Value);
            }
            return link;
        }
    }

    [ConfigurationCollection(typeof(ParamElement), AddItemName = "param", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class ParamsElementCollection : BaseConfigurationElementCollection<ParamElement>
    {
        protected override ConfigurationElement CreateNewElement() 
        {
            return new ParamElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((ParamElement)element).Key;
        }
    }

    public class ParamElement : ConfigurationElement
    {
        [ConfigurationProperty("key", Options=ConfigurationPropertyOptions.IsRequired)]
        public string Key { get { return (string)this["key"]; } }

        [ConfigurationProperty("value", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Value { get { return (string)this["value"]; } }
    }
}