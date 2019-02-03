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

            sb.Append("<html><body><h1>Client IP:" + "</h1>");
            sb.Append("<h1>Client Port: </h1>");
            sb.Append("<h1>Browser Information" + lines[5].Substring(10) + "</h1>");
            sb.Append("<h1>Accept Language" + lines[8].Substring(15) + "</h1>");
            sb.Append("<h1>Accept Encoding"+ lines[7].Substring(15) + "</h1>");
            //string IPAddress = GetIPAddress();
        
            sb.Append("</body></html>");
            response = new HTTPResponse(200);
            response.body = Encoding.UTF8.GetBytes(sb.ToString());
            return response;
        }

        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }


        //public string GetIPAddress()
        //{
        //    IPHostEntry Host = default(IPHostEntry);
        //    string Hostname = null;
        //    string IPaddress_client = null;
        //    Hostname = Environment.MachineName;
        //    Host = Dns.GetHostEntry(Hostname);

        //    foreach (IPAddress IP in Host.AddressList)
        //    {
        //        if (IP.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            IPaddress_client = Convert.ToString(IP);
        //        }
        //    }
        //    return IPaddress_client;
        //Ref : https://stackoverflow.com/questions/21155352/get-ip-address-of-client-machine
        //}
        
    }
}
