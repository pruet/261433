using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

namespace DNWS
{
    class Class1 : IPlugin
    {
        public Class1()
        {

        }

        public void PreProcessing(HTTPRequest request)
        {
            throw new NotImplementedException();
        }
        public HTTPResponse GetResponse(HTTPRequest request)
        {
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            String[] ip = request.getPropertyByKey("RemoteEndPoint").Split(':');//teach from 600611030
            sb.Append("<html><body>");
            sb.Append("<br>" + "Port:" + ip[1]);
            sb.Append("<br>" + "IP:" + ip[0]);
            sb.Append("<br>" + "Browser:" + request.getPropertyByKey("User-Agent"));
            sb.Append("<br>" + "Language:" + request.getPropertyByKey("Accept-Language"));
            sb.Append("<br>" + "Ecoding:" + request.getPropertyByKey("Accept-Encoding"));
            sb.Append("<br>" + "Thread ID: " + Thread.CurrentThread.ManagedThreadId);
            //from 600611030 give advice me.
            sb.Append("<br>" + "Amount of thread: " + Process.GetCurrentProcess().Threads.Count); 
            ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);
            ThreadPool.GetMaxThreads(out int max_workerThreads, out int max_completionPortThreads);
            sb.Append("<br>" + "Size of thread: " + max_workerThreads);
            sb.Append("<br>" + "Available thread " + workerThreads);
            sb.Append("<br>" + "Active thread: " + (max_workerThreads-workerThreads));
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
