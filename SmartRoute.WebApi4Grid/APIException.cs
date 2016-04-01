using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    public class APIException:Exception
    {
        public APIException()
        {
        }
        public APIException(string msg)
            : base(msg)
        {
        }
        public APIException(string msg, Exception innerError) : base(msg, innerError) { }

        public string ErrorStackTrace
        {
            get;
            set;
        }
    }
}
