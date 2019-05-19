using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;


namespace DNWS
{
    class ClientPlugin : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public ClientPlugin()
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
            
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
    
            string[] ip = request.getPropertyByKey("RemoteEndPoint").Split(":");
                //array ip type string collect the data in type string so it make  ip=ip[0]  port=ip[1] 
                            //split ip and port by using : to seperate them  --->   ip    :   port 

            sb.Append("<html><body><p>Client IP :   "+ip[0]+ "</p>"); //ip
            sb.Append("<p>Client Port :   "+ip[1] + "</p>"); //port
            sb.Append("<p>Browser Information :  " +request.getPropertyByKey("User-Agent") + "</p>");
            sb.Append("<p>Accept Language :   "+request.getPropertyByKey("Accept-Language") + "</p>");
            sb.Append("<p>Accept Encoding :   " +request.getPropertyByKey("Accept-Encoding") + "</p>");

            sb.Append("<p>Thread ID : " +Thread.CurrentThread.ManagedThreadId +"</p>");
            Process thispro = Process.GetCurrentProcess();  //collect process 
            int threadnum = thispro.Threads.Count;  //count thread number
            int thisproID = thispro.Id;             //collect process ID
            sb.Append("<p>Process ID : " + thisproID + "</p>");     //display process ID
            sb.Append("<p>Number of Thread : " + threadnum + "</p>");   //display thread number

            
            //threadpool 
            ThreadPool.GetAvailableThreads(out int thread_available, out int io);//get available number of worker and io
            ThreadPool.GetMaxThreads(out int max_thread,out int max_io); //get max number of worker and io

            sb.Append("<p>Available Thread : " + thread_available + "</p>"); //show available thread
            sb.Append("<p>Maximum Thread : " + max_thread + "</p>"); //show maximum thread
            sb.Append("<p>Active Thread : " + (max_thread - thread_available) + "</p>"); //display active thread

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
