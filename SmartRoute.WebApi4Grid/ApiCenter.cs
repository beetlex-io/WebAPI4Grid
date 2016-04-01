using SmartRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    public class ApiCenter
    {
   

        private SmartRoute.INode mNode;

        private Dictionary<string, ActioHandler> mHandlers = new Dictionary<string, ActioHandler>();

        private SmartRoute.SubscribeSwitch mSwitch;

        public ApiCenter()
        {


            mNode = SmartRoute.Route.CreateNode(CONST_VALUE.NODE_NAME);
            mNode.Open();
            mSwitch = new SmartRoute.SubscribeSwitch(mNode, CONST_VALUE.SWITCH_NAME);
        }

        public SmartRoute.INode Node
        {
            get
            {
                return mNode;
            }
        }

        public void Register(ApiController controller)
        {
            Type controllerType = controller.GetType();
            foreach (System.Reflection.MethodInfo method in controllerType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                WebApiAttribute[] waa = (WebApiAttribute[])method.GetCustomAttributes(typeof(WebApiAttribute), false);
                if (waa != null && waa.Length > 0)
                {
                    ActioHandler handler = new ActioHandler();
                    handler.Controller = controller;
                    handler.Method = method;
                    handler.Name = waa[0].Name;
                    mHandlers[handler.Name] = handler;
                    foreach (System.Reflection.ParameterInfo p in method.GetParameters())
                    {
                        handler.Parameters.Add(p);
                    }
                    mSwitch.GetServiceSubscribe(handler.Name).RegisterProcess<Message>(OnReceive);
                }
                    
            }
        }

        private void OnReceive(object sender, SubscribeEventArgs<Message> e)
        {
            Result result = new Result();
            ActioHandler handler;
            if (mHandlers.TryGetValue(e.Data.Api,out handler))
            {
                handler.Invoke(e, e.Data, result);
            }
            else
            {
                result.ErrorCode = 404;
                result.Error = string.Format("{0} api action not found!", e.Data.Api);
                e.Reply(result);
            }
            
        }
        public void Register<T>() where T : ApiController, new()
        {
            Register(new T());
        }
    }
}
