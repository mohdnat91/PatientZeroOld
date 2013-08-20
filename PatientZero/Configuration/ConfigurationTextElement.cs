using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;

namespace PatientZero.Configuration
{
    public class ConfigurationTextElement : ConfigurationElement
    {
        private string _value;

        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey) 
        {
            _value = reader.ReadElementContentAsString();
        }

        public string Value { get { return _value; } }
    }
}