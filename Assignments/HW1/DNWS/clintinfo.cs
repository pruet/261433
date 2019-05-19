using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
namespace DNWS
{
    class clintinfo : IPlugin
    {
        public void PreProcessing(HTTPRequest request)
        {
            throw new NotImplementedException();
        }
        public HTTPResponse GetResponse(HTTPRequest request)
        {
            HTTPResponse response = null;                                                                   //600611024 and 600611030 give me an idea and teach me
            StringBuilder sb = new StringBuilder();
            string[] getter = request.getPropertyByKey("RemoteEndPoint").Split(':');
            sb.Append("<html><body>Client IP: " + getter[0] + "</br></br>");
            sb.Append("Client Port: " + getter[1] + "</br></br>");
            sb.Append("Browser Information: " + request.getPropertyByKey("User-Agent") + "</br></br>");
            sb.Append("Accept-Language: " + request.getPropertyByKey("Accept-Language") + "</br></br>");
            sb.Append("Accept-Encoding: " + request.getPropertyByKey("Accept-Encoding"));  //600611039 help me for this
            sb.Append("</br>Threads-count: " + Process.GetCurrentProcess().Threads.Count);              
            sb.Append("<br>Threads State : " + Thread.CurrentThread.ThreadState);
            sb.Append("<br>Threads ID : " + Thread.CurrentThread.ManagedThreadId);
            sb.Append("</body></html>");
            response = new HTTPResponse(200);
            response.body = Encoding.UTF8.GetBytes(sb.ToString());
            return response;
        }
        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }
    }
}