using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    public class ControllerContext
    {

        public ControllerContext()
        {

        }

        private Dictionary<string, string> mResultHeader = new Dictionary<string, string>();

        private Dictionary<string, Header> mHeaders = new Dictionary<string, Header>();

        public Dictionary<string, Header> Headers
        {
            get
            {
                return mHeaders;
            }
        }

        public ControllerContext SetHeader(string name, string value)
        {
            mResultHeader[name] = value;
            return this;
        }

        public object Result
        {
            get;
            set;
        }

        public Dictionary<string, string> ResultHeaders
        {
            get
            {
                return mResultHeader;
            }
        }
    }
}
