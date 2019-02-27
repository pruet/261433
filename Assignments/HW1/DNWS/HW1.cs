using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics; //get Process command
using System.Threading; //Include Thread


namespace DNWS
{
    class HW1 : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public HW1()
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
            //teach by purit 600611039 and REF:https://en.wikipedia.org/wiki/List_of_HTTP_header_fields
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            string port_ip = request.getPropertyByKey("RemoteEndPoint"); //GET ip and port of client.
            string []ans = port_ip.Split(':'); //Split ip and port by :.
            sb.Append("<br>Clinet IP : " + ans[0]); //show ip 
            sb.Append("<br>Client Port : " + ans[1]); //show port
            sb.Append("<br>Browser Information :" + request.getPropertyByKey("User-Agent"));  //show Browser Information.
			sb.Append("<br>Accept-Charset : " + request.getPropertyByKey("Accept-Language"));  //show support Language.
            sb.Append("<br>Accept-Encoding : " + request.getPropertyByKey("Accept-Encoding")); //show Encoding.
            //ref : https://stackoverflow.com/questions/15381174/how-to-count-the-amount-of-concurrent-threads-in-net-application.
            sb.Append("<br>Amount of concurrent threads : " + Process.GetCurrentProcess().Threads.Count); //show amount of concurrent threads.
            //ref : https://stackoverflow.com/questions/1679243/getting-the-thread-id-from-a-thread.
            sb.Append("<br>Threads name : " + Thread.CurrentThread.Name); //show Threads name.
            sb.Append("<br>Threads ID : " + Thread.CurrentThread.ManagedThreadId); //show Threads ID.
            sb.Append("<br>Threads State : " + Thread.CurrentThread.ThreadState); //show Threads State.

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