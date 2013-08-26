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
        public SectionDefElementCollection Sections { get { return (SectionDefElementCollection)this["sections"]; } }

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

    [ConfigurationCollection(typeof(SectionRefElement), AddItemName = "section", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class EntityElement : BaseConfigurationElementCollection<SectionRefElement>
    {

        public EntityElement() {
            AddElementName = "section";
        }

        [ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Type { get { return (string)this["type"]; } }

        protected override ConfigurationElement CreateNewElement() {
            return new SectionRefElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((SectionRefElement)element).Ref;
        }

        public EntityDefinition ToEntityDefinition() {
            EntityDefinition def = new EntityDefinition();

            def.Type = Type;
            def.Sections = this.Select(s => s.ToSectionReference());

            return def;
        }
    }

    public class SectionDefElement : ConfigurationElement
    {
        [ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Name { get { return (string)this["name"]; } }

        [ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Type { get { return (string)this["type"]; } }

        public SectionDefinition ToSectionDefinition() {
            SectionDefinition def = new SectionDefinition();

            def.Name = Name;
            def.Implementation = System.Type.GetType(Type);

            return def;
        }
    }

    [ConfigurationCollection(typeof(SectionDefElement), AddItemName = "section", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class SectionDefElementCollection : BaseConfigurationElementCollection<SectionDefElement>
    {
        protected override ConfigurationElement CreateNewElement() {
            return new SectionDefElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((SectionDefElement)element).Name;
        }
    }

    public class SectionRefElement : ConfigurationElement
    {
        [ConfigurationProperty("ref", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Ref { get { return (string)this["ref"]; } }

        [ConfigurationProperty("occurs", DefaultValue = "unlimited")]
        public string Occurs { get { return (string)this["occurs"]; } }

        [ConfigurationProperty("initial", DefaultValue = false)]
        public bool IsInitial { get { return (bool)this["initial"]; } }

        public SectionReference ToSectionReference() {
            SectionReference sec = new SectionReference();

            sec.DefinitionName = Ref;
            sec.Occurences = Range.Parse(Occurs);
            sec.IsInitial = IsInitial;

            return sec;
        }
    }
}