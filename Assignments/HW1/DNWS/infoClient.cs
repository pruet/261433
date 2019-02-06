using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace DNWS
{
    class infoclient : IPlugin
    {
        protected static Dictionary<String, int> infoclientDictionary = null;
        public infoclient()
        {
            if(infoclientDictionary == null)
            {
                infoclientDictionary = new Dictionary<string, int>();
            }
        }
        public void PreProcessing(HTTPRequest request)
        {
            if (infoclientDictionary.ContainsKey(request.Url))
            {
                infoclientDictionary[request.Url] = (int)infoclientDictionary[request.Url] + 1;
            }
            else
            {
                infoclientDictionary[request.Url] = 1;
            }
        }
        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }
        public HTTPResponse GetResponse(HTTPRequest request)
        {
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            //sb.Append("Client IP : " + IPAddress.Parse(((IPEndPoint)_client.RemoteEndPoint).Address.ToString ()) + "<br />");
            //sb.Append("Client Port : " + ((IPEndPoint)_client.RemoteEndPoint).Port.ToString() + "<br />");
            sb.Append("Client IP : " + request.getIP() + "<br />");
            sb.Append("Client Port : " + request.getPort() + "<br />");
            sb.Append("Browser Information : " + request.getPropertyByKey("User-Agent") + "<br />");
            sb.Append("Accept Language : " + request.getPropertyByKey("Accept-Language") + "<br />");
            sb.Append("Accept Encoding: " + request.getPropertyByKey("Accept-Encoding") + "<br />");
            sb.Append("</body></html>");
            response = new HTTPResponse(200);
            response.body = Encoding.UTF8.GetBytes(sb.ToString());
            return response;
        }
    }
}