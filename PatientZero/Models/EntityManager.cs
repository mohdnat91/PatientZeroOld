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

        private Dictionary<string, List<SectionDefinition>> mapping;

        private EntityManager() {
            EntityManagementSection conf = (EntityManagementSection)WebConfigurationManager.GetSection("entityManagement");

            mapping = new Dictionary<string, List<SectionDefinition>>();

            foreach (EntityElement entity in conf.Entities) {
                mapping[entity.Type] = new List<SectionDefinition>();
                mapping[entity.Type].AddRange(from s in entity.Sections join sd in conf.Sections on s.Ref equals sd.Name select new SectionDefinition(sd, s));
            }
        }

        public Entity Instantiate(string type) {
            type = type.ToLower();
            Entity entity = new Entity { Type = type, Sections = new List<Section>() };
            (from s in mapping[type] where s.IsInitial select s.Instantiate()).ToList().ForEach(s => entity.Sections.Add(s));
            return entity;
        }

        public Section InstantiateSection(string type) {
            string t = ((EntityManagementSection)WebConfigurationManager.GetSection("entityManagement")).Sections.Single(s => s.Name == type).Type;
            return (Section) Activator.CreateInstance(Type.GetType(t));
        }

        private class SectionDefinition
        {
            public string Name { get; set; }
            public string View { get; set; }
            public Type Type { get; set; }

            public bool IsInitial { get; set; }

            public int Occurs { get; set; }

            public SectionDefinition(SectionDefinitionElement def, SectionElement sec) {
                Name = def.Name;
                View = def.View;
                Type = Type.GetType(def.Type);
                IsInitial = sec.IsInitial;
                Occurs = sec.Occurs == "unlimited" ? int.MaxValue : int.Parse(sec.Occurs);
            }

            public Section Instantiate() {
                return (Section)Activator.CreateInstance(Type);
            }
        }
    }
}