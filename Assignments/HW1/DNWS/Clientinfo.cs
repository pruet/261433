using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class Clientinfo : IPlugin
    {
        public void PreProcessing(HTTPRequest request)
        {
            throw new NotImplementedException();
        }

        public HTTPResponse GetResponse(HTTPRequest request)
        {
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            string[] ipport = request.getPropertyByKey("RemoteEndPoint").Split(':');
            sb.Append("<html><body>Client IP: " + ipport[0] + "</br></br>");
            sb.Append("Client Port: " + ipport[1] + "</br></br>");
            sb.Append("Browser Information: " + request.getPropertyByKey("User-Agent") + "</br></br>");
            sb.Append("Accept-Language: " + request.getPropertyByKey("Accept-Language") + "</br></br>");
            sb.Append("Accept-Encoding: " + request.getPropertyByKey("Accept-Encoding"));
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
