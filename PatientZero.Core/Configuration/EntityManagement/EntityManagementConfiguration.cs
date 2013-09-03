using System;

namespace PatientZero.Core.Configuration.EntityManagement
{
	public class EntityManagementConfiguration
	{
        public EntityDefinition GetEntityDefinition(string type) {
            return null;
        }

        public SectionDefinition GetSectionDefinition(string type) {
            return null;
        }

        public SectionDefinition GetSectionDefinition(SectionReference reference) {
            return null;
        }
	}
}

