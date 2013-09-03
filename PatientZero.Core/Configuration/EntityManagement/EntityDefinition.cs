using System;
using System.Collections.Generic;

namespace PatientZero.Core.Configuration.EntityManagement
{
	public class EntityDefinition
	{
        public string Type
        {
            get;
            set;
        }

        public IList<SectionReference> Sections
        {
            get;
            set;
        }

        public IList<SectionReference> GetInitalSections() {
            return null;
        }
	}
}

