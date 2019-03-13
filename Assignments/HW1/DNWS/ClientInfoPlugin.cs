using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace DNWS
{
    class ClientInfoPlugin : IPlugin 
    {
        protected static Dictionary<String, int> statDictionary = null;
        public ClientInfoPlugin() //constructor
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

        public HTTPResponse GetResponse(HTTPRequest request) //response to client web browser
        {
            HTTPResponse response = null; //Declare response with HTTPResponse type
            StringBuilder sb = new StringBuilder(); //Build web page

            string info = request.ClientInfo; //Get infomation of Client such as Browser information etc.
            string[] lines = Regex.Split(info, "\\n"); //Split line with \\n

            string ClientIPport = request.GetIP(); //Get Client Ip and port number
            string[] IPport = Regex.Split(ClientIPport, ":"); //Split line ip and port with :
            sb.Append("<html style=\"font-family: Consolas, monaco, monospace; font-size:20px;\"><body><div>Client IP: " + IPport[0]+"</div><br>"); //IP
            sb.Append("<div>Client Port: "+ IPport[1] + "</div><br>"); //Port
            sb.Append("<div>Browser Information" + lines[5].Substring(10) + "</div><br>"); //Browser Information
            sb.Append("<div>" + lines[8] + "</div><br>"); //Accept Language
            sb.Append("<div>" + lines[7] + "</div><br>"); //Accpet Encoding
            //Show thread of client (Multithread)
            //sb.Append("<div>CurrentThread ID : " + Thread.CurrentThread.ManagedThreadId + "</div><br>"); //Thread ID
            //sb.Append("<div>Thread Status : " + Thread.CurrentThread.ThreadState + "</div><br>"); //Thread Status
            //sb.Append("<div>Thread IsAlive : "  + Thread.CurrentThread.IsAlive + "</div><br>"); //Thread is alive or not
            //sb.Append("<div>Number of threads : " + Process.GetCurrentProcess().Threads.Count + "</div><br>"); //Amount of threads
            sb.Append("</body></html>"); //End tag html
           
            response = new HTTPResponse(200); //200 ok
            response.body = Encoding.UTF8.GetBytes(sb.ToString()); //Encode UTF8
            return response; //Return response
        }

        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }
    }
}
