using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Microsoft.Extensions.Configuration;
namespace DNWS
{
    class ClientInfoPlugin : IPlugin 
    {

        protected static Dictionary<String, int> statDictionary = null;
        public ClientInfoPlugin()
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
            string info = request.ClientInfo;
            string[] lines = Regex.Split(info, "\\n");

            string ClientIPport = request.GetIP();
            string[] IPport = Regex.Split(ClientIPport, ":");
            sb.Append("<html><body><h1>Client IP:" + IPport[0]+"</h1>");
            sb.Append("<h1>Client Port:"+IPport[1] +"</h1>");
            sb.Append("<h1>" + lines[5] + "</h1>");
            sb.Append("<h1>" + lines[8] + "</h1>");
            sb.Append("<h1>"+ lines[7] + "</h1>");
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
