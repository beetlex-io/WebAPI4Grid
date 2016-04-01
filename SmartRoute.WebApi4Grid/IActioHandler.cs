using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    class ActioHandler
    {

        public ActioHandler()
        {
            Parameters = new List<System.Reflection.ParameterInfo>();
            UseThreads = false;
        }

        const string ToObjectMethodName = "ToObject";//type

        const string SelectTokenMethodName = "SelectToken";//string

        private static System.Reflection.MethodInfo mToObjectMethod;

        private static System.Reflection.MethodInfo mSelectTokenMethod;

        public string Name
        {
            get;
            set;
        }

        public bool UseThreads
        {
            get;
            set;
        }

        public List<System.Reflection.ParameterInfo> Parameters
        {
            get;
            private set;
        }

        public ApiController Controller
        {
            get;
            set;
        }

        public System.Reflection.MethodInfo Method
        {
            get;
            set;
        }

        public void Invoke(SubscribeEventArgs<Message> routemsg, Message message, Result result)
        {
            object[] data = new object[] { routemsg, message, result };
            if (UseThreads)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(OnInvoke, data);

            }
            else
            {
                OnInvoke(data);
            }


        }

        private object GetTypeDefault(Type p)
        {
            if (p.IsValueType)
                return Activator.CreateInstance(p);
            else
                return null;
        }
        protected void OnInvoke(object state)
        {
            object[] data = (object[])state;
            SubscribeEventArgs<Message> routemsg = (SubscribeEventArgs<Message>)data[0];
            Message message = (Message)data[1]; 
            Result result = (Result)data[2];
            try
            {

                ControllerContext context = new ControllerContext();
                Controller.Context = context;
                if (message.Body != null && (mToObjectMethod == null || mSelectTokenMethod == null))
                {
                    mToObjectMethod = message.Body.GetType().GetMethod(ToObjectMethodName, new Type[] { typeof(Type) });
                    mSelectTokenMethod = message.Body.GetType().GetMethod(SelectTokenMethodName, new Type[] { typeof(String) });
                }
                object[] objs = new Object[Parameters.Count];
                if (Parameters.Count == 1 && Parameters[0].ParameterType.IsClass)
                {
                    objs[0] = mToObjectMethod.Invoke(message.Body, new object[] { Parameters[0].ParameterType });
                }
                else
                {
                    for (int i = 0; i < Parameters.Count; i++)
                    {
                        System.Reflection.ParameterInfo p = Parameters[i];
                        object pobject = mSelectTokenMethod.Invoke(message.Body, new object[] { p.Name });
                        if (pobject != null)
                        {
                            objs[i] = mToObjectMethod.Invoke(pobject, new object[] { p.ParameterType });
                        }
                        else
                        {
                            objs[i] =GetTypeDefault(p.ParameterType);
                        }
                    }

                }
                result.Data = Method.Invoke(Controller, objs);
                result.Headers = context.ResultHeaders;
            }
            catch (Exception e_)
            {
                result.ErrorCode = 500;
                result.Error = e_.Message;
                result.ErrorStackTrace = e_.StackTrace;
            }
            finally
            {
                routemsg.Reply(result);
            }

        }
    }
}
