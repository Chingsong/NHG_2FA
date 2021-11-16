using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace SP_2FAModule
{
    // a temp handler used to force the SessionStateModule to load session state
    public class MyHttpHandler : IHttpHandler, IRequiresSessionState
    {
        internal readonly IHttpHandler OriginalHandler;

        public MyHttpHandler(IHttpHandler originalHandler)
        {
            OriginalHandler = originalHandler;
        }
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //ProcessRequest() will not be called, but let's be safe
            throw new NotImplementedException("MyHttpHandler cannot process requests.");
        }
    }
}
