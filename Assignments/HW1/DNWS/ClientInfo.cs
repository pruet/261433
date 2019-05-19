using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace DNWS
{
  class ClientInfo : IPlugin
  {
    public ClientInfo()
    {
      
    }

    public void PreProcessing(HTTPRequest request)
    {
      throw new NotImplementedException();
    }

    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      string[] remoteEndpoint = request.getPropertyByKey("RemoteEndPoint").Split(':');
      string ip = remoteEndpoint[0], port = remoteEndpoint[1];       
      sb.Append("<html><body>Client IP: "+ ip +"</br></br>");
      sb.Append("Client Port: " + port + "</br></br>");
      sb.Append("Browser Information: " + request.getPropertyByKey("User-Agent") + "</br></br>");
      sb.Append("Accept-Language: " + request.getPropertyByKey("Accept-Language") + "</br></br>");
      sb.Append("Accept-Encoding: " + request.getPropertyByKey("Accept-Encoding") + "</br></br>");
      sb.Append("Thread ID: " + Thread.CurrentThread.ManagedThreadId + "</br></br>");
      //from https://stackoverflow.com/questions/15381174/how-to-count-the-amount-of-concurrent-threads-in-net-application
      sb.Append("Amount of thread: " + Process.GetCurrentProcess().Threads.Count + "</br></br>");
      ThreadPool.GetAvailableThreads(out int workers, out int completion);
      ThreadPool.GetMaxThreads(out int max_workers, out int max_completion);
      sb.Append("Size of thread pool: " + max_workers + "<br /><br />");
      sb.Append("Available threads in thread pool: " + workers + "<br /><br />");    
      sb.Append("Active threads in thread pool: " + (max_workers - workers) +"<br /><br />");
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