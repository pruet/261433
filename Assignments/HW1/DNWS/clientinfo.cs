using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace DNWS
{
    class ClientInfo : IPlugin //reference from 600611030
    {
        public void PreProcessing(HTTPRequest request) 
        {
            throw new NotImplementedException();
        }

        public HTTPResponse GetResponse(HTTPRequest request)
        {
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            string[] port = request.getPropertyByKey("RemoteEndPoint").Split(':');
            sb.Append("<html><body>Client IP: " + port[0] + "</br></br>");
            sb.Append("Client Port: " + port[1] + "</br></br>");
            sb.Append("Browser Information: " + request.getPropertyByKey("UserAgent") + "</br></br>");
            sb.Append("AcceptLanguage: " + request.getPropertyByKey("AcceptLanguage") + "</br></br>");
            sb.Append("AcceptEncoding: " + request.getPropertyByKey("AcceptEncoding") + "</br></br>");
            sb.Append("Number of Thread: " + Process.GetCurrentProcess().Threads.Count +"</br></br>");
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