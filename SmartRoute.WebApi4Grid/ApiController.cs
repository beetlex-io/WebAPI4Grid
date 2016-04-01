using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    public abstract class ApiController
    {

        [ThreadStatic]
        private static ControllerContext mContext;

        public  ControllerContext Context
        {
            get
            {
                return mContext;
            }
            internal set
            {
                mContext = value;
            }
        }
    }
}
