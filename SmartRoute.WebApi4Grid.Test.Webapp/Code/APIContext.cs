using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartRoute.WebApi4Grid.Test.Webapp.Code
{
    public class APIContext : SmartRoute.WebApi4Grid.IAPIContext
    {
        public string Path()
        {
            return HttpContext.Current.Request.CurrentExecutionFilePath;
        }

        private bool mOutPutErrorStackTrace = false;

        public SmartRoute.WebApi4Grid.Header GetHeader(string name)
        {
            return new SmartRoute.WebApi4Grid.Header { Name = name, Value = HttpContext.Current.Request[name] };
        }

        public object Request()
        {
            HttpContext contex = HttpContext.Current;
           
                Dictionary<string, string> result = new Dictionary<string, string>();
                foreach (string item in contex.Request.QueryString.Keys)
                {
                    result[item] = contex.Request.QueryString[item];
                }
                foreach (string item in contex.Request.Form.Keys)
                {
                    result[item] = contex.Request.Form[item];
                }
                return result;
           
        }

        public object Response(SmartRoute.WebApi4Grid.Result result)
        {
            return result;
        }

        public T Response<T>(SmartRoute.WebApi4Grid.Result result)
        {
            if (!mOutPutErrorStackTrace)
                result.ErrorStackTrace = null;
            object apiresult = new StringResult(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            return (T)apiresult;
        }
        public class StringResult : ActionResult
        {
            public StringResult(string value)
            {
                Value = value;
            }
            public string Value
            {
                get;
                set;
            }
            public override void ExecuteResult(System.Web.Mvc.ControllerContext context)
            {
                var response = context.RequestContext.HttpContext.Response;

                response.Clear();
                response.ContentType = "text/html; charset=UTF-8";
                if (Value != null)
                {

                    response.Output.Write(Value);
                }
                response.End();
            }
        }
    }
}