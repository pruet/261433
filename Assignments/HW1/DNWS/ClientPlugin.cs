using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class ClientPlugin : IPlugin
    {
        public ClientPlugin()
        {
            
        }

        public void PreProcessing(HTTPRequest request)
        {
            throw new NotImplementedException();
        }
        public HTTPResponse GetResponse(HTTPRequest request)
        {
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            string[] ip_port = request.getPropertyByKey("RemoteEndPoint").Split(':');//this file 600611030 teach me ;
            sb.Append("<html><body>" + "Client IP: " + ip_port[0]);
            sb.Append("<br>" + "Client Port: " + ip_port[1]);
            sb.Append("<br>"+ "Browser Information: " + request.getPropertyByKey("User-Agent"));
            sb.Append("<br>" + "Accept Language: " + request.getPropertyByKey("Accept-Language"));
            sb.Append("<br>" + "Accept Encoding: " + request.getPropertyByKey("Accept-Encoding"));

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