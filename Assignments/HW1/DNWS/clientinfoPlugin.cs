using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
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
            sb.Append("<html><body><h2>Client IP address:"+ip_port[0] + "</h2>");
            sb.Append("<h2>Client Port:"+ip_port[1]+"</h2>");
            sb.Append("<h2>Browser Information:"+ i[1]+"</h2>");
            sb.Append( "<h2>"+ lines[8] + "</h2>");
            sb.Append( "<h2>"+ lines[7]+"</h2>");
            foreach (KeyValuePair<String, int> entry in statDictionary)
            {
                sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
            }
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
