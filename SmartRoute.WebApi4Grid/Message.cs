using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    public class Message
    {
        public Message()
        {
            Headers = new Dictionary<string, string>();
        }

        public string Api
        {
            get;
            set;
        }
        public Dictionary<string, string> Headers
        {
            get;
            set;
        }
        public object Body
        {
            get;
            set;
        }
    }
}
