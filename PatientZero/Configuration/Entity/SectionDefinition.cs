using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientZero.Configuration.Entity
{
    public class SectionDefinition
    {
        public string Name { get; set; }

        public Type Implementation { get; set; }
    }
}