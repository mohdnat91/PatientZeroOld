using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace PatientZero.Configuration.Navigation
{
    public class NavigationLink
    {
        public string DisplayName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public RouteValueDictionary Params { get; set; }
    }
}