using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class Class1 : IPlugin
    {
        public Class1()
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
            String[] ip = request.getPropertyByKey("RemoteEndPoint").Split(':');//teach from 600611030
            sb.Append("<html><body>");
            sb.Append("<br>" + "Port:" + ip[1]);
            sb.Append("<br>" + "IP:" + ip[0]);
            sb.Append("<br>" + "Browser:" + request.getPropertyByKey("User-Agent"));
            sb.Append("<br>" + "Language:" + request.getPropertyByKey("Accept-Language"));
            sb.Append("<br>" + "Ecoding:" + request.getPropertyByKey("Accept-Encoding"));


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
