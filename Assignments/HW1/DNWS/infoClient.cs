using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;
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
            int worker = new int[2];
            int io = new int[2];
            ThreadPool.GetAvailableThreads(out worker[0], out io[0]);
            ThreadPool.GetMaxThreads(out worker[1],out io[1]);
            try{
                sb.Append("<html><body>");
                sb.Append("Client IP : " + request.getPropertyByKey("RemoteEndPointIP") + "<br />");
                sb.Append("Client Port : " + request.getPropertyByKey("RemoteEndPointPORT") + "<br />");
                sb.Append("Browser Information : " + request.getPropertyByKey("User-Agent") + "<br />");
                sb.Append("Accept Language : " + request.getPropertyByKey("Accept-Language") + "<br />");
                sb.Append("Accept Encoding : " + request.getPropertyByKey("Accept-Encoding") + "<br />");
                sb.Append("Host : " + request.getPropertyByKey("host") + "<br />");
                //ref https://stackoverflow.com/questions/15381174/how-to-count-the-amount-of-concurrent-threads-in-net-application/15381637#15381637
                sb.Append("Amount of Threads: " + Process.GetCurrentProcess().Threads.Count + "<br />"); //cann't be used in program.cs because having a instance same as namespace;
                sb.Append("<h4>Threads Available</h4>");
                sb.Append("<table style='border: 1px solid black;'><tr style='border: 1px solid black;'><th style='border: 1px solid black;'>Worker threads</th><th style='border: 1px solid black;'>Asynchronous I/O threads</th></tr>");
                sb.Append("<tr style='border: 1px solid black;'><td style='border: 1px solid black;'>" + worker[0] + "</td><td style='border: 1px solid black;'>" + io[1] + "</td></tr></table>");
                sb.Append("<br />");
                sb.Append("<h4>Threads running</h4>");
                sb.Append("<table style='border: 1px solid black;'><tr style='border: 1px solid black;'><th style='border: 1px solid black;'>Worker threads</th><th style='border: 1px solid black;'>Asynchronous I/O threads</th></tr>");
                sb.Append("<tr style='border: 1px solid black;'><td style='border: 1px solid black;'>" + worker[1]-worker[0] + "</td><td style='border: 1px solid black;'>" + io[1]-io[0] + "</td></tr></table>");
                sb.Append("<br />");
                sb.Append("</body></html>");
                response = new HTTPResponse(200);
                response.body = Encoding.UTF8.GetBytes(sb.ToString());
            }catch(Exception ex){
                response = new HTTPResponse(500);
                Console.WriteLine(ex.ToString());
                
            }
            return response;
            
        }
    }
}