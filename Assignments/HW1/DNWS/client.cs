using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class client : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public client()
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
            request.getPropertyByKey("RemoteEndPoint");
            string _req = request.req;
            HTTPResponse response = null;
            StringBuilder sb = new StringBuilder();
            string[] arr = _req.Split("User-Agent:");
            string[] arr2 = arr[1].Split("Accept");
            string[] arr3 = _req.Split("Accept-Encoding:");
            string[] arr4 = arr3[1].Split("Accept");
            string[] arr5 = _req.Split("Accept-Language:");
            string[] port1 = request.getPropertyByKey("RemoteEndPoint").Split(":");
            string[] port = request.getPropertyByKey("RemoteEndPoint").Split(":");
            // string[] arr3 = _req.

            sb.Append("<html><body><h1>Client IP : " +port[0] + "</h1>");
            sb.Append("<html><body><h1>Port : " + port[1] + "</h1>");


            sb.Append("<html><body><h1>Browser information : "+ arr2[0] +"</h1>");
            foreach (KeyValuePair<String, int> entry in statDictionary)
            {
                sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
            }



            sb.Append("<html><body><h1>Accept-Language : " + arr5[1] + "</h1> <br>");

            sb.Append("<html><body><h1>Accept-Encoding : " + arr4[0] + "</h1> <br>");


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
