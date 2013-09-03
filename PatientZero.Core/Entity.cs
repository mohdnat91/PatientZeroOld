using System;
using System.Collections.Generic;

namespace PatientZero.Core
{
    public class Entity
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public List<Section> Sections { get; set; }
    }
}

