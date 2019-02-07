using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class ClientInfo : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public ClientInfo()
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
            string[] IPinfo = request.getPropertyByKey("RemoteEndPoint").Split(":");
            sb.Append("<html><body><h1>Client Info</h1>");
            sb.Append("<div>IP Address " + IPinfo[0] + "</div>");
            sb.Append("<div>Port : " + IPinfo[1] + "</div>");
            sb.Append("<div>Browser Information : " + request.getPropertyByKey("User-Agent") + "</div>");
            sb.Append("<div>Accept Language : " + request.getPropertyByKey("Accept-Language") + "</div>");
            sb.Append("<div>Acccept Encoding : " + request.getPropertyByKey("Accept-Encoding") + "</div>");
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