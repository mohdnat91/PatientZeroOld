using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace PatientZero.Models.Sections
{
    public class PatientInfoSection : Section
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string PhoneNum { get; set; }
    }
}