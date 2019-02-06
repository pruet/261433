using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class HW1 : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public HW1()
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
            //teach by purit 600611039 and REF:https://en.wikipedia.org/wiki/List_of_HTTP_header_fields
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            sb.Append(request.getPropertyByKey("RemoteEndPoint") + "<br>");
            sb.Append(request.getPropertyByKey("User-Agent")); 
			sb.Append(request.getPropertyByKey("Accept-Language")); 
            sb.Append(request.getPropertyByKey("Accept-Encoding"));
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