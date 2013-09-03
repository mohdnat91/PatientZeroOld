using System;
using System.Collections.Generic;

namespace PatientZero.Core.Configuration.EntityManagement
{
	public class SectionReference
	{
        public string DefinitionName
        {
            get;
            set;
        }

        public bool IsInitial
        {
            get;
            set;
        }

        public string Occurences
        {
            get;
            set;
        }
	}
}

