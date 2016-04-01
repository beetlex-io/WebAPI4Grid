using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    [AttributeUsage(AttributeTargets.Method)]
    public class WebApiAttribute : Attribute
    {
        public WebApiAttribute(string name)
        {
            Name = name.ToLower();
        }
        public string Name
        {
            get;
            private set;
        }
    }
}
