﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class result : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public result()
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
            string[] tmp = request.getPropertyByKey("RemoteEndpoint").Split(':');//pon(1010) told me
            string IP = tmp[0]; // keep ip address
            string Port = tmp[1]; //keep port number
            string[] B = request.Get_B.Split("User-Agent: "); //keep Browser info
            string[] L = request.Get_L.Split("Accept-Language: "); //keep Language info
            string[] E = request.Get_E.Split("Accept-Encoding: "); //keep Encoding info
            sb.Append("<html><body> <b>Client IP</b>: " + IP); //return ip address
            sb.Append("<br /> <b>Client Port</b>: " + Port); //return port number
            sb.Append("<br /> <b>Browser Information</b>: " + B[1]); //return port number
            sb.Append("<br /> <b>Accept Language</b>: " + L[1]); //return port number
            sb.Append("<br /> <b>Accept Encoding</b>: " + E[1]); //return port number
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