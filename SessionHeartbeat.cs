using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace PipelineFeatureList
{
    public class SessionHeartbeatHttpHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            context.Session["Heartbeat"] = DateTime.Now;
        }
    }
}
