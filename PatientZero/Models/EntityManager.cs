using PatientZero.Configuration.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace PatientZero.Models
{
    public class EntityManager
    {
        private static readonly Lazy<EntityManager> _instance = new Lazy<EntityManager>(() => new EntityManager(), true);

        public static EntityManager Instance { get { return _instance.Value; } }

        private Dictionary<string, SectionDefinition> sections;
        private Dictionary<string, EntityDefinition> entities;

        public EntityManager() 
        {
            sections = new Dictionary<string, SectionDefinition>();
            entities = new Dictionary<string, EntityDefinition>();

            EntityManagementSection conf = (EntityManagementSection)WebConfigurationManager.GetSection("entityManagement");

            sections = conf.Sections.Select(s => s.ToSectionDefinition()).ToDictionary(s => s.Name);

            entities = conf.Entities.Select(e => e.ToEntityDefinition()).ToDictionary(e => e.Type);
        }

        public Entity InstantiateEntity(string type) 
        {
            EntityDefinition def = entities[type];

            Entity entity = new Entity { Type = type, Sections = new List<Section>() };
            foreach (SectionReference section in def.Sections.Where(s => s.IsInitial)) 
            {
                entity.Sections.Add(InstantiateSection(section.DefinitionName));
            }

            return entity;
        }

        public Section InstantiateSection(string name)
        {
            return (Section)Activator.CreateInstance(sections[name].Implementation);
        }
    }
}