using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartRoute.WebApi4Grid
{
    public class ApiAgent
    {



        private SmartRoute.INode mNode;


        private SmartRoute.SubscribeSwitch mSwitch;


        public ApiAgent()
        {
            mNode = SmartRoute.Route.CreateNode(CONST_VALUE.NODE_NAME);
            mNode.Open();
            mSwitch = new SmartRoute.SubscribeSwitch(mNode, CONST_VALUE.SWITCH_NAME);
        }

        public RESULT Invoke<RESULT>(IAPIContext context)
        {
            return Invoke<RESULT>(context, null);
        }

        public RESULT Invoke<RESULT>(IAPIContext context, params Header[] headers)
        {
            SmartRoute.WebApi4Grid.Result result;
            try
            {
                Message message = new Message();
                message.Body = context.Request();
                if (headers != null)
                {
                    foreach (Header item in headers)
                    {
                        message.Headers[item.Name] = item.Value;
                    }
                }
                string service = context.Path();

                message.Api = service;


                SmartRoute.IMessage rmessage = mSwitch.Send(service, message);
                result = rmessage.GetBody<SmartRoute.WebApi4Grid.Result>();
            }
            catch (Exception e_)
            {
                result = new Result();
                result.ErrorCode = 501;
                result.Error = e_.Message;
                result.ErrorStackTrace = e_.StackTrace;
            }

            return context.Response<RESULT>(result);
        }

    }
}
