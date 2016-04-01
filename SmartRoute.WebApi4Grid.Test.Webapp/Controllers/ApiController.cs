using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartRoute.WebApi4Grid.Test.Webapp.Controllers
{
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
}
