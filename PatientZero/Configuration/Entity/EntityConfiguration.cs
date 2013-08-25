using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PatientZero.Configuration.Entity
{
    public class EntityManagementSection : ConfigurationSection
    {
        [ConfigurationProperty("sections", Options = ConfigurationPropertyOptions.IsRequired)]
        public SectionDefinitionCollection Sections { get { return (SectionDefinitionCollection)this["sections"]; } }

        [ConfigurationProperty("entities", Options = ConfigurationPropertyOptions.IsRequired)]
        public EntityElementCollection Entities { get { return (EntityElementCollection)this["entities"]; } }
    }

    [ConfigurationCollection(typeof(EntityElement), AddItemName = "entity", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class EntityElementCollection : BaseConfigurationElementCollection<EntityElement>
    {
        protected override ConfigurationElement CreateNewElement() {
            return new EntityElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((EntityElement)element).Type;
        }
    }

    public class EntityElement : ConfigurationElement
    {
        [ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Type { get { return (string)this["type"]; } }

        [ConfigurationProperty("sections", Options = ConfigurationPropertyOptions.IsRequired)]
        public SectionElementCollection Sections { get { return (SectionElementCollection)this["sections"]; } } 
    }

    [ConfigurationCollection(typeof(EntityElement), AddItemName = "section", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class SectionElementCollection : BaseConfigurationElementCollection<SectionElement>
    {
        protected override ConfigurationElement CreateNewElement() {
            return new SectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((SectionElement)element).Ref;
        }
    }

    public class SectionElement : ConfigurationElement
    {
        [ConfigurationProperty("ref", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Ref { get { return (string)this["ref"]; } }

        [ConfigurationProperty("occurs", DefaultValue = "unlimited")]
        public string Occurs { get { return (string)this["occurs"]; } }

        [ConfigurationProperty("initial", DefaultValue = false)]
        public bool IsInitial { get { return (bool)this["initial"]; } }
    }

    [ConfigurationCollection(typeof(SectionDefinitionElement), AddItemName = "section", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class SectionDefinitionCollection : BaseConfigurationElementCollection<SectionDefinitionElement>
    {
        protected override ConfigurationElement CreateNewElement() {
            return new SectionDefinitionElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((SectionDefinitionElement)element).Name;
        }
    }

    public class SectionDefinitionElement : ConfigurationElement
    {
        [ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Name { get { return (string)this["name"]; } }

        [ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Type { get { return (string)this["type"]; } }

        [ConfigurationProperty("view", Options = ConfigurationPropertyOptions.IsRequired)]
        public string View { get { return (string)this["view"]; } }
    }
}