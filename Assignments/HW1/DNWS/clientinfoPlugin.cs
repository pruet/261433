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
            string ip = request.IPclient;
      
            string x = request.line;
            //split each lines
            string[] lines = Regex.Split(x, "\\n");
        
            //add some informations
            sb.Append("<html><body><h1>Client IP address:</h1>");
            sb.Append("<h1>Client Port:</h1>");
            sb.Append("<h1>Browser Information:" + lines[5]+"</h1>");
            sb.Append( "<h1>"+ lines[8] + "</h1>");
            sb.Append( "<h1>"+ lines[7]+"</h1>");
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
