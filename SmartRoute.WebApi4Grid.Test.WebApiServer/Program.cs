using SmartRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid.Test.WebApiServer
{
    class Program : SmartRoute.WebApi4Grid.ApiController
    {

        private static SmartRoute.WebApi4Grid.ApiCenter mCenter;

        static void Main(string[] args)
        {                
            mCenter = new SmartRoute.WebApi4Grid.ApiCenter();
            mCenter.Register(new Program());
            Console.WriteLine("api service start!");
            System.Threading.Thread.Sleep(-1);
        }

        [WebApi("/TestApi/Login")]
        public object Login(string username, string pwd)
        {
            object result= new { UserName = username, PWD = pwd };
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            return result;
        }
        [WebApi("/TestApi/Register")]
        public object Register(string username, string email, string pwd)
        {
            object result= new { UserName = username, EMail = email, PWD = pwd };
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            return result;
        }

    }
}
