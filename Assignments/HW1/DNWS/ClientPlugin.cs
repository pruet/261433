using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; //to use thread
using System.Diagnostics; //guided by 600611010


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

            string[] cli_info = request.getPropertyByKey("RemoteEndPoint").Split(":");
            string data = request.Inf;
            string[] detail  = data.Split("User-Agent:");
            string[] Browser = detail[1].Split("Accept-Language:");
            string[] LangAcc = Browser[1].Split("Accept-Encoding:");
            string[] EncodeAcc = LangAcc[1].Split("Connection:");

            sb.Append("<html><body><h4>Client IP: " + cli_info[0] + "</h4>");
            sb.Append("<html><body><h4>Client Port: " + cli_info[1] + "</h4>");
            sb.Append("<html><body><h4>Browser Information: " + Browser[0] + "</h4>");
            sb.Append("<html><body><h4>Accept Language: " + LangAcc[0] + "</h4>");
            sb.Append("<html><body><h4>Accept Encoding: " + EncodeAcc[0] + "</h4>");

            //HW2 guided by 600611001//
            sb.Append("<div>threadID : " + Thread.CurrentThread.ManagedThreadId + "</div><br>"); //tell that there is only one thread for one connection
            sb.Append("<div>threadState : " + Thread.CurrentThread.ThreadState + "</div><br>"); //print status of the trade
            sb.Append("<div>Thread_IsAlive : " + Thread.CurrentThread.IsAlive + "</div><br>"); //tell that if thread is working
            sb.Append("<div>Number_of_thread : " + Process.GetCurrentProcess().Threads.Count + "</div><br>"); //tell that how many thread there are

            foreach (KeyValuePair<String, int> entry in statDictionary)
            {
                sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
            }
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