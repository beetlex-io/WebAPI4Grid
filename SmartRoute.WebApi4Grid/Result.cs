using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    public class Result
    {
        public Dictionary<string, string> Headers
        {
            get;
            set;
        }
        public int ErrorCode
        {
            get;
            set;
        }
        public string ErrorStackTrace
        {
            get;
            set;
        }
        public string Error
        {
            get;
            set;
        }
        public object Data
        {
            get;
            set;
        }
    }

}
