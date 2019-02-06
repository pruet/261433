using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class ClientPlugin : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public ClientPlugin()
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
    
            string[] ip = request.getPropertyByKey("RemoteEndPoint").Split(":");
                //array ip type string collect the data in type string so it make  ip=ip[0]  port=ip[1] 
                            //split ip and port by using : to seperate them  --->   ip    :   port 

            sb.Append("<html><body><p>Client IP :   "+ip[0]+ "</p>"); //ip
            sb.Append("<html><body><p>Client Port :   "+ip[1] + "</p>"); //port
            sb.Append("<html><body><p>Browser Information :  " +request.getPropertyByKey("User-Agent") + "</p>");
            sb.Append("<html><body><p>Accept Language :   "+request.getPropertyByKey("Accept-Language") + "</p>");
            sb.Append("<html><body><p>Accept Encoding :   " +request.getPropertyByKey("Accept-Encoding") + "</p>");

            

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
