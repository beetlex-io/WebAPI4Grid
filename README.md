# WebAPI4Grid
  使用WebAPI4Grid可以轻易把webapi的操作负载到相关应用中,并通过WebAPI4Grid实现热节点加载和故障处理.
##Agent使用
```
 public class TestApiController : Controller
    {
        //
        // GET: /Api/

        public ActionResult Login()
        {
            return InvokeAgent();
        }

        public ActionResult Register()
        {
            return InvokeAgent();
        }

        private ActionResult InvokeAgent()
        {
            return Agent.Invoke<ActionResult>(Context);
        }

        public ApiAgent Agent
        {
            get
            {
                return MvcApplication.ApiAgent;
            }
        }

        public Code.APIContext Context
        {
            get
            {
                return MvcApplication.ApiContext;
            }
        }
    }
```
##Server定义
```
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
```
##应用效果
![image](https://github.com/IKende/WebAPI4Grid/blob/master/TESTPIC.png)
