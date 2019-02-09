using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class Clientinfo : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public Clientinfo()
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
            string info_test = request.info; 
            string[] info_1 = request.info.Split(":");
            // 600611010 method
            string[] info_2 = info_1[5].Split("Accept"); 
            string[] info_3 = info_1[6].Split("Accept-Encoding"); //Acpt Language:
            string[] info_4 = info_1[7].Split("Connection");//Acpt Encoding:
            string[] port_addr = request.getPropertyByKey("RemoteEndPoint").Split(":");
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body><h3>Client IP : "+port_addr[0]+"</h3>");
            sb.Append("<html><body><h3>Client Port : "+port_addr[1]+"</h3>"); //600611010 suggest me to do like this
            sb.Append("<html><body><h3>Browser Information : " + info_2[0] + "</h3>");
            sb.Append("<html><body><h3>Accept Language : " + info_3[0] + "</h3>");
            sb.Append("<html><body><h3>Accept Encoding : " + info_4[0] + "</h3>");



          


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