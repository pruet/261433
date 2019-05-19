﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace DNWS
{
    class ClientInfo : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public ClientInfo()
        {
            if (statDictionary == null)
            {
                statDictionary = new Dictionary<String, int>();

            }
        }

        public void PreProcessing(HTTPRequest request)
        {
            if (statDictionary.ContainsKey(request.Url))
            {
                statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
            }
            else
            {
                statDictionary[request.Url] = 1;
            }
        }

        public HTTPResponse GetResponse(HTTPRequest request)
        {
            int workerThr, completionPort;
            ThreadPool.GetAvailableThreads(out workerThr, out completionPort);
            int maxWorker, maxComPort;
            ThreadPool.GetMaxThreads(out maxWorker, out maxComPort);

            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            string[] IPinfo = request.getPropertyByKey("RemoteEndPoint").Split(":"); //600611030 told me the idea of this solution.
            sb.Append("<html><body><h1>Client Info</h1>");                           //These keys are also in the textbook as I looking for more info of them.
            sb.Append("<div>IP Address " + IPinfo[0] + "</div>");
            sb.Append("<div>Port : " + IPinfo[1] + "</div>");
            sb.Append("<div>Browser Information : " + request.getPropertyByKey("User-Agent") + "</div>");
            sb.Append("<div>Accept Language : " + request.getPropertyByKey("Accept-Language") + "</div>");
            sb.Append("<div>Acccept Encoding : " + request.getPropertyByKey("Accept-Encoding") + "</div>");
            sb.Append("<div>Thread ID : " + Thread.CurrentThread.ManagedThreadId.ToString() + "</div>"); // show thread id
            sb.Append("<div>Thread Alive Status : " + Thread.CurrentThread.IsAlive.ToString() + "</div>"); //show thread alive status
            sb.Append("<div>Thread Status : " + Thread.CurrentThread.ThreadState.ToString() + "</div>"); // show thread processing status
            sb.Append("<div>Total Threads Running : " + Process.GetCurrentProcess().Threads.Count + "</div>"); //show total number of running threads
            sb.Append("<div>Available in pool - Thread : " + workerThr + " asyncIO : " + completionPort + "</div>"); //show available thread
            sb.Append("<div>Maximum in pool - Thread : " + maxWorker + " asyncIO : " + maxComPort + "</div>"); //show maximum thread
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