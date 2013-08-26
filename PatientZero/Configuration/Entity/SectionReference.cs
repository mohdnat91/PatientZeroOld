using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientZero.Configuration.Entity
{
    public class SectionReference
    {
        public string DefinitionName { get; set; }

        public bool IsInitial { get; set; }

        public Range Occurences { get; set; }
    }
}