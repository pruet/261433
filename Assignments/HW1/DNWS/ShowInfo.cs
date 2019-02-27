using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace DNWS
{
    class ShowPlugin : IPlugin
    {
        public void PreProcessing(HTTPRequest request)
        {
            throw new NotImplementedException(); //Request HTTP from server function
        }

        public HTTPResponse GetResponse(HTTPRequest request) //Server response with HTTP
        {
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            string[] ipport = request.getPropertyByKey("RemoteEndPoint").Split(':');
            sb.Append("<html><font face=\"verdana\"><style>body {background-color: lightblue;}</style><body>Client IP: " + ipport[0] + "</br></br>"); //open Tag html in application layer
            sb.Append("Client Port: " + ipport[1] + "</br></br>");
            sb.Append("Browser Information: " + request.getPropertyByKey("User-Agent") + "</br></br>");
            sb.Append("Accept Language: " + request.getPropertyByKey("Accept-Language") + "</br></br>");
            sb.Append("Accept Encoding: " + request.getPropertyByKey("Accept-Encoding"));
            sb.Append("Thread ID: " + Thread.CurrentThread.ManagedThreadId + "</br></br>"); //From https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.getcurrentprocess?view=netframework-4.7.2
            sb.Append("Thead: " + Process.GetCurrentProcess().Threads.Count); 
            sb.Append("</body></font></html>"); //Close tag
            response = new HTTPResponse(200); //ACK 
            response.body = Encoding.UTF8.GetBytes(sb.ToString()); //Encode (translate to string)
            return response;
        }
        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }
    }
}