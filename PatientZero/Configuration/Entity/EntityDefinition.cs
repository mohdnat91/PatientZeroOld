using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientZero.Configuration.Entity
{
    public class EntityDefinition
    {
        public string Type { get; set; }

        public IEnumerable<SectionReference> Sections { get; set; }
    }
}