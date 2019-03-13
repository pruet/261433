using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace DNWS
{
    class Clientinfo : IPlugin
    {
        public void PreProcessing(HTTPRequest request)
        {
            throw new NotImplementedException();
        }

        public HTTPResponse GetResponse(HTTPRequest request)    //600611030 teach me and give me advices
        {
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            string[] ipport = request.getPropertyByKey("RemoteEndPoint").Split(':');
            sb.Append("<html><body>Client IP: " + ipport[0] + "</br></br>");
            sb.Append("Client Port: " + ipport[1] + "</br></br>");
            sb.Append("Browser Information: " + request.getPropertyByKey("User-Agent") + "</br></br>");
            sb.Append("Accept-Language: " + request.getPropertyByKey("Accept-Language") + "</br></br>");
            sb.Append("Accept-Encoding: " + request.getPropertyByKey("Accept-Encoding") + "</br></br>");
            sb.Append("Thread ID: " + Thread.CurrentThread.ManagedThreadId + "</br></br>");
            sb.Append("Number of Threads: " + Process.GetCurrentProcess().Threads.Count + "</br></br>");//ref https://stackoverflow.com/questions/15381174/how-to-count-the-amount-of-concurrent-threads-in-net-application
            sb.Append("Amount of thread: " + Process.GetCurrentProcess().Threads.Count + "</br></br>");
            ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);
            ThreadPool.GetMaxThreads(out int max_workerThreads, out int max_completionPortThreads);
            sb.Append("Maximum number of worker threads: " + max_workerThreads + "</br/></br>");
            sb.Append("Available threads in thread pool: " + workerThreads + "</br></br>");    
            sb.Append("Active threads in thread pool: " + (max_workerThreads - workerThreads) +"</br></br>");
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
