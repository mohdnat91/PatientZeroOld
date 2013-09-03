using System;
using PatientZero.Core.Configuration;
using PatientZero.Core.Configuration.EntityManagement;
using System.Collections.Generic;
using System.Linq;

namespace PatientZero.Core
{
    public class EntityRepository
    {
        public Entity CreateEntity(string type) {
            EntityDefinition def = Conf.EntityManagement.GetEntityDefinition(type);
            List<Section> sections = def.GetInitalSections().Select(d => (Section) Activator.CreateInstance(Type.GetType(Conf.EntityManagement.GetSectionDefinition(d).Type))).ToList();
            return new Entity { Type = type, Sections = sections };
        }
    }
}

