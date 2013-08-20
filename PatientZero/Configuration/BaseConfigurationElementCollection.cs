using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PatientZero.Configuration
{
    public abstract class BaseConfigurationElementCollection<T> : ConfigurationElementCollection, IEnumerable<T> where T : ConfigurationElement
    {
        public new IEnumerator<T> GetEnumerator() {
            IEnumerator old = base.GetEnumerator();
            while (old.MoveNext()) {
                yield return (T)old.Current;
            }
        }
    }
}