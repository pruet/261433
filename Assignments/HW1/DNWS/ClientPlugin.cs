using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DNWS
{
    class ClientPlugin : IPlugin
    {
        public ClientPlugin()
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
            ThreadPool.GetAvailableThreads(out int worker, out int io);
            ThreadPool.GetMaxThreads(out int workerThreads, out int portThreads);
            string[] ip_port = request.getPropertyByKey("RemoteEndPoint").Split(':');//this file 600611030 teach me ;
            sb.Append("<html><body>" + "Client IP: " + ip_port[0]);
            sb.Append("<br>" + "Client Port: " + ip_port[1]);
            sb.Append("<br>"+ "Browser Information: " + request.getPropertyByKey("User-Agent"));
            sb.Append("<br>" + "Accept Language: " + request.getPropertyByKey("Accept-Language"));
            sb.Append("<br>" + "Accept Encoding: " + request.getPropertyByKey("Accept-Encoding"));
            sb.Append("<br>" + "Thread ID: " + Thread.CurrentThread.ManagedThreadId);//I find in this web https://stackoverflow.com/questions/1679243/getting-the-thread-id-from-a-thread
            sb.Append("<br>" + "Thread count: " + Process.GetCurrentProcess().Threads.Count);//I get code from this web https://stackoverflow.com/questions/15381174/how-to-count-the-amount-of-concurrent-threads-in-net-application
            sb.Append("<br>" + "Size of thread: " + workerThreads);
            sb.Append("<br>" + "Available thread : " + worker);
            sb.Append("<br>" + "Active thread: " + (workerThreads - worker));
            //https://docs.microsoft.com/en-us/dotnet/api/system.threading.threadpool?view=netframework-4.7.2



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