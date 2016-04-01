using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    public class Header
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public T ValueTo<T>()
        {
            return (T)Convert.ChangeType(Value, typeof(T));
        }
    }
}
