using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    public interface IAPIContext
    {
        string  Path();

        Header GetHeader(string name);

        object Request();

        object Response(SmartRoute.WebApi4Grid.Result result);

        T Response<T>(SmartRoute.WebApi4Grid.Result result);
    }
}
