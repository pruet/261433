using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using System.Diagnostics;
// build clientinfoPlugin class
namespace DNWS 
{
    class clientinfoPlugin : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public clientinfoPlugin()
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
            //int ip = request.IPclient;
            string[] ip_port = request.getPropertyByKey("RemoteEndpoint").Split(':'); // create string array ip_port to split between ip and port
            request.getPropertyByKey("RemoteEndpoint"); // to call ip and port
            string x = request.line;//create string x to split between line
            //split each lines
            string[] lines = Regex.Split(x, "\\n");
            //split :User-Agent
            string[] i = lines[5].Split(':');
            //add some informations
            //create variable name th is a thread
            var th = Thread.CurrentThread;
            int ThreadsCount = 0;
            lock (th)
            ThreadsCount = Process.GetCurrentProcess().Threads.Count;
            sb.Append("<html><body><p>Client IP address : "+ip_port[0] + "</p>");
            sb.Append("<p>Client Port : "+ip_port[1]+"</p>");
            sb.Append("<p>Browser Information : "+ i[1]+"</p>");
            sb.Append( "<p>"+ lines[8] + "</p>");
            sb.Append( "<p>"+ lines[7]+"</p>");
            sb.Append("<p>Thread ID : " + th.ManagedThreadId + "</p>");
            sb.Append("<p>Thread is alive : " + th.IsAlive + "</p>");//This thread is still alive or not 
            sb.Append("<p>Thread run on background : " + th.IsBackground + "</p>");//This thread has run on background or not
            sb.Append("<p>Thread status : " + th.ThreadState + "</p>");
            sb.Append("<p>Threads count: "+ ThreadsCount + "</p>" );

            //th.Start();
            //Console.WriteLine( Thread.ResetAbort());

            /*foreach (KeyValuePair<String, int> entry in statDictionary)
            {
                sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
            }*/
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
